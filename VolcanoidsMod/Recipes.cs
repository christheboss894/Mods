using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using Mono.WebBrowser;
using UnityEngine;

namespace VolcanoidsMod
{
    public class Recipes : MonoBehaviour
    {
        private void Awake()
        {
            haserror = false;
            Debug.Log("Module: " + GetType().Name + " loaded successfully");
            // CreateRecipeSimple("CoalOre", 1, "539DF755FAC847BB88CF43DF2AA159C4", "CaptainHead", 1, "ShipCoreUpgrade3Recipe", "CaptainHeadRecipe", 1f, null);
            CreateRecipeSimple("CoalOre", 1, "AlloyT1Ingot", 10, "B01B65E123DB438D98D1AB7DED917DE5", "AlloyT2Ingot", 1, "AlloyT2Recipe", "AlloyT1toAlloyT3Recipe", 3f, null);
            CreateRecipeSimple("CoalOre", 4, "AlloyT2Ingot", 10, "0E98FAF0FA9449338428C7DC771E42F6", "AlloyT3Ingot", 1, "AlloyT3Recipe", "AlloyT2toAlloyT3Recipe", 3f, null);
            CreateRecipeSimple("CoalOre", 15, "CopperOre", 10, "0B7717FC2854467FA3D23347E3B9DCFB", "CopperIngot", 10, "CopperIngotRecipe", "10xCopperSmeltingRecipe", 5f, null);
            CreateRecipeSimple("CoalOre", 20, "CopperOre", 20, "B98720D4DB54470B82DBC2D1B4D7C8C4", "CopperIngot", 20, "CopperIngotRecipe", "20xCopperSmeltingRecipe", 10f, null);
            
            // CreateRecipeSimple("AlloyT2Ingot", 10, "IntelProductionT2", 1, "C6A85ACFE1504FAFB437A89553B84144", "TurretImproved", 1, "TurretModuleRecipe", "TurretImprovedRecipe", 1.5f, null);

            CreateRecipeSimple("TitaniumIngot", 15, "CrystalIngot", 10, "2E3EE998AD8F470CAF35471421CA3AAE", "TungstenIngot", 1, "AlloyT3Recipe", "TungstenIngotRecipe", 2f, null);

            CreateRecipeSimple("AlloyT4Ingot", 30, "DiamondIngot", 20, "61C5C4430D7F42D2B599A582C44E930E", "UnobtainiumIngot", 1, "AlloyT3Recipe", "UnobtainiumIngotRecipe", 5f, null);

            CreateRecipeSimple("TungstenIngot", 2, "TitaniumIngot", 1, "A7E62288C0D646739C2F53968C210957", "AlloyT4Ingot", 1, "UnobtainiumIngotRecipe", "AlloyT4Recipe", 2f, null);

            CreateRecipeSimple("TungstenIngot", 1, "46ACCE55EE28490BA14F73C08A83050C", "TungstenPlates", 2, "ResearchT3_AnalyzerRecipe", "TungstenPlatesRecipe", 1.5f, null);

            CreateRecipeSimple("TungstenIngot", 1, "A5D9565795624D7FAD861F8FC0B0403D", "TungstenTubes", 1, "TungstenPlatesRecipe", "TungstenTubesRecipe", 1.5f, null);

            CreateRecipeSimple("TungstenIngot", 1, "CFA38F3064494CF884D40EE8DA2772E8", "TungstenBolts", 2, "TungstenTubesRecipe", "TungstenBoltsRecipe", 1.5f, null);

            CreateRecipeSimple("UnobtainiumIngot", 1, "71FAB79094F4495291EDABB2D67146B0", "UnobtainiumPlates", 2, "TungstenPlatesRecipe", "UnobtainiumPlatesRecipe", 1.5f, null);

            CreateRecipeSimple("UnobtainiumIngot", 1, "66BFCE9D2EEF494BA6AC7CF56DB66E1C", "UnobtainiumTubes", 1, "UnobtainiumPlatesRecipe", "UnobtainiumTubesRecipe", 1.5f, null);

            CreateRecipeSimple("UnobtainiumIngot", 1, "4C1A520F5ECB45B0A0C0CD3E2A2CC109", "UnobtainiumBolts", 2, "UnobtainiumTubesRecipe", "UnobtainiumBoltsRecipe", 1.5f, null);

            CreateRecipeSimple("AlloyT3Ingot", 40, "IntelResearchT3", 5, "IntelRefineryT3", 5, "IntelProductionT3", 5, "Parts_TracksT4_Advanced", 5, "EABEBE7067F44B228BDEBAC23548AE47", "TracksT5_Supreme", 1, "TracksUpgrade4Recipe", "TracksT5Recipe", 2f, null);

            CreateRecipeSimple("TitaniumTubes", 4, "TitaniumPlates", 2, "DiamondIngot", 6, "IntelProductionT2", 4, "23CA71CB89644644881DC484A1879D0D", "ShipCoreUpgrade4", 1, "ShipCoreUpgrade3Recipe", "ShipCoreUpgrade4Recipe", 2f, null);

            CreateRecipeSimple("TungstenIngot", 12, "IntelRefineryT3", 4, "IntelProductionT3", 4, "D7415C76EB4A438C86432501D59998E0", "ShipCoreUpgrade5", 1, "ShipCoreUpgrade4Recipe", "ShipCoreUpgrade5Recipe", 2f, null);

            CreateRecipeSimple("UnobtainiumIngot", 10, "UnobtainiumTubes", 4, "UnobtainiumBolts", 5, "35FCCDDC5C464FD5A3F7FFA6094AAF53", "ShipCoreUpgrade6", 1, "ShipCoreUpgrade5Recipe", "ShipCoreUpgrade6Recipe", 4f, null); 

            CreateRecipeSimple("TungstenPlates", 5, "TungstenBolts", 4, "A3046813A43345AEB6A8AA5353E8A77B", "HullT5_Tungsten", 1, "HullUpgrade4Recipe", "HullUpgrade5Recipe", 2f, null);

            CreateRecipeSimple("UnobtainiumPlates", 20, "UnobtainiumBolts", 15, "UnobtainiumIngot", 15, "B37606501B2B40FABF697CEF68C26DA4", "HullT6_Unobtainium", 1, "HullUpgrade5Recipe", "HullUpgrade6Recipe", 5f, null);

            CreateRecipeSimple("TungstenIngot", 10, "TungstenPlates", 8, "TungstenTubes", 5, "TungstenBolts", 5, "2CBFF4BD72C54C6AA7D85B4AA22711D7", "DrillT5_Tungsten", 1, "DrillUpgrade4Recipe", "DrillUpgrade5Recipe", 2f, null);

            CreateRecipeSimple("UnobtainiumIngot", 15, "UnobtainiumPlates", 10, "UnobtainiumTubes", 10, "UnobtainiumBolts", 10, "C922C8F07B294F98BAF5AAAD706F2E4E4", "DrillT6_Unobtainium", 1, "DrillUpgrade5Recipe", "DrillUpgrade6Recipe", 5f, null);

            CreateRecipeSimple("TitaniumTubes", 4, "TitaniumBolts", 5, "TitaniumPlates", 5, "3A53A2D331444AF0A4AE125736F232AF", "EngineUpgrade4", 1, "EngineUpgrade3Recipe", "EngineUpgrade4Recipe", 3f, null);

            CreateRecipeSimple("AlloyT4Ingot", 5, "TungstenBolts", 4, "TungstenTubes", 2, "F0B832C6187D44CE86A6F63165A6D698", "EngineUpgrade5", 1, "EngineUpgrade4Recipe", "EngineUpgrade5Recipe", 2f, null);

            CreateRecipeSimple("Leek", 1, "41231C07EFA44D2F92E3300E93410F19", "Leek", 1, "CopperIngotRecipe", "LeakRecipe", 1, null);

            CreateRecipeSimple("CoalOre", 1, "41231C07EFA44D2F92E3300E93410F19", "Leek", 1, "CopperIngotRecipe", "LeakRecipe", 1, null);


            if (GenericMod.Cheese)
            {
                CreateRecipeSimple("CoalOre", 1, GUID.Create().ToString(), "Cheese", 1, "ShipCoreUpgrade5Recipe", "Cheese", 0.01f, null);
            }
            if (GenericMod.InfiniteInventory)
            {
                CreateRecipeSimple("CopperOre", 1, "2B2F51C0DCA6446681867FB440B15472", "CoalOre", 50, "CopperIngotRecipe", "InfiniteInventoryRecipe", 0.01f, null);
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
            if (haserror)
            {
                Debug.Log("Module: " + GetType().Name + " Initialized with error");
            }
            else
            {
                Debug.Log("Module: " + GetType().Name + " Initialized successfully");
            }
        }
        public void CreateRecipe(InventoryItemData[] Inputs, InventoryItemData Output, string guidstring, Recipe recipecategory, string name, float ProductionTimeMultiplier, Sprite icon)
        {
            var newrecipe = ScriptableObject.CreateInstance<Recipe>();
            newrecipe.name = name;
            newrecipe.Order = recipecategory.Order;
            newrecipe.Inputs = Inputs;
            newrecipe.Output = Output;
            newrecipe.Icon = icon;
            newrecipe.RequiredUpgrades = recipecategory.RequiredUpgrades;
            newrecipe.Categories = recipecategory.Categories.ToArray();
            newrecipe.ProductionTime = recipecategory.ProductionTime * ProductionTimeMultiplier;

            var guid = GUID.Parse(guidstring);

            typeof(Definition).GetField("m_assetId", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(newrecipe, guid);

            AssetReference[] assets = new AssetReference[] { new AssetReference() { Object = newrecipe, Guid = guid, Labels = new string[0] } };
            RuntimeAssetStorage.Add(assets, default);
        }
        public ItemDefinition GetItem(string itemname)
        {
            ItemDefinition item = GameResources.Instance.Items.FirstOrDefault(s => s.name == itemname);
            if (item == null)
            {
                Debug.LogError("Item is null, name: " + itemname + ". Replacing with NullItem");
                haserror = true;
                return GameResources.Instance.Items.FirstOrDefault(s => s.name == "NullItem");
            }
            return item;

        }
        public Sprite Sprite2(string iconpath)
        {
            if (iconpath == null)
            {
                return null;
            }
            var path = System.IO.Path.Combine(Application.persistentDataPath, "Mods", iconpath);
            var bytes = File.ReadAllBytes(path);


            var texture = new Texture2D(512, 512, TextureFormat.ARGB32, true);
            texture.LoadImage(bytes);

            var sprite = Sprite.Create(texture, new Rect(Vector2.zero, Vector2.one * texture.width), new Vector2(0.5f, 0.5f), texture.width, 0, SpriteMeshType.FullRect, Vector4.zero, false);
            return sprite;
        }
        public Recipe GetRecipe(string recipename)
        {
            return GameResources.Instance.Recipes.FirstOrDefault(s => s.name == recipename);
        }
        public InventoryItemData CreateSingleIID(string itemname, int amount)
        {
            return new InventoryItemData { Item = GetItem(itemname), Amount = amount };
        }
        public void CreateRecipeSimple(string Input1name, int Input1amount, string Input2name, int Input2amount, string GUID, string Output, int Outputamount, string recipecategory, string RecipeName, float ProductionTimeMultiplier, Sprite icon)
        {
            CreateRecipe(new InventoryItemData[] { CreateSingleIID(Input1name, Input1amount),
            CreateSingleIID(Input2name, Input2amount) },
            CreateSingleIID(Output, Outputamount), GUID,
            GetRecipe(recipecategory), RecipeName, ProductionTimeMultiplier, icon);
        }
        public void CreateRecipeSimple(string Input1name, int Input1amount, string GUID, string Output, int Outputamount, string recipecategory, string RecipeName, float ProductionTimeMultiplier, Sprite icon)
        {
            CreateRecipe(new InventoryItemData[] { CreateSingleIID(Input1name, Input1amount) },
            CreateSingleIID(Output, Outputamount), GUID,
            GetRecipe(recipecategory), RecipeName, ProductionTimeMultiplier, icon);
        }
        public void CreateRecipeSimple(string Input1name, int Input1amount, string Input2name, int Input2amount, string Input3name, int Input3amount, string GUID, string Output, int Outputamount, string recipecategory, string RecipeName, float ProductionTimeMultiplier, Sprite icon)
        {
            CreateRecipe(new InventoryItemData[] { CreateSingleIID(Input1name, Input1amount),
            CreateSingleIID(Input2name, Input2amount), CreateSingleIID(Input3name, Input3amount) },
            CreateSingleIID(Output, Outputamount), GUID,
            GetRecipe(recipecategory), RecipeName, ProductionTimeMultiplier, icon);
        }
        public void CreateRecipeSimple(string Input1name, int Input1amount, string Input2name, int Input2amount, string Input3name, int Input3amount, string Input4name, int Input4amount, string GUID, string Output, int Outputamount, string recipecategory, string RecipeName, float ProductionTimeMultiplier, Sprite icon)
        {
            CreateRecipe(new InventoryItemData[] { CreateSingleIID(Input1name, Input1amount),
            CreateSingleIID(Input2name, Input2amount), CreateSingleIID(Input3name, Input3amount), CreateSingleIID(Input4name, Input4amount) },
            CreateSingleIID(Output, Outputamount), GUID,
            GetRecipe(recipecategory), RecipeName, ProductionTimeMultiplier, icon);
        }
        public void CreateRecipeSimple(string Input1name, int Input1amount, string Input2name, int Input2amount, string Input3name, int Input3amount, string Input4name, int Input4amount, string Input5name, int Input5amount,  string GUID, string Output, int Outputamount, string recipecategory, string RecipeName, float ProductionTimeMultiplier, Sprite icon)
        {
            CreateRecipe(new InventoryItemData[] { CreateSingleIID(Input1name, Input1amount),
            CreateSingleIID(Input2name, Input2amount), CreateSingleIID(Input3name, Input3amount), CreateSingleIID(Input4name, Input4amount), CreateSingleIID(Input5name, Input5amount) },
            CreateSingleIID(Output, Outputamount), GUID,
            GetRecipe(recipecategory), RecipeName, ProductionTimeMultiplier, icon);
        }
        private bool haserror;

    }
}
