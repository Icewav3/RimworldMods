using HarmonyLib;
using Verse;

namespace MechanoidFunctionalityRework
{
    [StaticConstructorOnStartup]
    public static class CenturionMod
    {
        static CenturionMod()
        {
            var harmony = new Harmony("com.Icewave.MechanoidFunctionalityRework");
            harmony.PatchAll();
        }
    }
}