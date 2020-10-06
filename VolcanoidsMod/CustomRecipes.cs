using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using Mono.WebBrowser;
using UnityEngine;

namespace VolcanoidsMod
{
    public class CustomRecipes : MonoBehaviour
    {
        private void Awake()
        {
            Debug.Log("Module: " + GetType().Name + " loaded successfully");
            // Add custom recipes below here



















            // Add custom recipes above here
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
        public void CreateRecipeSimple(string Input1name, int Input1amount, string Input2name, int Input2amount, string Input3name, int Input3amount, string Input4name, int Input4amount, string Input5name, int Input5amount, string GUID, string Output, int Outputamount, string recipecategory, string RecipeName, float ProductionTimeMultiplier, Sprite icon)
        {
            CreateRecipe(new InventoryItemData[] { CreateSingleIID(Input1name, Input1amount),
            CreateSingleIID(Input2name, Input2amount), CreateSingleIID(Input3name, Input3amount), CreateSingleIID(Input4name, Input4amount), CreateSingleIID(Input5name, Input5amount) },
            CreateSingleIID(Output, Outputamount), GUID,
            GetRecipe(recipecategory), RecipeName, ProductionTimeMultiplier, icon);
        }
        private bool haserror;
    }
}
