using HarmonyLib;
using RimWorld;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Verse;

namespace MechSupercharger
{
    public class Building_MechSupercharger : Building_MechCharger
    {
        public int SuperchargeLevel = 1;
        public float BasePowerDraw = 0;
        public bool HasMechCharging = false;

        private FieldInfo currentlyChargingMech;
        private Pawn CurrentlyChargingMech => (Pawn)currentlyChargingMech.GetValue(this);
        private FieldInfo wasteProduced;
        private float WasteProduced
        {
            get { return (float)wasteProduced.GetValue(this); }
            set { wasteProduced.SetValue(this, value); }
        }
        private float ChargePerTick = 0;

        private ThingComp_Supercharger SuperchargerComp => this.TryGetComp<ThingComp_Supercharger>();

        public override void SpawnSetup(Map map, bool respawningAfterLoad)
        {
            base.SpawnSetup(map, respawningAfterLoad);
            BasePowerDraw = Power.PowerOutput;

            var flags = BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance;
            foreach (var field in typeof(Building_MechCharger).GetFields(flags))
            {
                if (field.Name == "currentlyChargingMech")
                {
                    Log.Message($"{ToString()} captured currentlyChargingMech");
                    currentlyChargingMech = field;
                    continue;
                }
                if (field.Name == "wasteProduced")
                {
                    Log.Message($"{ToString()} captured wasteProduced");
                    wasteProduced = field;
                    continue;
                }
                if (field.Name == "ChargePerTick")
                {
                    ChargePerTick = (float)field.GetValue(this);
                    Log.Message($"{ToString()} captured currentlyChargingMech value {ChargePerTick}");
                    continue;
                }
            }
        }

        public override string GetInspectString()
        {
            StringBuilder stringBuilder = new StringBuilder();
            String inspect = base.GetInspectString();
            if (!inspect.NullOrEmpty())
                stringBuilder.Append(inspect);
            if(SuperchargerComp != null)
            {
                stringBuilder.Append($"\nSupercharge Level: {SuperchargerComp.OverCharge}");
            }
            return stringBuilder.ToString();
        }


        public override void Tick()
        {
            base.Tick();
            if (HasMechCharging)
            {
                Power.PowerOutput = BasePowerDraw * SuperchargerComp.OverCharge;
                Pawn chargingMech = CurrentlyChargingMech;
                if (chargingMech == null)
                {
                    Log.Warning("Could not resolve a chanring mech");
                    return;
                }
                chargingMech.needs.energy.CurLevel += 0.000833333354f * (SuperchargerComp.OverCharge - 1);
                float wasteProducedPerTick = chargingMech.GetStatValue(StatDefOf.WastepacksPerRecharge) * (0.000833333354f / chargingMech.needs.energy.MaxLevel);
                if(SuperchargerComp.WasteEfficiency == 0)
                {
                    WasteProduced = 0;
                }
                else
                {
                    float wasteProduced = WasteProduced;
                    wasteProduced += wasteProducedPerTick * (SuperchargerComp.OverCharge - 1) * SuperchargerComp.WasteEfficiency;
                    WasteProduced = wasteProduced;
                }
            }
            else
            {
                Power.PowerOutput = BasePowerDraw;
            }
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
            if(supercharger == null) return;
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
            supercharger.HasMechCharging = true;
        }
    }
}
