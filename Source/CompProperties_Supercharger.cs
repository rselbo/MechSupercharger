using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Verse;

namespace MechSupercharger
{
    internal class CompProperties_Supercharger : CompProperties
    {
        public float WasteEfficiency = 1f;

        public CompProperties_Supercharger()
        {
            this.compClass = typeof(ThingComp_Supercharger);
        }
    }
}
