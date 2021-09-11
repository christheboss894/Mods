using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
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
                
                var Zone3NewSource = new GameObject("GenericModZone3NewSource", typeof(LavaSource));
                LAVASOURCE_M_INDEX.SetValue(Zone3NewSource.GetComponent<LavaSource>(), 3);
                LAVASOURCE_M_KIND.SetValue(Zone3NewSource.GetComponent<LavaSource>(), LavaSourceKind.LavaSource);
                Zone3NewSource.GetComponent<LavaSource>().SetSourceActive(true);
                Zone3NewSource.GetComponent<LavaSource>().Save();

                var VolcanoNewSource = new GameObject("GenericModVolcanoNewSource", typeof(LavaSource));
                LAVASOURCE_M_INDEX.SetValue(VolcanoNewSource.GetComponent<LavaSource>(), 4);
                LAVASOURCE_M_KIND.SetValue(VolcanoNewSource.GetComponent<LavaSource>(), LavaSourceKind.VolcanoSource);
                VolcanoNewSource.GetComponent<LavaSource>().SetSourceActive(true);
                VolcanoNewSource.GetComponent<LavaSource>().Save();

                
                foreach (LavaSource source in LavaSource.Instances)
                {
                    if (source.Kind == LavaSourceKind.VolcanoSource)
                    {
                        source.enabled = true;
                        source.SetActive(true);
                        source.SetSourceActive(true);
                    }
                }

                var fStationPrefabs = RuntimeAssetDatabase.Get<ItemDefinition>().FirstOrDefault(s => s.AssetId == REFINERY_STATION)?.Prefabs;
                fStationPrefabs.Append(RuntimeAssetDatabase.Get<ItemDefinition>().FirstOrDefault(s => s.AssetId == REFINERY_HUB)?.Prefabs);
                if (fStationPrefabs != null)
                {
                    foreach (var prefab in fStationPrefabs)
                    {
                        if (prefab.TryGetComponent<FactoryStation>(out var component))
                        {
                            var categories = component.Categories.ToList();
                            categories.Add(RuntimeAssetDatabase.Get<Recipe>().First(s => s.name == "GenericModUnobtainiumIngotRecipe")?.Categories.First());
                            var newList = new ArrayReader<RecipeCategory>(categories.ToArray());
                            PRODUCER_M_CATEGORIES.SetValue(component, newList.ToArray());
                        }
                    }
                }
                var pStationPrefabs = RuntimeAssetDatabase.Get<ItemDefinition>().FirstOrDefault(s => s.AssetId == PRODUCTION_STATION)?.Prefabs;
                pStationPrefabs.Append(RuntimeAssetDatabase.Get<ItemDefinition>().FirstOrDefault(s => s.AssetId == PRODUCTION_HUB)?.Prefabs);
                if (pStationPrefabs != null)
                {
                    foreach (var prefab in pStationPrefabs)
                    {
                        if (prefab.TryGetComponent<FactoryStation>(out var component))
                        {
                            var categories = component.Categories.ToList();
                            categories.Add(RuntimeAssetDatabase.Get<Recipe>().FirstOrDefault(s => s.name == "GenericModUnobtainiumBoltsRecipe")?.Categories.First());
                            var newList = new ArrayReader<RecipeCategory>(categories.ToArray());
                            PRODUCER_M_CATEGORIES.SetValue(component, newList.ToArray());
                        }
                    }
                }
                var rStationPrefabs = RuntimeAssetDatabase.Get<ItemDefinition>().FirstOrDefault(s => s.AssetId == RESEARCH_STATION)?.Prefabs;
                rStationPrefabs.Append(RuntimeAssetDatabase.Get<ItemDefinition>().FirstOrDefault(s => s.AssetId == RESEARCH_HUB)?.Prefabs);
                if (rStationPrefabs != null)
                {
                    foreach (var prefab in rStationPrefabs)
                    {
                        if (prefab.TryGetComponent<FactoryStation>(out var component))
                        {
                            var categories = component.Categories.ToList();
                            categories.Add(RuntimeAssetDatabase.Get<Recipe>().FirstOrDefault(s => s.name == "GenericModIntelOmniRecipe")?.Categories.First());
                            var newList = new ArrayReader<RecipeCategory>(categories.ToArray());
                            PRODUCER_M_CATEGORIES.SetValue(component, newList.ToArray());
                        }
                    }
                }

                //RuntimeAssetDatabase.Get<WeaponReloaderAmmoDefinition>().FirstOrDefault(s => s.AssetId == REVOLVER_RELOADER).Ammunition.Append(RuntimeAssetDatabase.Get<AmmoDefinition>().FirstOrDefault(s => s.name == "GenericModTungstenRevolverAmmo"));
                RuntimeAssetDatabase.Get<ModuleCategory>().FirstOrDefault(s => s.AssetId == PRODUCTION_CATEGORY).Modules.Append(RuntimeAssetDatabase.Get<ItemDefinition>().FirstOrDefault(s => s.name == "GenericModProductionModuleT4"));
                RuntimeAssetDatabase.Get<ModuleCategory>().FirstOrDefault(s => s.AssetId == REFINEMENT_CATEGORY).Modules.Append(RuntimeAssetDatabase.Get<ItemDefinition>().FirstOrDefault(s => s.name == "GenericModRefineryModuleT4"));
                RuntimeAssetDatabase.Get<ModuleCategory>().FirstOrDefault(s => s.AssetId == RESEARCH_CATEGORY).Modules.Append(RuntimeAssetDatabase.Get<ItemDefinition>().FirstOrDefault(s => s.name == "GenericModResearchModuleT4"));
                
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
        private static readonly GUID REVOLVER_RELOADER = GUID.Parse("17d679d30a80ba941a68374092614434");
        private static readonly GUID REFINERY_STATION = GUID.Parse("3b35b8f4f39847945b9881e25bb01f5a");
        private static readonly GUID RESEARCH_STATION = GUID.Parse("a7764724dfb030a47a531f7c5e87ff9e");
        private static readonly GUID PRODUCTION_STATION = GUID.Parse("7c32d187420152f4da3a79d465cbe87a");
        private static readonly GUID REFINERY_HUB = GUID.Parse("d4446b96f5a46494e8bed91cc40c06b7");
        private static readonly GUID RESEARCH_HUB = GUID.Parse("00175574f3d8b8c41b2da96cd19cfc40");
        private static readonly GUID PRODUCTION_HUB = GUID.Parse("ca0964a43824b38468eed492d2385ec4");
        private static readonly GUID PRODUCTION_CATEGORY = GUID.Parse("dc8b5ae383f169340a9b108e643b681f");
        private static readonly GUID REFINEMENT_CATEGORY = GUID.Parse("b7e64936fe9469d4abe811b66f863cd3");
        private static readonly GUID RESEARCH_CATEGORY = GUID.Parse("b425d7e3255eb054999d94d503ac2f04");

        private static readonly FieldInfo PRODUCER_M_CATEGORIES = typeof(Producer).GetField("m_categories", BindingFlags.NonPublic | BindingFlags.Instance);
        private static readonly FieldInfo LAVASOURCE_M_INDEX = typeof(LavaSource).GetField("m_index", BindingFlags.NonPublic | BindingFlags.Instance);
        private static readonly FieldInfo LAVASOURCE_M_KIND = typeof(LavaSource).GetField("m_kind", BindingFlags.NonPublic | BindingFlags.Instance);
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
