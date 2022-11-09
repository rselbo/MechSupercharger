using HarmonyLib;
using RimWorld;
using Verse;

namespace MechSupercharger
{
    [StaticConstructorOnStartup]
    internal static class HarmonyPatches
    {
        static HarmonyPatches() => HarmonyPatches.Patch();
        public static void Patch()
        {
            Harmony harmony = new Harmony("net.selbo.rimworld.mechsupercharger");
            harmony.PatchAll();
        }
    }

    [HarmonyPatch(typeof(ThingListGroupHelper))]
    [HarmonyPatch("Includes")]
    static class ThingListGroupHelper_Includes_Patch
    {
        // Add typeof(Building_MechSupercharger) to the ThingRequestGroup.MechCharger
        static void Postfix(ref bool __result, ThingRequestGroup group, ThingDef def)
        {
            if (group == ThingRequestGroup.MechCharger)
            {
                __result = __result || def.thingClass == typeof(Building_MechSupercharger);
            }
        }
    }

    [HarmonyPatch(typeof(Building_MechCharger))]
    [HarmonyPatch(nameof(Building_MechCharger.StartCharging))]
    static class Building_MechCharger_StartCharging_Patch
    {
        //Hook onto Building_MechCharger.StartCharging so we can react to charging station in use
        static void Postfix(Building_MechCharger __instance)
        {
            Building_MechSupercharger supercharger = __instance as Building_MechSupercharger;
            if (supercharger == null) return;
            supercharger.HasMechCharging = true;
        }
    }

    [HarmonyPatch(typeof(Building_MechCharger))]
    [HarmonyPatch(nameof(Building_MechCharger.StopCharging))]
    static class Building_MechCharger_StopCharging_Patch
    {
        //Hook onto Building_MechCharger.StopCharging so we can react to charging station not in use
        static void Postfix(Building_MechCharger __instance)
        {
            Building_MechSupercharger supercharger = __instance as Building_MechSupercharger;
            if (supercharger == null) return;
            supercharger.HasMechCharging = false;
        }
    }
}
