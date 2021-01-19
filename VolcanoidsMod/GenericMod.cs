using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace VolcanoidsMod
{
    // Token: 0x02000003 RID: 3
    public class GenericMod : GameMod
    {
        // Token: 0x06000004 RID: 4 RVA: 0x00002110 File Offset: 0x00000310
        public override void Load()
        {
            System.Version version = typeof(GenericMod).Assembly.GetName().Version;
            SceneManager.sceneLoaded += OnSceneLoaded;
            Debug.Log(string.Format("GenericMod loaded: {0}, build time: {1}", version, File.GetLastWriteTime(typeof(GenericMod).Assembly.Location).ToShortTimeString()));
            Loaded = true;
        }

        // Token: 0x06000005 RID: 5 RVA: 0x0000217C File Offset: 0x0000037C
        private void OnSceneLoaded(Scene scene, LoadSceneMode loadSceneMode)
        {
            if (scene.name != "Island" || onSceneLoadedDone) return;
            onSceneLoadedDone = true;
            Debug.Log("what the fuck");
            InfiniteInventory = false;
            InfiniteInventory = false;
            Cheese = false;
            ArtificialSun = false;
            if (scene.name == "Island")
            {
                InfiniteInventoryCheck();
                CheeseCheck();
                ArtificialSunCheck();
                new GameObject("GenericModFramework", typeof(Framework));
                new GameObject("GenericModBaseScript", typeof(ModBehaviour));
                new GameObject("GenericModItemScript", typeof(Items));
                new GameObject("GenericModModuleScript", typeof(Modules));
                new GameObject("GenericModRecipeScript", typeof(Recipes));
                new GameObject("GenericModCustomRecipesScript", typeof(CustomRecipes));
                new GameObject("GenericModDepositsScript", typeof(Deposits));
                // Adds a new lava source to keep volcano running after end game
                new GameObject("UnstableLavaSource", typeof(LavaSource));
                new GameObject("GenericModMiscDebugScript", typeof(MiscDebug));
            }
        }
        private void InfiniteInventoryCheck()
        {
            if (File.Exists(Path.Combine(
                    Application.persistentDataPath,
                    "InfiniteInventory.txt")))
            {
                Debug.Log("Infinite Inventory enabled");
                InfiniteInventory = true;
            }
        }
        private void ArtificialSunCheck()
        {
            if (File.Exists(Path.Combine(
                    Application.persistentDataPath,
                    "ArtificialSun.txt")))
            {
                Debug.Log("Artificial Sun enabled");
                ArtificialSun = true;
            }
        }
        private void CheeseCheck()
        {
            if (File.Exists(Path.Combine(
                    Application.persistentDataPath,
                    "Cheese.txt")))
            {
                Debug.Log("Cheese enabled");
                Cheese = true;
            }
        }

        // Token: 0x06000006 RID: 6 RVA: 0x000021B3 File Offset: 0x000003B3
        public override void Unload()
        {
            Debug.Log("Mod unloaded");
            Loaded = false;
        }
        public bool Loaded;
        public static bool InfiniteInventory;
        private bool onSceneLoadedDone;
        public static bool Cheese;
        public static bool ArtificialSun;

        public static Scene scene;

        

    }
}
