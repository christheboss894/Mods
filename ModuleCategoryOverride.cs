using System;
using System.Collections;
using System.Reflection;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.GenericMod_Release
{
    [CreateAssetMenu(menuName = "GenericMod/ModuleCategoryOverrideAsset")]
    public class ModuleCategoryOverride : ModCallbackAsset
    {
        public ItemDefinition[] Modules;
        ItemDefinition[] backup;
        public ModuleCategory Category;

        public override void Init()
        {
            backup = Category.Modules;
            List<ItemDefinition> list = Category.Modules.ToList();
            foreach(ItemDefinition module in Modules)
            {
                list.Add(module);
            }
            Category.Modules = list.ToArray();
            ContentPatch.Push(OnRevert);
        }

        private void OnRevert()
        {
            Category.Modules = backup;
        }
    }
}