using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using Verse;

namespace MechSupercharger
{
    internal class ThingComp_Supercharger : ThingComp
    {
        public CompProperties_Supercharger Props => (CompProperties_Supercharger)this.props;
        public int OverCharge = 1;
        public float WasteEfficiency => Props.WasteEfficiency;

        public void IncreaseOvercharge()
        {
            if (OverCharge >= 10)
                return;
            ++OverCharge;
        }

        public void DecreaseOvercharge()
        {
            if (OverCharge <= 1)
                return;
            --OverCharge;
        }

        public override void PostExposeData()
        {
            base.PostExposeData();
            Scribe_Values.Look<int>(ref this.OverCharge, "OverCharge");
        }

        public override IEnumerable<Gizmo> CompGetGizmosExtra()
        {
            Command_Action commandAction1 = new Command_Action();
            commandAction1.action = new Action(this.DecreaseOvercharge);
            commandAction1.defaultLabel = (string)"UI_LowerPower".Translate();
            commandAction1.defaultDesc = (string)"UI_LowerPowerDesc".Translate();
            commandAction1.icon = (Texture)ContentFinder<Texture2D>.Get("UI/Commands/TempLower");
            yield return (Gizmo)commandAction1;
            Command_Action commandAction2 = new Command_Action();
            commandAction2.action = new Action(this.IncreaseOvercharge);
            commandAction2.defaultLabel = (string)"UI_RaisePower".Translate();
            commandAction2.defaultDesc = (string)"UI_RaisePowerDesc".Translate();
            commandAction2.icon = (Texture)ContentFinder<Texture2D>.Get("UI/Commands/TempRaise");
            yield return (Gizmo)commandAction2;
        }
    }
}
