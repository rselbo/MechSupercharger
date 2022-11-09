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
            listingStandard.Label($"Normal supercharger toxic waste factor {settings.NormalToxicWasteFactor}", tooltip: "The factor of how much toxic waste the machine uses compared to a vanilla charger");
            Widgets.HorizontalSlider(listingStandard.GetRect(22f), ref settings.NormalToxicWasteFactor, new FloatRange(0.05f, 5f), label: "", roundTo: 0.05f);
            listingStandard.TextFieldNumericLabeled("Normal supercharger idle power", ref settings.NormalIdlePower, ref NormalIdlePower, 0, 10000);
            listingStandard.TextFieldNumericLabeled("Normal supercharger base power draw", ref settings.NormalBasePower, ref NormalBasePower, 0, 10000);
            listingStandard.Label($"");

            listingStandard.Label($"Large supercharger toxic waste factor {settings.LargeToxicWasteFactor}", tooltip: "The factor of how much toxic waste the machine uses compared to a vanilla charger");
            Widgets.HorizontalSlider(listingStandard.GetRect(22f), ref settings.LargeToxicWasteFactor, new FloatRange(0.05f, 5f), label: "", roundTo: 0.05f);
            listingStandard.TextFieldNumericLabeled("Large supercharger idle power", ref settings.LargeIdlePower, ref LargeIdlePower, 0, 10000);
            listingStandard.TextFieldNumericLabeled("Large supercharger base power draw", ref settings.LargeBasePower, ref LargeBasePower, 0, 10000);
            listingStandard.Label($"");

            listingStandard.Label($"Normal advanced supercharger toxic waste factor {settings.NormalAdvancedToxicWasteFactor}", tooltip: "The factor of how much toxic waste the machine uses compared to a vanilla charger");
            Widgets.HorizontalSlider(listingStandard.GetRect(22f), ref settings.NormalAdvancedToxicWasteFactor, new FloatRange(0f, 1f), label: "", roundTo: 0.05f);
            listingStandard.TextFieldNumericLabeled("Normal advanced supercharger idle power", ref settings.NormalAdvancedIdlePower, ref NormalAdvancedIdlePower, 0, 10000);
            listingStandard.TextFieldNumericLabeled("Normal advanced supercharger base power draw", ref settings.NormalAdvancedBasePower, ref NormalAdvancedBasePower, 0, 10000);
            listingStandard.Label($"");

            listingStandard.Label($"Large advanced supercharger toxic waste factor {settings.LargeAdvancedToxicWasteFactor}", tooltip: "The factor of how much toxic waste the machine uses compared to a vanilla charger");
            Widgets.HorizontalSlider(listingStandard.GetRect(22f), ref settings.LargeAdvancedToxicWasteFactor, new FloatRange(0f, 1f), label: "", roundTo: 0.05f);
            listingStandard.TextFieldNumericLabeled("Large advanced supercharger idle power", ref settings.LargeAdvancedIdlePower, ref LargeAdvancedIdlePower, 0, 10000);
            listingStandard.TextFieldNumericLabeled("Large advanced Supercharger base power draw", ref settings.LargeAdvancedBasePower, ref LargeAdvancedBasePower, 0, 10000);
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
            return "Mech Supercharger";
        }

    }
}
