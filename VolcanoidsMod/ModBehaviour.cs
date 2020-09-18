using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace VolcanoidsMod
{
    // Token: 0x02000002 RID: 2
    internal class ModBehaviour : MonoBehaviour
    {
        // Token: 0x06000001 RID: 1 RVA: 0x00002050 File Offset: 0x00000250
        private void Awake()
        {
            var a = "m";var h = " ";var i = "L";var c = "d";var d = "e";var j = "i";var k = "n";var e = " ";var b = "a";var f = "b";var g = "y";var z = "u";var l = "s";Debug.Log(a + b + c + d + e + f + g + h + i + j + k + z + l);
            Debug.Log("Its up and runnin Chief");
        }
        private void Update()
        {
            if (Player.Local.TryGetComponent(out TrainCrewMember member) && member.PrimaryDrillship != null)
            {
                trainUpgrades = member.PrimaryDrillship.GetComponent<TrainUpgrades>();
                train = member.PrimaryDrillship.GetComponent<Train>();
                engines = GameResources.Instance.Items.OfType<TrainEngineItemDefinition>();
                cores = GameResources.Instance.Items.OfType<TrainCoreItemDefinition>();
                tracks = GameResources.Instance.Items.OfType<TrainTracksItemDefinition>();
                drills = GameResources.Instance.Items.OfType<TrainDrillItemDefinition>();
                hulls = GameResources.Instance.Items.OfType<TrainHullItemDefinition>();
                segments = GameResources.Instance.Items.OfType<TrainSegmentDefinition>();
            }
            else
            {
                return;
            }
            foreach (TrainEngineItemDefinition engine in engines)
            {
                if (engine.name == "EngineUpgrade1")
                {
                    // Increases max segment to 2 for Tier 1 Engine
                    engine.SegmentCount = 2;
                }
                if (engine.name == "EngineUpgrade2")
                {
                    // Increases max segment to 4 for Tier 2 Engine
                    engine.SegmentCount = 4;
                }
                if (engine.name == "EngineUpgrade3")
                {
                    // Increases max segment to 7 for Tier 3 Engine
                    engine.SegmentCount = 7;
                }
            }
        }
        TrainUpgrades trainUpgrades;
        Train train;
        private IEnumerable<TrainEngineItemDefinition> engines;
        private IEnumerable<TrainCoreItemDefinition> cores;
        private IEnumerable<TrainTracksItemDefinition> tracks;
        private IEnumerable<TrainDrillItemDefinition> drills;
        private IEnumerable<TrainHullItemDefinition> hulls;
        private IEnumerable<TrainSegmentDefinition> segments;
    }
}
