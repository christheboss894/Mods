using System.IO;
using System.Linq;
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
            ModPath = Path.Combine(Application.persistentDataPath, "Mods/GenericMod");
            System.Version version = typeof(GenericMod).Assembly.GetName().Version;
            SceneManager.sceneLoaded += OnSceneLoaded;
            InfiniteInventory = false;
            Cheese = false;
            ArtificialSun = false;
            if (scene.name == "Island")
                Debug.Log(string.Format("GenericMod loaded: {0}, build time: {1}", version, File.GetLastWriteTime(typeof(GenericMod).Assembly.Location).ToShortTimeString()));
            Loaded = true;
        }

        // Token: 0x06000005 RID: 5 RVA: 0x0000217C File Offset: 0x0000037C
        private void OnSceneLoaded(Scene scene, LoadSceneMode arg1)
        {
            if (scene.name != "Island" || onSceneLoadedDone) return;
            onSceneLoadedDone = true;
            if (GameResources.Instance.Items.FirstOrDefault(s => s.name == "NullItem") != null)
            {
                return;
            }
            {
                InfiniteInventoryCheck();
                CheeseCheck();
                ArtificialSunCheck();
                new GameObject("GenericModItemScript", typeof(Items));
                new GameObject("GenericModRecipeScript", typeof(Recipes));
                new GameObject("GenericModCustomRecipesScript", typeof(CustomRecipes));
                new GameObject("GenericModDepositsScript", typeof(Deposits));
                // Adds a new lava source to keep volcano running after end game
                new GameObject("UnstableLavaSource", typeof(LavaSource));
            }
        }
        private void InfiniteInventoryCheck()
        {
            if (File.Exists(Path.Combine(
                    ModPath,
                    "InfiniteInventory.txt")))
            {
                Debug.Log("Infinite Inventory enabled");
                InfiniteInventory = true;
            }
        }
        private void ArtificialSunCheck()
        {
            if (File.Exists(Path.Combine(
                    ModPath,
                    "ArtificialSun.txt")))
            {
                Debug.Log("Artificial Sun enabled");
                ArtificialSun = true;
            }
        }
        private void CheeseCheck()
        {
            if (File.Exists(Path.Combine(
                    ModPath,
                    "Cheese.txt")))
            {
                Debug.Log("Cheese enabled");
                Cheese = true;
            }
        }

        public bool Loaded;
        public static bool InfiniteInventory;
        public static bool onSceneLoadedDone;
        public static bool Cheese;
        public static bool ArtificialSun;

        public static Scene scene;

        public string ModPath;

    }
}
