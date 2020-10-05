using System.Linq;
using System.Reflection;
using UnityEngine.Sprites;
using UnityEngine;
using UnityEngine.Rendering;
using System.IO;

namespace VolcanoidsMod
{
    public class Items : MonoBehaviour
    {
        private void Awake()
        {
            foreach (CellMaterial cellMaterial in GameResources.Instance.CellMaterials)
            {
                Debug.Log(cellMaterial.name);
            }
            Debug.Log("Module: " + GetType().Name + " loaded successfully");
            // CreateItem("CaptainHead", 1, "Captain", "Come at me", "8010BA2246374E52A9865DEE7A4058BF", "ShipCoreUpgrade3", Sprite2("Captain.png")); 
            CreateItemTracks("TracksT5_Supreme", 18, 62, 1, "Supreme Tracks",
                "Supremely engineered Tracks and efficient Gearbox ratios result in unmatched performance, \r\n " +
                "and allow it to maintain ultimate grip no matter the weight of the drillship", 
                "713A4D41B14346EFB234A548B16BADEC", "TracksT4_Advanced", Sprite2("TracksUpgrade5.png"));
            CreateItemCore("ShipCoreUpgrade4", 400, 1200, 1, "Drillship Core 4", 
                "Specialised steam tubes allow for a higher flow to the modules", 
                "787AE6617DA54690BBD05D89653FF707", "ShipCoreUpgrade3", Sprite2("CoreUpgrade4.png"));
            CreateItemCore("ShipCoreUpgrade5", 800, 2000, 1, "Drillship Core 5", 
                "Highly advanced production technology allows the use of tungsten to further improve the core",
                "2602080730BE43979380D9F381927002", "ShipCoreUpgrade4", Sprite2("CoreUpgrade5.png"));
            CreateItemHull("HullT5_Tungsten", 0, 0.6f, 0.15f, 3410, 1, "Tungsten Hull",
                "Drillship hull made from Tungsten, granting a much higher melting point and a higher damage resistance than Titanium",
                "5A806861DF50469CA38CD13DD4E7598F", "HullT4_Titanium", Sprite2("HullT5.png"));
            CreateItem("TungstenIngot", 10, "Tungsten", 
                "Used in production to produce base components", 
                "DD81B8351C2F47A5B55F9400E2ECA86F", "TitaniumIngot", Sprite2("TungstenIngot.png"));
            CreateItemDrill("DrillT5_Tungsten", 0.9f, 1, "Tungsten Drill",
                "Drillship drill made from Tungsten, completely eliminating the risk of diamond fracture", 
                "652CE5F6CC9544AA94D96D9EEA862C8B", "DrillT4_Diamond", Sprite2("DrillUpgrade5.png"));
            CreateItemDrill("DrillT6_Unobtainium", 0.9999f, 1, "Unobtainium Drill",
                "Drillship drill made from Unobtainium resulting in complete damage protection",
                "BC676806155B4E5BA4190344105A11D4", "DrillT5_Tungsten", Sprite2("DrillUpgrade6.png"));
            CreateItemHull("HullT6_Unobtainium", 0, 0.9999f, 0, 9999, 1, "Unobtainium Hull",
                "Drillship hull made from Unobtainium resulting in complete damage protection", 
                "F84B796D47474DBFA81A6579716807DE", "HullT5_Tungsten", Sprite2("HullT6.png"));
            CreateItem("AlloyT4Ingot", 20, "Alloy T4", 
                "Used in production to produce base components", 
                "6939388A466C45B899EEF83634EEA6C6", "AlloyT3Ingot", Sprite2("AlloyT4.png"));
            CreateItem("UnobtainiumIngot", 10, "Unobtainium",
                "A once hypothetical element has come to life due to the close proximity to the volcano. \r\n" +
                "The use of Unobtainium allows for practically infinite durability and protection", 
                "C84135CD0447417B9668570D5AADF502", "TungstenIngot", Sprite2("PurplishIngot.png"));
            CreateItem("TungstenPlates", 10, "Tungsten plates",
                "Used in production to produce devices",
                "CF9FB96DD9B049C5A88CD1E8195AFE94", "TitaniumPlates", Sprite2("TungstenPlates.png"));
            CreateItem("TungstenBolts", 10, "Tungsten bolts",  
                "Used in production to produce devices",
                "CE39BCA989E84E62AEA54306BCF8F60C", "TungstenPlates", Sprite2("TungstenBolts.png"));  
            CreateItem("TungstenTubes", 10, "Tungsten tubes",
                "Used in production to produce base components",
                "BA7D952FDAA24DFF999682365156F6DB", "TungstenPlates", Sprite2("TungstenTubes.png"));
            CreateItem("UnobtainiumPlates", 10, "Unobtainium plates",
                "Used in production to produce devices",
                "1243872643B54937B0B91ECDD5FCB1B6", "TungstenPlates", Sprite2("PurplishPlates.png"));
            CreateItem("UnobtainiumBolts", 10, "Unobtainium bolts",
                "Used in production to produce devices", 
                "7A296148A35F44639DA94EB165D2BFF7", "UnobtainiumPlates", Sprite2("PurplishBolts.png")); 
            CreateItem("UnobtainiumTubes", 10, "Unobtainium tubes",
                "Used in production to produce base components",
                "7C3BAEF8C42F4A07B10810BC76F73015", "UnobtainiumPlates", Sprite2("PurplishTubes.png"));
            CreateItem("NullItem", 1000, "Null Item", "How did you even actually get this item?", "29B8BE6CAB6E43BB99ED496C06553B0A", "UnobtainiumIngot", Sprite2("Cheese.png"));
            if (GenericMod.Cheese)
            {
                CreateItemEngine("Cheese", 100, 0, 10, "Cheese",
                    "By replacing coal with Cheese, we can enhance the flavor of our steam",
                    GUID.Create().ToString(), "ShipCoreUpgrade4", Sprite2("Cheese.png")); 
            }
            Debug.Log("Module: " + GetType().Name + " Initialized successfully");
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
        public void CreateItemTracks(string codename, int surfacemovementspeed, int undergroundmovementspeed, int maxstack, LocalizedString name, LocalizedString desc, string guidstring, string recipecategoryname, Sprite icon)
        {
            var recipecategory = GameResources.Instance.Items.FirstOrDefault(s => s.name == recipecategoryname);

            var item = ScriptableObject.CreateInstance<TrainTracksItemDefinition>();
            item.name = codename;
            item.Category = recipecategory.Category;
            item.SurfaceMovementSpeed = surfacemovementspeed;
            item.UndergroundMovementSpeed = undergroundmovementspeed;
            item.MaxStack = maxstack;
            item.Icon = icon;
            LocalizedString nameStr = name;
            LocalizedString descStr = desc;
            Initialize(ref nameStr);
            Initialize(ref descStr);

            typeof(ItemDefinition).GetField("m_name", BindingFlags.NonPublic | BindingFlags.Instance).SetValue(item, nameStr);
            typeof(ItemDefinition).GetField("m_description", BindingFlags.NonPublic | BindingFlags.Instance).SetValue(item, descStr);

            var guid = GUID.Parse(guidstring);

            typeof(Definition).GetField("m_assetId", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(item, guid);

            AssetReference[] assets = new AssetReference[] { new AssetReference() { Object = item, Guid = guid, Labels = new string[0] } };
            RuntimeAssetStorage.Add(assets, default);
        }
        public void CreateItemHull(string codename, float damageperdegree, float armorbonus, float temperatureflow, float temperature, int maxstack, LocalizedString name, LocalizedString desc, string guidstring, string recipecategoryname, Sprite icon)
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
            LocalizedString nameStr = name;
            LocalizedString descStr = desc;
            Initialize(ref nameStr);
            Initialize(ref descStr);

            typeof(ItemDefinition).GetField("m_name", BindingFlags.NonPublic | BindingFlags.Instance).SetValue(item, nameStr);
            typeof(ItemDefinition).GetField("m_description", BindingFlags.NonPublic | BindingFlags.Instance).SetValue(item, descStr);

            var guid = GUID.Parse(guidstring);

            typeof(Definition).GetField("m_assetId", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(item, guid);

            AssetReference[] assets = new AssetReference[] { new AssetReference() { Object = item, Guid = guid, Labels = new string[0] } };
            RuntimeAssetStorage.Add(assets, default);
        }
        public void CreateItemDrill(string codename, float armorbonus, int maxstack, LocalizedString name, LocalizedString desc, string guidstring, string recipecategoryname, Sprite icon)
        {
            var recipecategory = GameResources.Instance.Items.FirstOrDefault(s => s.name == recipecategoryname);
            var materials = GameResources.Instance.CellMaterials.ToArray();

            var item = ScriptableObject.CreateInstance<TrainDrillItemDefinition>();
            item.name = codename;
            item.Category = recipecategory.Category;
            item.ArmorBonus = armorbonus;
            item.Materials = materials;
            item.MaxStack = maxstack;
            item.Icon = icon;
            LocalizedString nameStr = name;
            LocalizedString descStr = desc;
            Initialize(ref nameStr);
            Initialize(ref descStr);

            typeof(ItemDefinition).GetField("m_name", BindingFlags.NonPublic | BindingFlags.Instance).SetValue(item, nameStr);
            typeof(ItemDefinition).GetField("m_description", BindingFlags.NonPublic | BindingFlags.Instance).SetValue(item, descStr);

            var guid = GUID.Parse(guidstring);

            typeof(Definition).GetField("m_assetId", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(item, guid);

            AssetReference[] assets = new AssetReference[] { new AssetReference() { Object = item, Guid = guid, Labels = new string[0] } };
            RuntimeAssetStorage.Add(assets, default);
        }
        public void CreateItemEngine(string codename, int segmentcount, int mincoreslotcount, int maxstack, LocalizedString name, LocalizedString desc, string guidstring, string recipecategoryname, Sprite icon)
        {
            var recipecategory = GameResources.Instance.Items.FirstOrDefault(s => s.name == recipecategoryname);

            var item = ScriptableObject.CreateInstance<TrainEngineItemDefinition>();
            item.name = codename;
            item.Category = recipecategory.Category;
            item.SegmentCount = segmentcount;
            item.MinimumCoreSlotCount = mincoreslotcount;
            item.MaxStack = maxstack;
            item.Icon = icon;
            LocalizedString nameStr = name;
            LocalizedString descStr = desc;
            Initialize(ref nameStr);
            Initialize(ref descStr);

            typeof(ItemDefinition).GetField("m_name", BindingFlags.NonPublic | BindingFlags.Instance).SetValue(item, nameStr);
            typeof(ItemDefinition).GetField("m_description", BindingFlags.NonPublic | BindingFlags.Instance).SetValue(item, descStr);

            var guid = GUID.Parse(guidstring);

            typeof(Definition).GetField("m_assetId", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(item, guid);

            AssetReference[] assets = new AssetReference[] { new AssetReference() { Object = item, Guid = guid, Labels = new string[0] } };
            RuntimeAssetStorage.Add(assets, default);
        }
        public void CreateItemCore(string codename, int slotcount, int maxenergy, int maxstack, LocalizedString name, LocalizedString desc, string guidstring, string recipecategoryname, Sprite icon)
        {
            var recipecategory = GameResources.Instance.Items.FirstOrDefault(s => s.name == recipecategoryname);

            var item = ScriptableObject.CreateInstance<TrainCoreItemDefinition>();
            item.name = codename;
            item.Category = recipecategory.Category;
            item.SlotCount = slotcount;
            item.MaxEnergy = maxenergy;
            item.Icon = icon;
            item.MaxStack = maxstack;
            LocalizedString nameStr = name;
            LocalizedString descStr = desc;
            Initialize(ref nameStr);
            Initialize(ref descStr);

            typeof(ItemDefinition).GetField("m_name", BindingFlags.NonPublic | BindingFlags.Instance).SetValue(item, nameStr);
            typeof(ItemDefinition).GetField("m_description", BindingFlags.NonPublic | BindingFlags.Instance).SetValue(item, descStr);

            var guid = GUID.Parse(guidstring);

            typeof(Definition).GetField("m_assetId", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(item, guid);

            AssetReference[] assets = new AssetReference[] { new AssetReference() { Object = item, Guid = guid, Labels = new string[0] } };
            RuntimeAssetStorage.Add(assets, default);
        }
        public void CreateItem(string codename, int maxstack, LocalizedString name, LocalizedString desc, string guidstring, string recipecategoryname, Sprite icon)
        {
            var recipecategory = GameResources.Instance.Items.FirstOrDefault(s => s.name == recipecategoryname);

            var item = ScriptableObject.CreateInstance<ItemDefinition>();
            item.name = codename;
            item.Category = recipecategory.Category;
            item.MaxStack = maxstack;
            item.Icon = icon;
            LocalizedString nameStr = name;
            LocalizedString descStr = desc;
            Initialize(ref nameStr);
            Initialize(ref descStr);

            typeof(ItemDefinition).GetField("m_name", BindingFlags.NonPublic | BindingFlags.Instance).SetValue(item, nameStr);
            typeof(ItemDefinition).GetField("m_description", BindingFlags.NonPublic | BindingFlags.Instance).SetValue(item, descStr);

            var guid = GUID.Parse(guidstring);

            typeof(Definition).GetField("m_assetId", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(item, guid);

            AssetReference[] assets = new AssetReference[] { new AssetReference() { Object = item, Guid = guid, Labels = new string[0] } };
            RuntimeAssetStorage.Add(assets, default);
        }
        
    }
}
