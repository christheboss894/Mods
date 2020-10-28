using UnityEngine;
using System;
using System.Linq;
using System.IO;
using System.Reflection;

namespace VolcanoidsMod
{
    public class Modules : MonoBehaviour
    {
        private void Awake()
        {
            Debug.Log("Module: " + GetType().Name + " loaded successfully");
            haserror = false;
            /*
            CreateItemModuleTurret("TurretImproved", "UpgradedTurret", 5,
                "Partially Upgraded Turret", "This turret has been modified to increase barrel pressure",
                "392E44970E284FC38C112B79FB60BC13",
                "TurretModule", 
                Sprite2("GenericModFiles/Items/TurretImproved.png"),
                2, 3, 2, 2, 2);
            CreateItemModuleProduction("Omni-ModuleT1", "Tier1OmniModule", 1,
                "Omni-Module", "This module acts as an all in one module, \r\n " +
                "it can be a refinery, it can do research, it can even produce things", 
                "22B3DFEFECC94F48AA30638113CA2C77", 
                "ProductionModuleT3", "Refinement", true, Sprite2("GenericModFiles/Items/OmniModuleT1.png"),
                new RecipeCategory[] {
                    Findcategories("ProductionTier1"),
                    Findcategories("ProductionTier2"),
                    Findcategories("ProductionTier3"), 
                    Findcategories("ProductionTierSubmarine"), 
                    Findcategories("RefinementTier1"), 
                    Findcategories("RefinementTier2"), 
                    Findcategories("RefinementTier3"), 
                    Findcategories("ResearchTier1"), 
                    Findcategories("ResearchTier2"), 
                    Findcategories("ResearchTier3"), 
                    Findcategories("ScrapTier1"), 
                    Findcategories("ScrapTier2"), 
                    Findcategories("ScrapTier3") 
                });
            */
            if (haserror)
            {
                Debug.LogError("Module: " + GetType().Name + " Initialized with error");
            }
            else
            {
                Debug.Log("Module: " + GetType().Name + " Initialized successfully");
            }
        }
        public static void Initialize<T>(ref T str)
    where T : struct, ISerializationCallbackReceiver
        {
            str.OnAfterDeserialize();
        }
        public Sprite Sprite2(string iconpath)
        {
            var path = System.IO.Path.Combine(Application.persistentDataPath, "Mods", iconpath);
            var bytes = File.ReadAllBytes(path);


            var texture = new Texture2D(512, 512, TextureFormat.ARGB32, true);
            texture.LoadImage(bytes);

            var sprite = Sprite.Create(texture, new Rect(Vector2.zero, Vector2.one * texture.width), new Vector2(0.5f, 0.5f), texture.width, 0, SpriteMeshType.FullRect, Vector4.zero, false);
            return sprite;
        }
        public void CreateItemModuleTurret(string codename, string variantname, int maxstack, LocalizedString name, LocalizedString desc, string guidstring, string categoryname, Sprite icon, int aimspeed, int damagemultiplier, int rangemultiplier, int rateoffiremultiplier, int effectiverangemultiplier)
        {
            var category = GameResources.Instance.Items.FirstOrDefault(s => s.name == categoryname).Category;
            var item = ScriptableObject.CreateInstance<ItemDefinition>();
            item.name = codename;
            item.Category = category;
            item.MaxStack = maxstack;
            item.Icon = icon;
            var prefabParent = new GameObject();
            var olditem = GameResources.Instance.Items.FirstOrDefault(s => s.name == "TurretModule");
            prefabParent.SetActive(false);
            var turretStrong = Instantiate(olditem.Prefabs[0], prefabParent.transform);
            var turretComponent = turretStrong.GetComponentInChildren<Turret>();
            var turretWeapon = turretStrong.GetComponentInChildren<Weapon>();
            var reloader = turretStrong.GetComponentInChildren<WeaponReloaderNoAmmo>();
            var ammoStats = reloader.LoadedAmmo;
            turretComponent.m_pitchSpeed *= aimspeed;
            turretComponent.m_yawSpeed *= aimspeed;
            ammoStats.Damage *= damagemultiplier;
            ammoStats.MaximumRange *= rangemultiplier;
            ammoStats.RateOfFire *= rateoffiremultiplier;
            ammoStats.EffectiveRange *= effectiverangemultiplier;
            turretStrong.GetComponent<GridModule>().VariantName = variantname;
            turretStrong.GetComponent<GridModule>().Item = item;
            item.Prefabs = new GameObject[] { turretStrong };


            LocalizedString nameStr = name;
            LocalizedString descStr = desc;
            Initialize(ref nameStr);
            Initialize(ref descStr);

            typeof(WeaponReloaderNoAmmo).GetField("m_stats", BindingFlags.NonPublic | BindingFlags.Instance).SetValue(reloader, ammoStats);
            typeof(ItemDefinition).GetField("m_name", BindingFlags.NonPublic | BindingFlags.Instance).SetValue(item, nameStr);
            typeof(ItemDefinition).GetField("m_description", BindingFlags.NonPublic | BindingFlags.Instance).SetValue(item, descStr);

            var guid = GUID.Parse(guidstring);

            typeof(Definition).GetField("m_assetId", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(item, guid);

            AssetReference[] assets = new AssetReference[] { new AssetReference() { Object = item, Guid = guid, Labels = new string[0] } };
            RuntimeAssetStorage.Add(assets, default);
        }
        public RecipeCategory Findcategories(string categoryname)
        {
            tempcategory = null;
            foreach(Recipe recipe in GameResources.Instance.Recipes)
            {
                foreach(RecipeCategory category in recipe.Categories)
                {
                    if (category.name == categoryname)
                    {
                        tempcategory = category;
                    }
                }
            }
            return tempcategory;
        }
        public void CreateItemModuleProduction(string codename, string variantname, int maxstack, LocalizedString name, LocalizedString desc, string guidstring, string categoryname, string factorytypename, bool omni, Sprite icon, RecipeCategory[] categories)
        {
            var category = GameResources.Instance.Items.FirstOrDefault(s => s.name == categoryname).Category;
            var item = ScriptableObject.CreateInstance<ItemDefinition>();
            item.name = codename;
            item.Category = category;
            item.MaxStack = maxstack;
            item.Icon = icon;
            var prefabParent = new GameObject();
            var olditem = GameResources.Instance.Items.FirstOrDefault(s => s.name == "ProductionModuleT3");
            var factorytype = GameResources.Instance.FactoryTypes.FirstOrDefault(s => s.name == factorytypename);
            prefabParent.SetActive(false);
            var newmodule = Instantiate(olditem.Prefabs[0], prefabParent.transform);
            var module = newmodule.GetComponentInChildren<ProductionModule>();
            var gridmodule = newmodule.GetComponent<GridModule>();
            gridmodule.VariantName = variantname;
            gridmodule.Item = item;
            item.Prefabs = new GameObject[] { newmodule };
            if (omni)
            {
                var modulecategory = RuntimeAssetCacheLookup.Get<ModuleCategory>().First(s => s.name == "Packable");
                modulecategory.Modules = modulecategory.Modules.Concat(new ItemDefinition[] { item }).ToArray();
            }
            if (!omni)
            {
                var modulecategory = RuntimeAssetCacheLookup.Get<ModuleCategory>().First(s => s.name == factorytypename);
                modulecategory.Modules = modulecategory.Modules.Concat(new ItemDefinition[] { item }).ToArray();
            }
            LocalizedString nameStr = name;
            LocalizedString descStr = desc;
            Initialize(ref nameStr);
            Initialize(ref descStr);

            typeof(ProductionModule).GetField("m_factoryType", BindingFlags.NonPublic | BindingFlags.Instance).SetValue(module, factorytype);
            typeof(ProductionModule).GetField("m_module", BindingFlags.NonPublic | BindingFlags.Instance).SetValue(module, gridmodule);
            typeof(ProductionModule).GetField("m_categories", BindingFlags.NonPublic | BindingFlags.Instance).SetValue(module, categories);
            typeof(ItemDefinition).GetField("m_name", BindingFlags.NonPublic | BindingFlags.Instance).SetValue(item, nameStr);
            typeof(ItemDefinition).GetField("m_description", BindingFlags.NonPublic | BindingFlags.Instance).SetValue(item, descStr);

            var guid = GUID.Parse(guidstring);

            typeof(Definition).GetField("m_assetId", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(item, guid);

            AssetReference[] assets = new AssetReference[] { new AssetReference() { Object = item, Guid = guid, Labels = new string[0] } };
            RuntimeAssetStorage.Add(assets, default);
        }

        private bool haserror;

        private RecipeCategory[] recipecategories = null;

        private RecipeCategory tempcategory;
    }
}
