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
            Debug.Log("Module: " + GetType().Name + " loaded successfully");
            CreateItemTracks("TracksT5_Supreme", 18, 62, 1, "Supreme Tracks",
                "Supremely engineered Tracks and efficient Gearbox ratios result in unmatched performance, \r\n " +
                "and allow it to maintain ultimate grip no matter the weight of the drillship", 
                "713A4D41B14346EFB234A548B16BADEC", "TracksT4_Advanced", Sprite2("TracksUpgrade5.png"));
            CreateItemCore("ShipCoreUpgrade4", 400, 1200, 1, "Drillship Core 4", 
                "Specialised steam tubes allow for a higher flow to the modules", 
                "787AE6617DA54690BBD05D89653FF707", "ShipCoreUpgrade3", Sprite2("CoreUpgrade4.png"));
            CreateItemCore("ShipCoreUpgrade5", 800, 2000, 1, "Drillship Core 5", 
                "Highly advanced production technology allows the use of a titanium - iron alloy to further improve the core",
                "2602080730BE43979380D9F381927002", "ShipCoreUpgrade4", Sprite2("CoreUpgrade5.png"));
            // CreateItemHull
            if (GenericMod.Cheese)
            {
                CreateItemEngine("Cheese", 100, 0, 10, "Cheese",
                    "By replacing coal with Cheese, we can enhance the flavor of our steam",
                    GUID.Create().ToString(), "ShipCoreUpgrade4", Sprite2("Cheese.png"));
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
        public void CreateItemDrill(string codename, CellMaterial[] materials, float armorbonus, int maxstack, LocalizedString name, LocalizedString desc, string guidstring, string recipecategoryname, Sprite icon)
        {
            var recipecategory = GameResources.Instance.Items.FirstOrDefault(s => s.name == recipecategoryname);

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
