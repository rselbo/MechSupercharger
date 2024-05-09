using RimWorld;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using Verse;

namespace MechSupercharger
{
    public class MechSuperchargerMod : Mod
    {
        private MechSuperchargerSettings settings;
        private string NormalIdlePower;
        private string NormalBasePower;
        private string LargeIdlePower;
        private string LargeBasePower;
        private string NormalAdvancedIdlePower;
        private string NormalAdvancedBasePower;
        private string LargeAdvancedIdlePower;
        private string LargeAdvancedBasePower;

        public MechSuperchargerMod(ModContentPack content) : base(content)
        {
            settings = GetSettings<MechSuperchargerSettings>();
        }

        /// <summary>
        /// The (optional) GUI part to set your settings.
        /// </summary>
        /// <param name="inRect">A Unity Rect with the size of the settings window.</param>
        public override void DoSettingsWindowContents(Rect inRect)
        {
            Listing_Standard listingStandard = new Listing_Standard();
            listingStandard.Begin(inRect);
            listingStandard.Label("Settings_ToxicWasteFactor".Translate("Settings_Normal".Translate(), settings.NormalToxicWasteFactor.ToString("F2")), tooltip: "Settings_ToxicWasteFactorTooltip".Translate());
            Widgets.HorizontalSlider(listingStandard.GetRect(22f), ref settings.NormalToxicWasteFactor, new FloatRange(0.05f, 5f), label: "", roundTo: 0.05f);
            listingStandard.TextFieldNumericLabeled("Settings_IdlePower".Translate("Settings_Normal".Translate()), ref settings.NormalIdlePower, ref NormalIdlePower, 0, 10000);
            listingStandard.TextFieldNumericLabeled("Settings_BasePower".Translate("Settings_Normal".Translate()), ref settings.NormalBasePower, ref NormalBasePower, 0, 10000);
            listingStandard.Label($"");


            listingStandard.Label("Settings_ToxicWasteFactor".Translate("Settings_Large".Translate(), settings.LargeToxicWasteFactor.ToString("F2")), tooltip: "Settings_ToxicWasteFactorTooltip".Translate());
            Widgets.HorizontalSlider(listingStandard.GetRect(22f), ref settings.LargeToxicWasteFactor, new FloatRange(0.05f, 5f), label: "", roundTo: 0.05f);
            listingStandard.TextFieldNumericLabeled("Settings_IdlePower".Translate("Settings_Large".Translate()), ref settings.LargeIdlePower, ref LargeIdlePower, 0, 10000);
            listingStandard.TextFieldNumericLabeled("Settings_BasePower".Translate("Settings_Large".Translate()), ref settings.LargeBasePower, ref LargeBasePower, 0, 10000);
            listingStandard.Label($"");

            listingStandard.Label("Settings_ToxicWasteFactor".Translate("Settings_NormalAdvanced".Translate(), settings.NormalAdvancedToxicWasteFactor.ToString("F2")), tooltip: "Settings_ToxicWasteFactorTooltip".Translate());
            Widgets.HorizontalSlider(listingStandard.GetRect(22f), ref settings.NormalAdvancedToxicWasteFactor, new FloatRange(0f, 1f), label: "", roundTo: 0.05f);
            listingStandard.TextFieldNumericLabeled("Settings_IdlePower".Translate("Settings_NormalAdvanced".Translate()), ref settings.NormalAdvancedIdlePower, ref NormalAdvancedIdlePower, 0, 10000);
            listingStandard.TextFieldNumericLabeled("Settings_BasePower".Translate("Settings_NormalAdvanced".Translate()), ref settings.NormalAdvancedBasePower, ref NormalAdvancedBasePower, 0, 10000);
            listingStandard.Label($"");

            listingStandard.Label("Settings_ToxicWasteFactor".Translate("Settings_LargeAdvanced".Translate(), settings.LargeAdvancedToxicWasteFactor.ToString("F2")), tooltip: "Settings_ToxicWasteFactorTooltip".Translate());
            Widgets.HorizontalSlider(listingStandard.GetRect(22f), ref settings.LargeAdvancedToxicWasteFactor, new FloatRange(0f, 1f), label: "", roundTo: 0.05f);
            listingStandard.TextFieldNumericLabeled("Settings_IdlePower".Translate("Settings_LargeAdvanced".Translate()), ref settings.LargeAdvancedIdlePower, ref LargeAdvancedIdlePower, 0, 10000);
            listingStandard.TextFieldNumericLabeled("Settings_BasePower".Translate("Settings_LargeAdvanced".Translate()), ref settings.LargeAdvancedBasePower, ref LargeAdvancedBasePower, 0, 10000);
            listingStandard.Label($"");

            base.DoSettingsWindowContents(inRect);
        }

        /// <summary>
        /// Override SettingsCategory to show up in the list of settings.
        /// Using .Translate() is optional, but does allow for localisation.
        /// </summary>
        /// <returns>The (translated) mod name.</returns>
        public override string SettingsCategory()
        {
            return "Settings_MechSupercharger".Translate();
        }

    }
}
