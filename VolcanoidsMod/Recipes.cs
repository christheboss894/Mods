using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEngine;

namespace VolcanoidsMod
{
    public class Recipes : MonoBehaviour
    {
        private void Awake()
        {
            Debug.Log("Module: " + GetType().Name + " loaded successfully");
            CreateRecipeSimple("CoalOre", 1, "AlloyT1Ingot", 10, "B01B65E123DB438D98D1AB7DED917DE5", "AlloyT2Ingot", 1, "AlloyT2Recipe", "AlloyT1toAlloyT3Recipe", 3f);
            CreateRecipeSimple("CoalOre", 4, "AlloyT2Ingot", 10, "0E98FAF0FA9449338428C7DC771E42F6", "AlloyT3Ingot", 1, "AlloyT3Recipe", "AlloyT2toAlloyT3Recipe", 3f);
            CreateRecipeSimple("CoalOre", 15, "CopperOre", 10, "0B7717FC2854467FA3D23347E3B9DCFB", "CopperIngot", 10, "CopperIngotRecipe", "10xCopperSmeltingRecipe", 5f);
            CreateRecipeSimple("CoalOre", 20, "CopperOre", 20, "B98720D4DB54470B82DBC2D1B4D7C8C4", "CopperIngot", 20, "CopperIngotRecipe", "20xCopperSmeltingRecipe", 10f);
            CreateRecipeSimple("AlloyT3Ingot", 40, "IntelResearchT3", 5, "IntelRefineryT3", 5, "IntelProductionT3", 5, "EABEBE7067F44B228BDEBAC23548AE47", "TracksT5_Supreme", 1, "TracksUpgrade4Recipe", "TracksT5Recipe", 2f);
            // CreateRecipeSimple("AlloyT2Ingot", 10, "IntelProductionT2", 1, "C6A85ACFE1504FAFB437A89553B84144", "TurretImproved", 1, "TurretModuleRecipe", "TurretImprovedRecipe", 1.5f);
            CreateRecipeSimple("TitaniumTubes", 4, "TitaniumPlates", 2, "Diamond", 6, "IntelProductionT2", 4, "23CA71CB89644644881DC484A1879D0D", "ShipCoreUpgrade4", 1, "ShipCoreUpgrade3Recipe", "ShipCoreUpgrade4Recipe", 2f);
            CreateRecipeSimple("AlloyT3Ingot", 20, "IntelRefineryT3", 4, "IntelProductionT3", 4, "D7415C76EB4A438C86432501D59998E0", "ShipCoreUpgrade5", 1, "ShipCoreUpgrade4Recipe", "ShipCoreUpgrade5Recipe", 2f); 

            if (GenericMod.InfiniteInventory)
            {
                CreateRecipeSimple("CopperOre", 1, "2B2F51C0DCA6446681867FB440B15472", "CoalOre", 50, "CopperIngotRecipe", "InfiniteInventoryRecipe", 0.01f);
                var InfiniteInventoryRecipe = GetRecipe("InfiniteInventoryRecipe");
                InfiniteInventoryRecipe.ProductionTime = 1f;
                foreach (Recipe recipe in GameResources.Instance.Recipes)
                {
                    if (recipe.name != "InfiniteInventoryRecipe")
                    {
                        recipe.Inputs = new InventoryItemData[] { CreateSingleIID("CoalOre", 1) };
                        recipe.ProductionTime = 1f;
                    }
                }
            }
            

        }
        public void CreateRecipe(InventoryItemData[] Inputs, InventoryItemData Output, string guidstring, Recipe recipecategory, string name, float ProductionTimeMultiplier)
        {
            var newrecipe = ScriptableObject.CreateInstance<Recipe>();
            newrecipe.name = name;
            newrecipe.Order = recipecategory.Order;
            newrecipe.Inputs = Inputs;
            newrecipe.Output = Output;
            newrecipe.RequiredUpgrades = recipecategory.RequiredUpgrades;
            newrecipe.Categories = recipecategory.Categories.ToArray();
            newrecipe.ProductionTime = recipecategory.ProductionTime * 2;

            var guid = GUID.Parse(guidstring);

            typeof(Definition).GetField("m_assetId", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(newrecipe, guid);

            AssetReference[] assets = new AssetReference[] { new AssetReference() { Object = newrecipe, Guid = guid, Labels = new string[0] } };
            RuntimeAssetStorage.Add(assets, default);
        }
        public ItemDefinition GetItem(string itemname)
        {
            return GameResources.Instance.Items.FirstOrDefault(s => s.name == itemname);
        }
        public Recipe GetRecipe(string recipename)
        {
            return GameResources.Instance.Recipes.FirstOrDefault(s => s.name == recipename);
        }
        public InventoryItemData CreateSingleIID(string itemname, int amount)
        {
            return new InventoryItemData { Item = GetItem(itemname), Amount = amount };
        }
        public void CreateRecipeSimple(string Input1name, int Input1amount, string Input2name, int Input2amount, string GUID, string Output, int Outputamount, string recipecategory, string RecipeName, float ProductionTimeMultiplier)
        {
            CreateRecipe(new InventoryItemData[] { CreateSingleIID(Input1name, Input1amount),
            CreateSingleIID(Input2name, Input2amount) },
            CreateSingleIID(Output, Outputamount), GUID,
            GetRecipe(recipecategory), RecipeName, ProductionTimeMultiplier);
        }
        public void CreateRecipeSimple(string Input1name, int Input1amount, string GUID, string Output, int Outputamount, string recipecategory, string RecipeName, float ProductionTimeMultiplier)
        {
            CreateRecipe(new InventoryItemData[] { CreateSingleIID(Input1name, Input1amount) },
            CreateSingleIID(Output, Outputamount), GUID,
            GetRecipe(recipecategory), RecipeName, ProductionTimeMultiplier);
        }
        public void CreateRecipeSimple(string Input1name, int Input1amount, string Input2name, int Input2amount, string Input3name, int Input3amount, string GUID, string Output, int Outputamount, string recipecategory, string RecipeName, float ProductionTimeMultiplier)
        {
            CreateRecipe(new InventoryItemData[] { CreateSingleIID(Input1name, Input1amount),
            CreateSingleIID(Input2name, Input2amount), CreateSingleIID(Input3name, Input3amount) },
            CreateSingleIID(Output, Outputamount), GUID,
            GetRecipe(recipecategory), RecipeName, ProductionTimeMultiplier);
        }
        public void CreateRecipeSimple(string Input1name, int Input1amount, string Input2name, int Input2amount, string Input3name, int Input3amount, string Input4name, int Input4amount, string GUID, string Output, int Outputamount, string recipecategory, string RecipeName, float ProductionTimeMultiplier)
        {
            CreateRecipe(new InventoryItemData[] { CreateSingleIID(Input1name, Input1amount),
            CreateSingleIID(Input2name, Input2amount), CreateSingleIID(Input3name, Input3amount), CreateSingleIID(Input4name, Input4amount) },
            CreateSingleIID(Output, Outputamount), GUID,
            GetRecipe(recipecategory), RecipeName, ProductionTimeMultiplier);
        }


    }
}
