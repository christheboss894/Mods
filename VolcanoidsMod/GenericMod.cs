using System;
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
            System.Version version = typeof(GenericMod).Assembly.GetName().Version;
            SceneManager.sceneLoaded += this.OnSceneLoaded;
            Debug.Log(string.Format("GenericMod loaded: {0}, build time: {1}", version, File.GetLastWriteTime(typeof(GenericMod).Assembly.Location).ToShortTimeString()));
            this.Loaded = true;
        }

        // Token: 0x06000005 RID: 5 RVA: 0x0000217C File Offset: 0x0000037C
        private void OnSceneLoaded(Scene arg0, LoadSceneMode arg1)
        {

            if (arg0.name == "Island")
            {
                InfiniteInventoryCheck();
                CheeseCheck();
                scene = arg0;
                new GameObject("GenericModBaseScript", typeof(ModBehaviour));
                new GameObject("GenericModItemScript", typeof(Items));
                new GameObject("GenericModModuleScript", typeof(Modules));
                new GameObject("GenericModMiscDebugScript", typeof(MiscDebug));
                new GameObject("GenericModRecipeScript", typeof(Recipes));
                new GameObject("GenericModCustomRecipesScript", typeof(CustomRecipes));
                // Adds a new lava source to keep volcano running after end game
                new GameObject("UnstableLavaSource", typeof(LavaSource));
            }
        }

        private void InfiniteInventoryCheck()
        {
            if (File.Exists(Application.persistentDataPath + "\\InfiniteInventory.txt")) {
                Debug.Log(File.Exists(Application.persistentDataPath + "\\InfiniteInventory.txt"));
                InfiniteInventory = true;
            }
            else
            {
                Debug.Log(File.Exists(Application.persistentDataPath + "\\InfiniteInventory.txt"));
                InfiniteInventory = false;
            }

        }
        private void CheeseCheck()
        {
            if (File.Exists(Application.persistentDataPath + "\\Cheese.txt"))
            {
                Debug.Log(File.Exists(Application.persistentDataPath + "\\Cheese.txt"));
                Cheese = true;
            }
            else
            {
                Debug.Log(File.Exists(Application.persistentDataPath + "\\Cheese.txt"));
                Cheese = false;
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
        public static bool Cheese;

        public static Scene scene;

        

    }
}
