using HarmonyLib;
using RimWorld;
using Verse;
using Verse.AI;

namespace MechanoidFunctionalityRework
{
    [HarmonyPatch(typeof(Pawn_DraftController), "set_Drafted")]
    public static class DraftingPatch
    {
        [HarmonyPrefix]
        public static bool Prefix(Pawn_DraftController __instance, bool value)
        {
            Pawn pawn = __instance.pawn;

            // Allow Centurion to draft mechanoids
            if (pawn.def.defName == "Centurion" && pawn.RaceProps.IsMechanoid)
            {
                __instance.Drafted = value;
                return false; // Skip original method
            }

            return true; // Continue with original method
        }
    }
}