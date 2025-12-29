using Empire;
using Empire.NPC.S1API_NPCs;
using MelonLoader;
using S1API.Saveables;

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
                //MelonLogger.Msg("🧹 Resetting Empire static state after Main scene unload");
                //EmpirePhoneApp.Reset();
                //Contacts.Reset();
                
            }
        }

        public override void OnSceneWasInitialized(int buildIndex, string sceneName)
        {
            if (sceneName == "Main")
            {
				// Also reset on initialization to be safe
				//MelonLogger.Msg("🧹 Resetting Empire static state after Main scene initialization");
				//EmpirePhoneApp.Reset();
				//Contacts.Reset();

			}
        }
    }
}