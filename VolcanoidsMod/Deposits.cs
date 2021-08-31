using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEngine;

namespace VolcanoidsMod
{
    public class Deposits : MonoBehaviour
    {
        public static void Run()
        {
            depositsurface = Resources.FindObjectsOfTypeAll<DepositLocationSurface>();
            depositunderground = Resources.FindObjectsOfTypeAll<DepositLocationUnderground>();
            CreateDeposit(false, 10, "TungstenOre", 2, 5, "CoalOre");
            CreateDeposit(false, 5, "TungstenOre", 2, 5, "TitaniumOre");
            CreateDeposit(true, 10, "TungstenOre", 2, 5, "CoalOre");
            CreateDeposit(true, 5, "TungstenOre", 2, 5, "TitaniumOre");
        }
        public static void CreateDeposit(bool Underground, int PercentageToReplace, string outputname, float minyield, float maxyield, string ItemToReplace)
        {
            
            if(Underground)
            {
                foreach(DepositLocationUnderground underground in depositunderground)
                {
                    if (UnityEngine.Random.Range(0, 100) <= PercentageToReplace)
                    {
                        if ((ItemToReplace != null && underground.Ore == GetItem(ItemToReplace)) || ItemToReplace == null)
                        {
                            underground.Yield = UnityEngine.Random.Range(minyield, maxyield);
                            OreField.SetValue(underground, GetItem(outputname));
                        }
                    }
                }
            }
            if (!Underground)
            {
                foreach (DepositLocationSurface surface in depositsurface)
                {
                    if (UnityEngine.Random.Range(0, 100) <= PercentageToReplace)
                    {
                        if ((ItemToReplace != null && surface.Ore == GetItem(ItemToReplace)) || ItemToReplace == null)
                        {
                            surface.Yield = UnityEngine.Random.Range(minyield, maxyield);
                            OreField.SetValue(surface, GetItem(outputname));
                        }
                    }
                }
            }
        }
        public static ItemDefinition GetItem(string itemname)
        {
            ItemDefinition item = RuntimeAssetDatabase.Get<ItemDefinition>().FirstOrDefault(s => s.name == itemname);
            if (item == null)
            {
                Debug.LogError("Item is null, name: " + itemname + ". Replacing with NullItem");
                haserror = true;
                return RuntimeAssetDatabase.Get<ItemDefinition>().FirstOrDefault(s => s.name == "NullItem");
            }
            return item;

        }
        static readonly FieldInfo OreField = typeof(DepositLocation).GetField("m_ore", BindingFlags.NonPublic | BindingFlags.Instance);
        private static DepositLocationSurface[] depositsurface;
        private static DepositLocationUnderground[] depositunderground;
        private static bool haserror;
    }
    
}
