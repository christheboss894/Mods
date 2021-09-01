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
            //Cheese = false;
            ArtificialSun = false;
            if (scene.name == "Island")
                Debug.Log(string.Format("GenericMod loaded: {0}, build time: {1}", version, File.GetLastWriteTime(typeof(GenericMod).Assembly.Location).ToShortTimeString()));
            Loaded = true;
        }

        // Token: 0x06000005 RID: 5 RVA: 0x0000217C File Offset: 0x0000037C
        private void OnSceneLoaded(Scene scene, LoadSceneMode arg1)
        {
            if (scene.name == "Island")
            {
                ArtificialSunCheck();
                //CheeseCheck();
                InfiniteInventoryCheck();
                Deposits.Run();
                // Adds a new lava source to keep volcano running after end game
                new GameObject("UnstableLavaSource", typeof(LavaSource));
            }

        }
        /*private void OnGameLoaded(Scene gameScene)
        {
            if (gameScene.name == "Island")
            {
                Deposits.Run();
                // Adds a new lava source to keep volcano running after end game
                new GameObject("UnstableLavaSource", typeof(LavaSource));
            }
        }
        private void OnInitData()
        {
            InfiniteInventoryCheck();
            CheeseCheck();
            ArtificialSunCheck();
            Items.Run();
            Recipes.Run();
            CustomRecipes.Run();
        }
        */
        private void InfiniteInventoryCheck()
        {
            if (File.Exists(Path.Combine(
                    ModPath,
                    "InfiniteInventory.txt")))
            {
                Debug.Log("Infinite Inventory enabled");
                foreach (Recipe recipe in RuntimeAssetDatabase.Get<Recipe>())
                {
                    recipe.Inputs = new InventoryItem[0];
                    recipe.ProductionTime = 0f;
                }
            }
        }
        private void ArtificialSunCheck()
        {
            if (File.Exists(Path.Combine(
                    ModPath,
                    "ArtificialSun.txt")))
            {
                Debug.Log("Artificial Sun enabled");
                RuntimeAssetDatabase.Get<Recipe>().FirstOrDefault(s => s.name == "LightRecipe").Inputs = new InventoryItem[0];

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
