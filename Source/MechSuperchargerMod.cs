using HarmonyLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Verse;

namespace MechSupercharger
{
    public class MechSuperchargerMod : Mod
    {
        public MechSuperchargerMod(ModContentPack content) : base(content)
        {
            var harmony = new Harmony("net.selbo.mechsupercharger");
            harmony.PatchAll();
        }
    }
}
