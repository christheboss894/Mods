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
            CreateItemModuleTurret("TurretImproved", 5,
                "Partially Upgraded Turret", "This turret has been modified to increase barrel pressure",
                "392E44970E284FC38C112B79FB60BC13",
                "TurretModule", 
                Sprite2("TurretImproved.png"),
                2, 3, 2, 2, 2); 
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
        public void CreateItemModuleTurret(string codename, int maxstack, LocalizedString name, LocalizedString desc, string guidstring, string categoryname, Sprite icon, int aimspeed, int damagemultiplier, int rangemultiplier, int rateoffiremultiplier, int effectiverangemultiplier)
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
            var reloader = GetComponentInChildren<WeaponReloaderNoAmmo>();
            var ammoStats = reloader.LoadedAmmo;
            turretComponent.m_pitchSpeed *= aimspeed;
            turretComponent.m_yawSpeed *= aimspeed;
            ammoStats.Damage *= damagemultiplier;
            ammoStats.MaximumRange *= rangemultiplier;
            ammoStats.RateOfFire *= rateoffiremultiplier;
            ammoStats.EffectiveRange *= effectiverangemultiplier;

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
    }
}
