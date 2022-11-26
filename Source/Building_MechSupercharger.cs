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
    enum SuperchargerType
    {
        NormalSupercharger,
        LargeSupercharger,
        NormalAdvancedSupercharger,
        LargeAdvancedSupercharger
    }

    public class Building_MechSupercharger : Building_MechCharger
    {
        public int SuperchargeLevel = 1;
        public float IdlePowerDraw => GetIdlePowerDraw();
        public float BasePowerDraw => GetBasePowerDraw();
        public float ToxicEfficiency => GetToxicEfficiency();

        public bool HasMechCharging = false;
        
        private MechSuperchargerSettings settings = null;
        private SuperchargerType type;

        private FieldInfo currentlyChargingMech;
        private Pawn CurrentlyChargingMech => (Pawn)currentlyChargingMech.GetValue(this);
        private FieldInfo wasteProduced;
        private float WasteProduced
        {
            get { return (float)wasteProduced.GetValue(this); }
            set { wasteProduced.SetValue(this, value); }
        }

        private ThingComp_Supercharger SuperchargerComp => this.TryGetComp<ThingComp_Supercharger>();

        private int GetIdlePowerDraw()
        {
            switch (type)
            {
                case SuperchargerType.NormalSupercharger: return settings.NormalIdlePower;
                case SuperchargerType.LargeSupercharger: return settings.LargeIdlePower;
                case SuperchargerType.NormalAdvancedSupercharger: return settings.NormalAdvancedIdlePower;
                case SuperchargerType.LargeAdvancedSupercharger: return settings.LargeAdvancedIdlePower;
            }
            return 0;
        }
        private int GetBasePowerDraw()
        {
            switch (type)
            {
                case SuperchargerType.NormalSupercharger: return settings.NormalBasePower;
                case SuperchargerType.LargeSupercharger: return settings.LargeBasePower;
                case SuperchargerType.NormalAdvancedSupercharger: return settings.NormalAdvancedBasePower;
                case SuperchargerType.LargeAdvancedSupercharger: return settings.LargeAdvancedBasePower;
            }
            return 0;
        }
        private float GetToxicEfficiency()
        {
            switch (type)
            {
                case SuperchargerType.NormalSupercharger: return settings.NormalToxicWasteFactor;
                case SuperchargerType.LargeSupercharger: return settings.LargeToxicWasteFactor;
                case SuperchargerType.NormalAdvancedSupercharger: return settings.NormalAdvancedToxicWasteFactor;
                case SuperchargerType.LargeAdvancedSupercharger: return settings.LargeAdvancedToxicWasteFactor;
            }
            return 0;
        }
        public override void SpawnSetup(Map map, bool respawningAfterLoad)
        {
            settings = LoadedModManager.GetMod<MechSuperchargerMod >().GetSettings<MechSuperchargerSettings>();

            base.SpawnSetup(map, respawningAfterLoad);

            switch (def.defName)
            {
                case "MS_BasicSupercharger":
                    type = SuperchargerType.NormalSupercharger;
                    break;
                case "MS_StandardSupercharger":
                    type = SuperchargerType.LargeSupercharger;
                    break;
                case "MS_BasicAdvancedSupercharger":
                    type = SuperchargerType.NormalAdvancedSupercharger;
                    break;
                case "MS_StandardAdvancedSupercharger":
                    type = SuperchargerType.LargeAdvancedSupercharger;
                    break;
            }

            var flags = BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance;
            foreach (var field in typeof(Building_MechCharger).GetFields(flags))
            {
                if (field.Name == "currentlyChargingMech")
                {
                    currentlyChargingMech = field;
                    continue;
                }
                if (field.Name == "wasteProduced")
                {
                    wasteProduced = field;
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
                Power.PowerOutput = -(BasePowerDraw * SuperchargerComp.OverCharge);
                Pawn chargingMech = CurrentlyChargingMech;
                if (chargingMech == null)
                {
                    Log.Warning("Could not resolve a charging mech");
                    return;
                }
                chargingMech.needs.energy.CurLevel += 0.000833333354f * (SuperchargerComp.OverCharge - 1);
                float wasteProducedPerTick = chargingMech.GetStatValue(StatDefOf.WastepacksPerRecharge) * (0.000833333354f / chargingMech.needs.energy.MaxLevel);
                if(ToxicEfficiency == 0)
                {
                    WasteProduced = 0;
                }
                else
                {
                    float wasteProduced = WasteProduced;
                    wasteProduced += wasteProducedPerTick * (SuperchargerComp.OverCharge - 1) * ToxicEfficiency;
                    WasteProduced = wasteProduced;
                }
            }
            else
            {
                Power.PowerOutput = -IdlePowerDraw;
            }
        }
    }
}
