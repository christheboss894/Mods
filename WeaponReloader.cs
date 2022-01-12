using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEngine;

namespace Assets.GenericMod_Release
{
    [CreateAssetMenu(menuName = "GenericMod/ReloaderAmmoAsset")]
    public class WeaponReloader : ModCallbackAsset
    {
        public WeaponReloaderAmmoDefinition weaponReloader;
        public AmmoDefinition ammo;
        AmmoDefinition[] backup;

        public override void Init()
        {
            backup = weaponReloader.Ammunition;
            ContentPatch.Push(OnRevert);
            List<AmmoDefinition> list = weaponReloader.Ammunition.ToList();
            list.Add(ammo);
            weaponReloader.Ammunition = list.ToArray();
        }
        private void OnRevert()
        {
            weaponReloader.Ammunition = backup;
        }
    }
}
