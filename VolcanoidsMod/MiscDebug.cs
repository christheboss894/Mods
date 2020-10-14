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
            foreach(FactoryType factorytype in GameResources.Instance.FactoryTypes)
            {
                Debug.Log(factorytype.name);
            }
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
