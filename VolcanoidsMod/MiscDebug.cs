using UnityEngine;
using System.Collections;
using System.Linq;
using System.Collections.Generic;

namespace VolcanoidsMod
{
    public class MiscDebug : MonoBehaviour
    {
        private void Awake()
        {
            Debug.Log("Module: " + GetType().Name + " loaded successfully");
            var olditem = GameResources.Instance.Items.FirstOrDefault(s => s.name == "ProductionModuleT3");
            var olditem2 = olditem.Prefabs[0];
            var olditem3 = olditem2.GetComponentInChildren<ProductionModule>();
            var olditem4 = olditem2.GetComponent<GridModule>();
            Debug.Log(olditem3.FactoryType.name);
            foreach(RecipeCategory category in olditem3.Categories)
            {
                Debug.Log(category.name);
            }
            Debug.Log(olditem4.VariantName);
            Debug.Log("Module: " + GetType().Name + " Initialized successfully");
        }
        /*private void Update()
        {
            tracks = GameResources.Instance.Items.OfType<TrainTracksItemDefinition>();
            foreach(TrainTracksItemDefinition track in tracks)
            {
                if (name == "TracksT5_Supreme")
                {
                    Debug.Log()
                }
            }
        }
        private IEnumerable<TrainTracksItemDefinition> tracks;
        */
    }
}
