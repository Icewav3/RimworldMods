using HarmonyLib;
using RimWorld;
using RimWorld.Planet;
using Verse;

namespace MechanoidFunctionalityRework
{
    [HarmonyPatch(typeof(Dialog_FormCaravan), "TryReformCaravan")]
    public static class CaravanFormationPatch
    {
        [HarmonyPrefix]
        public static bool Prefix(ref bool __result, Caravan caravan)
        {
            bool hasColonist = caravan.PawnsListForReading.Any(pawn => pawn.IsColonist);

            if (!hasColonist)
            {
                hasColonist = caravan.PawnsListForReading.Any(pawn => pawn.def == ThingDef.Named("Centurion"));
            }

            __result = hasColonist;
            return false; // Skip original method if Centurion is found
        }
    }
}
