using BepInEx.Logging;
using BepInEx;
using HarmonyLib;
using FreeHandBoombox.Patches;

namespace PocketBoombox
{
    [BepInPlugin(modGUID, modName, modVersion)]
    public class PocketBoomboxBase : BaseUnityPlugin
    {
        private const string modGUID = "moritzrp.PocketBoombox";
        private const string modName = "PocketBoombox";
        private const string modVersion = "1.0.0";

        private readonly Harmony harmony = new Harmony(modGUID);
        private static PocketBoomboxBase instance;

        internal ManualLogSource logger;

        private void Awake()
        {
            if (instance == null)
            {
                instance = this;
            }

            logger = BepInEx.Logging.Logger.CreateLogSource(modGUID);

            harmony.PatchAll(typeof(PocketBoomboxBase));
            harmony.PatchAll(typeof(BoomboxPatch));
            logger.LogInfo("Boombox ready for pocketing");
        }
    }
}
