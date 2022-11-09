using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Verse;

namespace MechSupercharger
{
    internal class MechSuperchargerSettings : ModSettings
    {
        public int NormalIdlePower = 200;
        public int NormalBasePower = 200;
        public float NormalToxicWasteFactor = 1.25f;
        public int LargeIdlePower = 400;
        public int LargeBasePower = 400;
        public float LargeToxicWasteFactor = 1.25f;
        public int NormalAdvancedIdlePower = 300;
        public int NormalAdvancedBasePower = 300;
        public float NormalAdvancedToxicWasteFactor = 0f;
        public int LargeAdvancedIdlePower = 500;
        public int LargeAdvancedBasePower = 500;
        public float LargeAdvancedToxicWasteFactor = 0f;
        public override void ExposeData()
        {
            Scribe_Values.Look(ref NormalIdlePower, "NormalIdlePower");
            Scribe_Values.Look(ref NormalBasePower, "NormalBasePower");
            Scribe_Values.Look(ref NormalToxicWasteFactor, "NormalToxicWasteFactor");
            Scribe_Values.Look(ref LargeIdlePower, "LargeIdlePower");
            Scribe_Values.Look(ref LargeBasePower, "LargeBasePower");
            Scribe_Values.Look(ref LargeToxicWasteFactor, "LargeToxicWasteFactor");
            Scribe_Values.Look(ref NormalAdvancedIdlePower, "NormalAdvancedIdlePower");
            Scribe_Values.Look(ref NormalAdvancedBasePower, "NormalAdvancedBasePower");
            Scribe_Values.Look(ref NormalAdvancedToxicWasteFactor, "NormalAdvancedToxicWasteFactor");
            Scribe_Values.Look(ref LargeAdvancedIdlePower, "LargeAdvancedIdlePower");
            Scribe_Values.Look(ref LargeAdvancedBasePower, "LargeAdvancedBasePower");
            Scribe_Values.Look(ref LargeAdvancedToxicWasteFactor, "LargeAdvancedToxicWasteFactor");
            base.ExposeData();
        }
    }
}
