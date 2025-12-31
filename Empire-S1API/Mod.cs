using Empire;
using Empire.NPC;
using Empire.Phone;
using MelonLoader;

[assembly: MelonInfo(typeof(MyMod), "Empire (Forked by Kaen01)", "2.0", "Aracor")]
[assembly: MelonGame("TVGS", "Schedule I")]

namespace Empire
{
    public class MyMod : MelonMod
    {
        public override void OnInitializeMelon()
        {
			// Initialize JSON data first
			//JSONDeserializer.Initialize();
		}

		public override void OnSceneWasUnloaded(int buildIndex, string sceneName)
        {
            if (sceneName == "Main")
            {
                MelonLogger.Msg("🧹 Resetting Empire static state after Main scene unload");
				if (EmpirePhoneApp.Instance != null) 
                    EmpirePhoneApp.Reset();

                Contacts.Reset();
                
            }
        }

        public override void OnSceneWasInitialized(int buildIndex, string sceneName)
        {
            if (sceneName == "Main")
            {
                // Also reset on initialization to be safe
                MelonLogger.Msg("🧹 Resetting Empire static state after Main scene initialization");
                if (EmpirePhoneApp.Instance != null)
                    EmpirePhoneApp.Reset();
                
                Contacts.Reset();

            }
        }
    }
}