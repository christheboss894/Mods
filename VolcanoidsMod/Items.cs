using System.Linq;
using System.Reflection;
using UnityEngine;
using Volcanoids;
using System.IO;

namespace VolcanoidsMod
{
    public class Items : MonoBehaviour
    {
        public string ModPath;
        private void Awake()
        {
            ModPath = Path.Combine(Application.persistentDataPath, "Mods/GenericMod");
            Debug.Log("Module: " + GetType().Name + " loaded successfully");
            haserror = false;
            /*
            CreateItem("CaptainHead", 1, "Captain",
                "Come at me", 
                "8010BA2246374E52A9865DEE7A4058BF", "ShipCoreUpgrade3", Sprite2("GenericModFiles/Items/Captain.png")); 
            */
            CreateItemTracks("TracksT5_Supreme", 18, 62, 1, "Supreme Tracks",
                "Supremely engineered Tracks and efficient Gearbox ratios result in unmatched performance, \r\n " +
                "and allow it to maintain ultimate grip no matter the weight of the drillship",
                "713A4D41B14346EFB234A548B16BADEC", "TracksT4_Advanced", Sprite2("GenericModFiles/Upgrades/TracksUpgrade5.png"));

            CreateItemCore("ShipCoreUpgrade4", 400, 1200, 1, "Drillship Core 4",
                "Specialised steam tubes allow for a higher flow to the modules",
                "787AE6617DA54690BBD05D89653FF707", "ShipCoreUpgrade3", Sprite2("GenericModFiles/Upgrades/CoreUpgrade4.png"));

            CreateItemCore("ShipCoreUpgrade5", 500, 1600, 1, "Drillship Core 5",
                "Highly advanced production technology allows the use of tungsten to further improve the core",
                "2602080730BE43979380D9F381927002", "ShipCoreUpgrade4", Sprite2("GenericModFiles/Upgrades/CoreUpgrade5.png"));

            CreateItemCore("ShipCoreUpgrade6", 800, 2000, 1, "Drillship Core 6",
                "By implementing Unobtainium into the core, it can sustain a much higher load, \r\n " +
                "aswell as store energy directly in the unobtainium",
                "FD05057E43F64735843E8C52B3DCA447", "ShipCoreUpgrade5", Sprite2("GenericModFiles/Upgrades/CoreUpgrade6.png"));

            CreateItemHull("HullT5_Tungsten", 0, 0.6f, 0.15f, 3410, 1, "Tungsten Hull",
                "Drillship hull made from Tungsten, \r\n " +
                "granting a much higher melting point and damage resistance than Titanium",
                "5A806861DF50469CA38CD13DD4E7598F", "HullT4_Titanium", Sprite2("GenericModFiles/Upgrades/HullT5.png"));

            CreateItem("TungstenIngot", 10, "Tungsten Ingot",
                "Used in production to produce base components",
                "DD81B8351C2F47A5B55F9400E2ECA86F", "TitaniumIngot", Sprite2("GenericModFiles/Resources/TungstenIngot.png"));

            CreateItemDrill("DrillT5_Tungsten", 0.9f, 1, "Tungsten Drill",
                "Drillship drill made from Tungsten, completely eliminating the risk of diamond fracture",
                "652CE5F6CC9544AA94D96D9EEA862C8B", "DrillT4_Diamond", Sprite2("GenericModFiles/Upgrades/DrillUpgrade5.png"));

            CreateItemDrill("DrillT6_Unobtainium", 0.9999f, 1, "Unobtainium Drill",
                "Drillship drill made from Unobtainium resulting in complete damage protection",
                "BC676806155B4E5BA4190344105A11D4", "DrillT5_Tungsten", Sprite2("GenericModFiles/Upgrades/DrillUpgrade6.png"));

            CreateItemHull("HullT6_Unobtainium", 0, 0.9999f, 0, 9999, 1, "Unobtainium Hull",
                "Drillship hull made from Unobtainium resulting in complete damage protection",
                "F84B796D47474DBFA81A6579716807DE", "HullT5_Tungsten", Sprite2("GenericModFiles/Upgrades/HullT6.png"));

            CreateItem("AlloyT4Ingot", 20, "Alloy T4",
                "Used in production to produce base components",
                "6939388A466C45B899EEF83634EEA6C6", "AlloyT3Ingot", Sprite2("GenericModFiles/Resources/AlloyT4.png"));

            CreateItem("UnobtainiumIngot", 10, "Unobtainium Ingot",
                "A once hypothetical element has come to life due to the close proximity to the volcano. \r\n" +
                "The use of Unobtainium allows for practically infinite durability and protection",
                "C84135CD0447417B9668570D5AADF502", "TungstenIngot", Sprite2("GenericModFiles/Resources/PurplishIngot.png"));

            CreateItem("TungstenPlates", 10, "Tungsten plates",
                "Used in production to produce devices",
                "CF9FB96DD9B049C5A88CD1E8195AFE94", "TitaniumPlates", Sprite2("GenericModFiles/Resources/TungstenPlates.png"));

            CreateItem("TungstenBolts", 10, "Tungsten bolts",
                "Used in production to produce devices",
                "CE39BCA989E84E62AEA54306BCF8F60C", "TungstenPlates", Sprite2("GenericModFiles/Resources/TungstenBolts.png"));

            CreateItem("TungstenTubes", 10, "Tungsten tubes",
                "Used in production to produce base components",
                "BA7D952FDAA24DFF999682365156F6DB", "TungstenPlates", Sprite2("GenericModFiles/Resources/TungstenTubes.png"));

            CreateItem("UnobtainiumPlates", 10, "Unobtainium plates",
                "Used in production to produce devices",
                "1243872643B54937B0B91ECDD5FCB1B6", "TungstenPlates", Sprite2("GenericModFiles/Resources/PurplishPlates.png"));

            CreateItem("UnobtainiumBolts", 10, "Unobtainium bolts",
                "Used in production to produce devices",
                "7A296148A35F44639DA94EB165D2BFF7", "UnobtainiumPlates", Sprite2("GenericModFiles/Resources/PurplishBolts.png"));

            CreateItem("UnobtainiumTubes", 10, "Unobtainium tubes",
                "Used in production to produce base components",
                "7C3BAEF8C42F4A07B10810BC76F73015", "UnobtainiumPlates", Sprite2("GenericModFiles/Resources/PurplishTubes.png"));

            CreateItemEngine("EngineUpgrade4", 5, 400, 1, "Engine 4",
                "Utilises the cores specialised steam tubes to provide better flow to the engine",
                "D550C8AA6D6E408385F40C66882B15BA", "EngineUpgrade3", Sprite2("GenericModFiles/Upgrades/EngineUpgrade4.png"));

            CreateItemEngine("EngineUpgrade5", 6, 500, 1, "Engine 5",
                "Using a tungsten titanium alloy allows for the engine to let in more steam and provide more torque",
                "4F0ACF6B26A541A2AF2DB7D7FA50134C", "EngineUpgrade4", Sprite2("GenericModFiles/Upgrades/EngineUpgrade5.png"));

            CreateItem("TungstenOre", 20, "Tungsten Ore",
                "Tungsten Ore",
                "27144EB886144A0C84AEA4FEE5026D73", "TitaniumOre", Sprite2("GenericModFiles/Resources/TungstenOre.png"));

            CreateItem("NullItem", 1000, "Null Item",
                "This item is to indicate that an item is null",
                "29B8BE6CAB6E43BB99ED496C06553B0A", "UnobtainiumIngot", Sprite2("GenericModFiles/Items/Cheese.png"));

            CreateItem("Volcanite", 20, "Volcanite Ingot",
                "An ingot made by refining Volcanite Ore, has many uses",
                "864DF411103A4CF49B4407BCE047222C", "UnobtainiumIngot", Sprite2("GenericModFiles/Items/Cheese.png"));

            /*
            CreateItem("VolcaniteOre", 20, "Volcanite Ore",
               "This ore is made by the mixing of all known ores in the heart of a volcano",
                "934D053FC556405BB7876294B0F5500D", "UnobtainiumIngot", Sprite2("GenericModFiles/Items/Cheese.png"));

            CreateItem("Leek", 1, "Leek", 
                "Oh no, devs will be mad about you leeking this", 
                "33AA3296DE0342888497AFC837AE8E62", "CopperIngot", Sprite2("GenericModFiles/Items/Leek.png"));
            

            CreateItemArmor("TungstenArmor", "TungstenArmorDef", 
                "E26571F2E79A45F5AF7099FFE443CFD7", 800f, 4, 5, 
                "Tungsten Armor", "Cheese", "E64CF8A06C06401796B27CF25B3AFDD5", 
                "TitanArmor", Sprite2("GenericModFiles/Items/Cheese.png"));
            */
            if (GenericMod.Cheese)
            {
                CreateItemEngine("EngineUpgrade6", 100, 501, 1, "Cheese",
                    "By replacing coal with Cheese, we can enhance the flavor of our steam",
                    "C58EA469639340BF99A89F29BF434386", "EngineUpgrade5", Sprite2("GenericModFiles/Items/Cheese.png"));
            }
            if (GenericMod.InfiniteInventory)
            {
                foreach (ItemDefinition item in GameResources.Instance.Items)
                {
                    item.MaxStack = 999;
                }
            }
            if (haserror)
            {
                Debug.LogError("Module: " + GetType().Name + " Initialized with error");
            }
            else
            {
                Debug.Log("Module: " + GetType().Name + " Initialized successfully");
            }
        }
        private static void Initialize<T>(ref T str)
    where T : struct, ISerializationCallbackReceiver
        {
            str.OnAfterDeserialize();
        }
        public Sprite Sprite2(string iconpath)
        {
            var path = System.IO.Path.Combine(ModPath, iconpath);
            if (!File.Exists(path))
            {
                Debug.LogError("Specified Icon path not found: " + path);
                haserror = true;
                return null;
            }
            var bytes = File.ReadAllBytes(path);


            var texture = new Texture2D(512, 512, TextureFormat.ARGB32, true);
            texture.LoadImage(bytes);

            var sprite = Sprite.Create(texture, new Rect(Vector2.zero, new Vector2(texture.width, texture.height)), new Vector2(0.5f, 0.5f), texture.width, 0, SpriteMeshType.FullRect, Vector4.zero, false);
            return sprite;
        }
        public ArmorDefinition CreateArmor(string codename, string guidarmorstring, float maxhp, string armorinheritname)
        {
            var armorinherit = GameResources.Instance.Items.FirstOrDefault(s => s.name == armorinheritname);
            var oldarmordef = armorinherit.ItemPrefab.GetComponent<Armor>().Definition;
            var armor = ScriptableObject.CreateInstance<ArmorDefinition>();
            armor.name = codename;
            armor.MaxHP = maxhp;
            armor.Material = oldarmordef.Material;
            armor.HandsSkin = oldarmordef.HandsSkin;
            armor.CharacterSkin = oldarmordef.CharacterSkin;
            armor.DestroyedSkin = oldarmordef.DestroyedSkin;
            armor.DamagedCharacterMaterial = oldarmordef.DamagedCharacterMaterial;
            armor.DamagedHandsMaterial = oldarmordef.DamagedHandsMaterial;
            armor.DamageThreshold = oldarmordef.DamageThreshold;

            var guidarmor = GUID.Parse(guidarmorstring);

            typeof(Definition).GetField("m_assetId", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(armor, guidarmor);

            AssetReference[] assets = new AssetReference[] { new AssetReference() { Object = armor, Guid = guidarmor, Labels = new string[0] } };
            RuntimeAssetStorage.Add(assets);

            return armor;
        }
        public void CreateItemArmor(string codename, string armorcodename, string armorguidstring, float maxhp, float armorlevel, int maxstack, string name, string desc, string guidstring, string recipecategoryname, Sprite icon)
        {
            var recipecategory = GameResources.Instance.Items.FirstOrDefault(s => s.name == recipecategoryname);

            var item = ScriptableObject.CreateInstance<ItemDefinition>();
            item.name = codename;
            item.Category = recipecategory.Category;
            item.MaxStack = maxstack;
            item.ItemPrefab.AddComponent<Armor>().SetArmor(CreateArmor(armorcodename, armorguidstring, maxhp, recipecategoryname), armorlevel);
            item.Icon = icon;
            item.NameLocalization = new LocalizedString(".", name, null, name);
            item.DescriptionLocalization = new LocalizedString(".", desc, null, desc);

            var guid = GUID.Parse(guidstring);

            typeof(Definition).GetField("m_assetId", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(item, guid);

            AssetReference[] assets = new AssetReference[] { new AssetReference() { Object = item, Guid = guid, Labels = new string[0] } };
            RuntimeAssetStorage.Add(assets);
        }
        public void CreateItemTracks(string codename, int surfacemovementspeed, int undergroundmovementspeed, int maxstack, string name, string desc, string guidstring, string recipecategoryname, Sprite icon)
        {
            var recipecategory = GameResources.Instance.Items.FirstOrDefault(s => s.name == recipecategoryname);

            var item = ScriptableObject.CreateInstance<TrainTracksItemDefinition>();
            item.name = codename;
            item.Category = recipecategory.Category;
            item.SurfaceMovementSpeed = surfacemovementspeed;
            item.UndergroundMovementSpeed = undergroundmovementspeed;
            item.MaxStack = maxstack;
            item.Icon = icon;
            item.NameLocalization = new LocalizedString(".", name, null, name);
            item.DescriptionLocalization = new LocalizedString(".", desc, null, desc);

            var guid = GUID.Parse(guidstring);

            typeof(Definition).GetField("m_assetId", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(item, guid);

            AssetReference[] assets = new AssetReference[] { new AssetReference() { Object = item, Guid = guid, Labels = new string[0] } };
            RuntimeAssetStorage.Add(assets);
        }
        public void CreateItemHull(string codename, float damageperdegree, float armorbonus, float temperatureflow, float temperature, int maxstack, string name, string desc, string guidstring, string recipecategoryname, Sprite icon)
        {
            var recipecategory = GameResources.Instance.Items.FirstOrDefault(s => s.name == recipecategoryname);

            var item = ScriptableObject.CreateInstance<TrainHullItemDefinition>();
            item.name = codename;
            item.Category = recipecategory.Category;
            item.Temperature = temperature;
            item.TemperatureFlow = temperatureflow;
            item.ArmorBonus = armorbonus;
            item.DamagePerDegree = damageperdegree;
            item.MaxStack = maxstack;
            item.Icon = icon;
            item.NameLocalization = new LocalizedString(".", name, null, name);
            item.DescriptionLocalization = new LocalizedString(".", desc, null, desc);

            var guid = GUID.Parse(guidstring);

            typeof(Definition).GetField("m_assetId", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(item, guid);

            AssetReference[] assets = new AssetReference[] { new AssetReference() { Object = item, Guid = guid, Labels = new string[0] } };
            RuntimeAssetStorage.Add(assets);
        }
        public void CreateItemDrill(string codename, float armorbonus, int maxstack, string name, string desc, string guidstring, string recipecategoryname, Sprite icon)
        {
            var recipecategory = GameResources.Instance.Items.FirstOrDefault(s => s.name == recipecategoryname);
            var materials = GameResources.Instance.CellMaterials.ToArray();

            var item = ScriptableObject.CreateInstance<TrainDrillItemDefinition>();
            item.name = codename;
            item.Category = recipecategory.Category;
            item.ArmorBonus = armorbonus;
            item.Materials = materials;
            item.MaxStack = maxstack;
            var recipecategorydrill = GameResources.Instance.Items.FirstOrDefault(s => s.name == recipecategoryname) as TrainDrillItemDefinition;
            item.DrillPrefabs = recipecategorydrill.DrillPrefabs;
            item.DrillPrefabTrashes = recipecategorydrill.DrillPrefabTrashes;
            item.Icon = icon;
            item.NameLocalization = new LocalizedString(".", name, null, name);
            item.DescriptionLocalization = new LocalizedString(".", desc, null, desc);


            var guid = GUID.Parse(guidstring);

            typeof(Definition).GetField("m_assetId", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(item, guid);

            AssetReference[] assets = new AssetReference[] { new AssetReference() { Object = item, Guid = guid, Labels = new string[0] } };
            RuntimeAssetStorage.Add(assets);
        }
        public void CreateItemEngine(string codename, int segmentcount, int mincoreslotcount, int maxstack, string name, string desc, string guidstring, string recipecategoryname, Sprite icon)
        {
            var recipecategory = GameResources.Instance.Items.FirstOrDefault(s => s.name == recipecategoryname);

            var item = ScriptableObject.CreateInstance<TrainEngineItemDefinition>();
            item.name = codename;
            item.Category = recipecategory.Category;
            item.SegmentCount = segmentcount;
            item.MinimumCoreSlotCount = mincoreslotcount;
            item.MaxStack = maxstack;
            item.Icon = icon;
            item.NameLocalization = new LocalizedString(".", name, null, name);
            item.DescriptionLocalization = new LocalizedString(".", desc, null, desc);


            var guid = GUID.Parse(guidstring);

            typeof(Definition).GetField("m_assetId", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(item, guid);

            AssetReference[] assets = new AssetReference[] { new AssetReference() { Object = item, Guid = guid, Labels = new string[0] } };
            RuntimeAssetStorage.Add(assets);
        }
        public void CreateItemCore(string codename, int slotcount, int maxenergy, int maxstack, string name, string desc, string guidstring, string recipecategoryname, Sprite icon)
        {
            var recipecategory = GameResources.Instance.Items.FirstOrDefault(s => s.name == recipecategoryname);

            var item = ScriptableObject.CreateInstance<TrainCoreItemDefinition>();
            item.name = codename;
            item.Category = recipecategory.Category;
            item.SlotCount = slotcount;
            item.MaxEnergy = maxenergy;
            item.Icon = icon;
            item.MaxStack = maxstack;
            item.NameLocalization = new LocalizedString(".", name, null, name);
            item.DescriptionLocalization = new LocalizedString(".", desc, null, desc);

            var guid = GUID.Parse(guidstring);

            typeof(Definition).GetField("m_assetId", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(item, guid);

            AssetReference[] assets = new AssetReference[] { new AssetReference() { Object = item, Guid = guid, Labels = new string[0] } };
            RuntimeAssetStorage.Add(assets);
        }
        public void CreateItem(string codename, int maxstack, string name, string desc, string guidstring, string recipecategoryname, Sprite icon)
        {
            var recipecategory = GameResources.Instance.Items.FirstOrDefault(s => s.name == recipecategoryname);

            var item = ScriptableObject.CreateInstance<ItemDefinition>();
            item.name = codename;
            item.Category = recipecategory.Category;
            item.MaxStack = maxstack;
            item.Icon = icon;
            item.NameLocalization = new LocalizedString(".", name, null, name);
            item.DescriptionLocalization = new LocalizedString(".", desc, null, desc);


            var guid = GUID.Parse(guidstring);

            typeof(Definition).GetField("m_assetId", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(item, guid);

            AssetReference[] assets = new AssetReference[] { new AssetReference() { Object = item, Guid = guid, Labels = new string[0] } };
            RuntimeAssetStorage.Add(assets);
        }
        private bool haserror;
    }
}
