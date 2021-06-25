using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using UnityEngine;

namespace VolcanoidsMod
{
    public class CustomRecipes : MonoBehaviour
    {
        public string ModPath;
        private void Awake()
        {
            ModPath = Path.Combine(Application.persistentDataPath, "Mods/GenericMod");
            Debug.Log("Module: " + GetType().Name + " loaded successfully");
            // Add custom recipes below here

            CreateRecipeSimple("CopperOre", 5, "0B7717FC2854467FA3D23347E3B9DCFB", "CopperIngot", 6, "CopperIngotRecipe", "5xCopperSmeltingRecipe", 2f, null);

            CreateRecipeSimple("CopperScrapMetal", 5, "C887223B8D2E4A35BD21412BC6243BD5", "CopperIngot", 12, "CopperIngotRecipe2", "5xCopperScrapSmeltingRecipe", 2f, null);

            CreateRecipeSimple("CopperOre", 10, "B98720D4DB54470B82DBC2D1B4D7C8C4", "CopperIngot", 11, "5xCopperSmeltingRecipe", "10xCopperSmeltingRecipe", 1.225f, null);

            CreateRecipeSimple("IronOre", 5, "D2EFA87BECC544D28864889E28A9A022", "IronIngot", 6, "IronIngotRecipe", "5xIronSmeltingRecipe", 2f, null);

            CreateRecipeSimple("IronScrapMetal", 5, "33AB9A638A9748528674F2857007CC2A", "IronIngot", 11, "IronIngotRecipe2", "5xIronScrapSmeltingRecipe", 2f, null);

            CreateRecipeSimple("IronOre", 10, "9E9FBCF1D8B649D8A89D970AC316B6C0", "IronIngot", 11, "5xIronSmeltingRecipe", "10xIronSmeltingRecipe", 1.225f, null);

            CreateRecipeSimple("CrystalOre", 5, "BE6B310CCD124943A9F66D49083BCAEC", "CrystalIngot", 6, "CrystalIngotRecipe", "5xCrystalSmeltingRecipe", 2f, null);

            CreateRecipeSimple("CrystalOre", 10, "D7AD946D9CE340EBBADB19D96D2A48B7", "CrystalIngot", 11, "5xCrystalSmeltingRecipe", "10xCrystalSmeltingRecipe", 1.225f, null);

            CreateRecipeSimple("TitaniumOre", 5, "3244E6E78A9043D99BC1D847A30473B1", "TitaniumIngot", 6, "TitaniumIngotRecipe", "5xTitaniumSmeltingRecipe", 2f, null);

            CreateRecipeSimple("TitaniumScrapMetal", 5, "FE5251FD26D747EDA935BDFF7822CE3A", "TitaniumIngot", 12, "TitaniumIngotRecipe2", "5xTitaniumScrapSmeltingRecipe", 2f, null);

            CreateRecipeSimple("DiamondOre", 5, "A4E2E0A221264B26A316899DC25B84B3", "DiamondIngot", 6, "DiamondIngotRecipe", "5xDiamondSmeltingRecipe", 2f, null);

            // Add custom recipes above here
            if (haserror)
            {
                Debug.LogError("Module: " + GetType().Name + " Initialized with error");
            }
            else
            {
                Debug.Log("Module: " + GetType().Name + " Initialized successfully");
            }
        }

        public void CreateRecipe(InventoryItem[] Inputs, InventoryItem Output, string guidstring, Recipe recipecategory, string name, float ProductionTimeMultiplier, Sprite icon)
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
            RuntimeAssetStorage.Add(assets);
        }
        public ItemDefinition GetItem(string itemname)
        {
            ItemDefinition item = GameResources.Instance.Items.First(s => s.name == itemname);
            if (item == null)
            {
                Debug.LogError("Item is null, name: " + itemname + ". Replacing with NullItem");
                haserror = true;
                return GameResources.Instance.Items.First(s => s.name == "NullItem");
            }
            return item;

        }
        public Sprite Sprite2(string iconpath)
        {
            if (iconpath == null)
            {
                return null;
            }
            var path = System.IO.Path.Combine(ModPath, iconpath);
            if (!File.Exists(path))
            {
                Debug.LogError("Specified Icon path not found: " + path);
                haserror = true;
                return null;
            }
            var bytes = File.ReadAllBytes(path);


            var texture = new Texture2D(512, 512, TextureFormat.ARGB32, true);
            texture.LoadImage(bytes);

            var sprite = Sprite.Create(texture, new Rect(Vector2.zero, Vector2.one * texture.width), new Vector2(0.5f, 0.5f), texture.width, 0, SpriteMeshType.FullRect, Vector4.zero, false);
            return sprite;
        }
        public Recipe GetRecipe(string recipename)
        {
            var recipe = GameResources.Instance.Recipes.First(s => s.name == recipename);
            if (recipe == null)
            {
                Debug.LogError("Specified Recipe not found: " + recipename);
                haserror = true;
                return GameResources.Instance.Recipes.First(s => s.name == "GenericNullItem");
            }
            return recipe;
        }
        public InventoryItem CreateSingleIID(string itemname, int amount)
        {
            return new InventoryItem { Item = GetItem(itemname), Amount = amount };
        }
        public void CreateRecipeSimple(string Input1name, int Input1amount, string Input2name, int Input2amount, string GUID, string Output, int Outputamount, string recipecategory, string RecipeName, float ProductionTimeMultiplier, Sprite icon)
        {
            CreateRecipe(new InventoryItem[] { CreateSingleIID(Input1name, Input1amount),
            CreateSingleIID(Input2name, Input2amount) },
            CreateSingleIID(Output, Outputamount), GUID,
            GetRecipe(recipecategory), RecipeName, ProductionTimeMultiplier, icon);
        }
        public void CreateRecipeSimple(string Input1name, int Input1amount, string GUID, string Output, int Outputamount, string recipecategory, string RecipeName, float ProductionTimeMultiplier, Sprite icon)
        {
            CreateRecipe(new InventoryItem[] { CreateSingleIID(Input1name, Input1amount) },
            CreateSingleIID(Output, Outputamount), GUID,
            GetRecipe(recipecategory), RecipeName, ProductionTimeMultiplier, icon);
        }
        public void CreateRecipeSimple(string Input1name, int Input1amount, string Input2name, int Input2amount, string Input3name, int Input3amount, string GUID, string Output, int Outputamount, string recipecategory, string RecipeName, float ProductionTimeMultiplier, Sprite icon)
        {
            CreateRecipe(new InventoryItem[] { CreateSingleIID(Input1name, Input1amount),
            CreateSingleIID(Input2name, Input2amount), CreateSingleIID(Input3name, Input3amount) },
            CreateSingleIID(Output, Outputamount), GUID,
            GetRecipe(recipecategory), RecipeName, ProductionTimeMultiplier, icon);
        }
        public void CreateRecipeSimple(string Input1name, int Input1amount, string Input2name, int Input2amount, string Input3name, int Input3amount, string Input4name, int Input4amount, string GUID, string Output, int Outputamount, string recipecategory, string RecipeName, float ProductionTimeMultiplier, Sprite icon)
        {
            CreateRecipe(new InventoryItem[] { CreateSingleIID(Input1name, Input1amount),
            CreateSingleIID(Input2name, Input2amount), CreateSingleIID(Input3name, Input3amount), CreateSingleIID(Input4name, Input4amount) },
            CreateSingleIID(Output, Outputamount), GUID,
            GetRecipe(recipecategory), RecipeName, ProductionTimeMultiplier, icon);
        }
        public void CreateRecipeSimple(string Input1name, int Input1amount, string Input2name, int Input2amount, string Input3name, int Input3amount, string Input4name, int Input4amount, string Input5name, int Input5amount, string GUID, string Output, int Outputamount, string recipecategory, string RecipeName, float ProductionTimeMultiplier, Sprite icon)
        {
            CreateRecipe(new InventoryItem[] { CreateSingleIID(Input1name, Input1amount),
            CreateSingleIID(Input2name, Input2amount), CreateSingleIID(Input3name, Input3amount), CreateSingleIID(Input4name, Input4amount), CreateSingleIID(Input5name, Input5amount) },
            CreateSingleIID(Output, Outputamount), GUID,
            GetRecipe(recipecategory), RecipeName, ProductionTimeMultiplier, icon);
        }
        private bool haserror;
    }
}