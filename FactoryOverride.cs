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
    [CreateAssetMenu(menuName = "GenericMod/FactoryOverrideAsset")]
    public class FactoryOverride : ModCallbackAsset
    {
        public RecipeCategory[] Category;
        RecipeCategory[] backup;
        public GameObject[] Stations;

        public override void Init()
        {

            var StationPrefabs = Stations;
            if (StationPrefabs != null)
            {
                foreach (var prefab in StationPrefabs)
                {
                    if (prefab.TryGetComponent<FactoryStation>(out var component))
                    {
                        foreach (RecipeCategory category in Category)
                        {
                            var categories = component.Categories.ToList();
                            backup = component.Categories.ToArray();
                            categories.Add(category);
                            var newList = new ArrayReader<RecipeCategory>(categories.ToArray());
                            PRODUCER_M_CATEGORIES.SetValue(component, newList.ToArray());
                            
                        }
                        ContentPatch.Push(OnRevert);
                    }

                }
            }
        }
        
        private void OnRevert()
        {
            var StationPrefabs = Stations;
            foreach (var prefab in StationPrefabs)
            {
                if (prefab.TryGetComponent<FactoryStation>(out var component))
                {
                    PRODUCER_M_CATEGORIES.SetValue(component, backup);
                }
            }
        }
        private static readonly FieldInfo PRODUCER_M_CATEGORIES = typeof(Producer).GetField("m_categories", BindingFlags.NonPublic | BindingFlags.Instance);
    }
}
