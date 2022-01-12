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

            //Aboveground
            CreateDeposit(false, 10, "GenericModTungstenOre", 5, 12, "CoalOre");
            CreateDeposit(false, 5, "GenericModTungstenOre", 6, 16, "TitaniumOre");
            CreateDeposit(false, 5, "GenericModVolcaniteOre", 1, 5, "IronOre");
            CreateDeposit(false, 3, "GenericModVolcaniteOre", 1, 3, "CoalOre");

            //Underground
            CreateDeposit(true, 10, "GenericModTungstenOre", 5, 12, "CoalOre");
            CreateDeposit(true, 5, "GenericModTungstenOre", 6, 16, "TitaniumOre");
            CreateDeposit(true, 7, "GenericModVolcaniteOre", 1, 5, "IronOre");
            CreateDeposit(true, 5, "GenericModVolcaniteOre", 1, 3, "CoalOre");

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
                return RuntimeAssetDatabase.Get<ItemDefinition>().FirstOrDefault(s => s.name == "GenericModNullItem");
            }
            return item;

        }
        static readonly FieldInfo OreField = typeof(DepositLocation).GetField("m_ore", BindingFlags.NonPublic | BindingFlags.Instance);
        private static DepositLocationSurface[] depositsurface;
        private static DepositLocationUnderground[] depositunderground;
    }
    
}
