using HarmonyLib;
using RimWorld;
using Verse;

namespace MechanoidFunctionalityRework
{
    [HarmonyPatch(typeof(Building), "TickRare")]
    public static class BandNodePatch
    {
        private const float ControlRangeExtension = 15f; // Adjust as needed

        [HarmonyPostfix]
        public static void Postfix(Building __instance)
        {
            // Check if building is a BandNode
            if (__instance.def.defName != "BandNode") return;

            var map = __instance.Map;
            if (map != null)
            {
                // Find all mechanitors within the Band Node's effect radius
                foreach (Pawn pawn in map.mapPawns.AllPawnsSpawned)
                {
                    if (pawn.def.defName == "Centurion" && pawn.TryGetComp<CompMechanitor>() != null) //TODO its actually a hediff and is called MECHLINK
                    {
                        CompMechanitor mechanitor = pawn.TryGetComp<CompMechanitor>();

                        // Extend the control range
                        ExtendControlRange(mechanitor, __instance.Position);
                    }
                }
            }
        }

        private static void ExtendControlRange(CompMechanitor mechanitor, IntVec3 bandNodePosition)
        {
            float currentRange = mechanitor.controlRange;
            float newRange = currentRange + ControlRangeExtension;

            // Check if the Band Node is within the original control range
            if (mechanitor.parent.Position.DistanceTo(bandNodePosition) <= currentRange)
            {
                mechanitor.controlRange = newRange;
            }
        }
    }
}