using HarmonyLib;
using System.Collections.Generic;
using System.Linq;

namespace FreeHandBoombox.Patches
{
    [HarmonyPatch(typeof(BoomboxItem))]
    internal class BoomboxPatch
    {
        [HarmonyPatch("PocketItem")]
        [HarmonyTranspiler]
        public static IEnumerable<CodeInstruction> Transpiler(IEnumerable<CodeInstruction> instructions)
        {
            /*
             * We have:
             * [ ldarg.0, call, ldarg.0, ldc.i4.0, ldc.i4.0, call, ret ]
             * 
             * First call is base pocket method, second call turns off Boombox music
             * 
             * We want:
             * [ ldarg.0, call, ret ]
             */
            List<CodeInstruction> allInstructions = new List<CodeInstruction>(instructions);

            int startMusicArgIndex = allInstructions.FindLastIndex(instruction => instruction.IsLdarg());
            int startMusicCallIndex = allInstructions.FindIndex(instruction => instruction.ToString().Contains("BoomboxItem::StartMusic"));

            allInstructions.RemoveRange(startMusicArgIndex, startMusicCallIndex - startMusicArgIndex + 1);

            return allInstructions.AsEnumerable();
        }
    }
}
