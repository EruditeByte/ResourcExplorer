using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using RimWorld;
using Verse;
using ResourcExplorer;
public class ResourceCategories
{
    public static float HeaderSize = 25f;

    public static float ProductWidth = 500f;

    public static float ProductHeight = 30f;
    
    public static IEnumerable<RecipeDef> searchRecipes = ITab_ResourcExplorer.RescRecipes;

    public static Rect sectionRect = new Rect(0f, 0f, ProductWidth, ProductHeight);

    private static ThingDef selectedRescDef;

    public static List<RecipeDef> currentSectionRecipes = (List<RecipeDef>)searchRecipes.Where((RecipeDef recipes) => recipes.IsIngredient(selectedRescDef));

    public ResourceCategories(Rect categoryOutRect, Thing categoryResource)
    {
        Rect categoriesRect = new Rect(categoryOutRect.x, categoryOutRect.y, categoryOutRect.width, categoryOutRect.height);
        selectedRescDef = categoryResource.def;

        Widgets.BeginGroup(categoryOutRect);
        FoodCategory(sectionRect);
        ManufactureCategory(sectionRect);
        ConstructCategory(sectionRect);
        PharmaCategory(sectionRect);
        ClothesCategory(sectionRect);
        WeaponCategory(sectionRect);
        ArmorCategory(sectionRect);
        BuildingCategory(sectionRect);
        MiscCategory(sectionRect);
        Widgets.EndGroup();

    }
    public static float FoodCategory(Rect foodRect)
    {
        List<RecipeDef> foodRecipes = currentSectionRecipes;
        float num = 0;
        Rect foodTitleRect = new Rect(foodRect.x + 5f, foodRect.y, foodRect.width, HeaderSize);
        
        Widgets.BeginGroup(foodTitleRect);
        Text.Font = GameFont.Medium;
        GUI.color = Color.white;
        Widgets.Label(foodTitleRect, "AT_MealHeader".Translate().CapitalizeFirst());
        GUI.color = Color.white;
        Widgets.DrawLineHorizontal(foodTitleRect.x + 1f, HeaderSize + 2f, foodTitleRect.width - 1f);
        Widgets.EndGroup();

        for (int i = 0; i < foodRecipes.Count; i++)
        {
            if (foodRecipes[i] != null && foodRecipes[i].products[i].thingDef.thingCategories.Contains(ThingCategoryDefOf.Foods))
            {
                Rect foodListRect = new Rect(foodRect.x, foodTitleRect.yMax + num, foodRect.width, foodRect.height);

                Widgets.BeginGroup(foodListRect);
                Text.Font = GameFont.Small;
                GUI.color = Color.white;
                Widgets.LabelWithIcon(foodListRect, foodRecipes[i].products[i].thingDef.LabelCap, Widgets.GetIconFor(foodRecipes[i].products[i].thingDef), 1f);
                Widgets.InfoCardButton(foodListRect.width - 24f, foodListRect.height / 2f, foodRecipes[i].products[i].thingDef);
                Widgets.EndGroup();
                num += foodRect.height;
            }
        }
        return foodRect.height;
    }
    public static float ManufactureCategory(Rect manufactureRect)
    {
        float curY = FoodCategory(sectionRect);

        List<RecipeDef> manufactureRecipes = currentSectionRecipes;
        float num = 0;
        Rect manufactureTitleRect = new Rect(manufactureRect.x + 5f, curY, manufactureRect.width, HeaderSize);

        Widgets.BeginGroup(manufactureTitleRect);
        Text.Font = GameFont.Medium;
        GUI.color = Color.white;
        Widgets.Label(manufactureTitleRect, "AT_ManufactureHeader".Translate().CapitalizeFirst());
        GUI.color = Color.white;
        Widgets.DrawLineHorizontal(manufactureTitleRect.x + 1f, HeaderSize + 2f, manufactureTitleRect.width - 1f);
        Widgets.EndGroup();

        for (int i = 0; i < manufactureRecipes.Count; i++)
        {
            if (manufactureRecipes[i] != null && manufactureRecipes[i].products[i].thingDef.thingCategories.Contains(ThingCategoryDefOf.Manufactured))
            {
                Rect manufactureListRect = new Rect(manufactureRect.x, manufactureTitleRect.yMax + num, manufactureRect.width, manufactureRect.height);

                Widgets.BeginGroup(manufactureListRect);
                Text.Font = GameFont.Small;
                GUI.color = Color.white;
                Widgets.LabelWithIcon(manufactureListRect, manufactureRecipes[i].products[i].thingDef.LabelCap, Widgets.GetIconFor(manufactureRecipes[i].products[i].thingDef), 1f);
                Widgets.InfoCardButton(manufactureListRect.width - 24f, manufactureListRect.height / 2f, manufactureRecipes[i].products[i].thingDef);
                Widgets.EndGroup();
                num += manufactureRect.height;
            }
        }
        return manufactureRect.height;
    }
    public static float ConstructCategory(Rect constructRect)
    {
        float curY = ManufactureCategory(sectionRect);

        List<RecipeDef> constructRecipes = currentSectionRecipes;
        float num = 0;
        Rect constructTitleRect = new Rect(constructRect.x + 5f, curY, constructRect.width, HeaderSize);

        Widgets.BeginGroup(constructTitleRect);
        Text.Font = GameFont.Medium;
        GUI.color = Color.white;
        Widgets.Label(constructTitleRect, "AT_ConstructHeader".Translate().CapitalizeFirst());
        GUI.color = Color.white;
        Widgets.DrawLineHorizontal(constructTitleRect.x + 1f, HeaderSize + 2f, constructTitleRect.width - 1f);
        Widgets.EndGroup();

        for (int i = 0; i < constructRecipes.Count; i++)
        {
            if (constructRecipes[i] != null && (constructRecipes[i].products[i].thingDef.thingCategories.Contains(ThingCategoryDefOf.StoneBlocks)))
            {
                Rect constructListRect = new Rect(constructRect.x, constructTitleRect.yMax + num, constructRect.width, constructRect.height);

                Widgets.BeginGroup(constructListRect);
                Text.Font = GameFont.Small;
                GUI.color = Color.white;
                Widgets.LabelWithIcon(constructListRect, constructRecipes[i].products[i].thingDef.LabelCap, Widgets.GetIconFor(constructRecipes[i].products[i].thingDef), 1f);
                Widgets.InfoCardButton(constructListRect.width - 24f, constructListRect.height / 2f, constructRecipes[i].products[i].thingDef);
                Widgets.EndGroup();
                num += constructRect.height;
            }
        }
        return constructRect.height;
    }
    public static float PharmaCategory(Rect pharmaRect)
    {
        float curY = ConstructCategory(sectionRect);

        List<RecipeDef> pharmaRecipes = currentSectionRecipes;
        float num = 0;
        Rect pharmaTitleRect = new Rect(pharmaRect.x + 5f, curY, pharmaRect.width, HeaderSize);

        Widgets.BeginGroup(pharmaTitleRect);
        Text.Font = GameFont.Medium;
        GUI.color = Color.white;
        Widgets.Label(pharmaTitleRect, "AT_PharmaHeader".Translate().CapitalizeFirst());
        GUI.color = Color.white;
        Widgets.DrawLineHorizontal(pharmaTitleRect.x + 1f, HeaderSize + 2f, pharmaTitleRect.width - 1f);
        Widgets.EndGroup();

        for (int i = 0; i < pharmaRecipes.Count; i++)
        {
            if (pharmaRecipes[i] != null && (pharmaRecipes[i].products[i].thingDef.thingCategories.Contains(ThingCategoryDefOf.Medicine) ||
                pharmaRecipes[i].products[i].thingDef.thingCategories.Contains(ThingCategoryDefOf.Drugs)))
            {
                Rect pharmaListRect = new Rect(pharmaRect.x, pharmaTitleRect.yMax + num, pharmaRect.width, pharmaRect.height);

                Widgets.BeginGroup(pharmaListRect);
                Text.Font = GameFont.Small;
                GUI.color = Color.white;
                Widgets.LabelWithIcon(pharmaListRect, pharmaRecipes[i].products[i].thingDef.LabelCap, Widgets.GetIconFor(pharmaRecipes[i].products[i].thingDef), 1f);
                Widgets.InfoCardButton(pharmaListRect.width - 24f, pharmaListRect.height / 2f, pharmaRecipes[i].products[i].thingDef);
                Widgets.EndGroup();
                num += pharmaRect.height;
            }
        }
        return pharmaRect.height;
    }
    public static float ClothesCategory(Rect clothesRect)
    {
        float curY = PharmaCategory(sectionRect);

        List<RecipeDef> clothesRecipes = currentSectionRecipes;
        float num = 0;
        Rect clothesTitleRect = new Rect(clothesRect.x + 5f, curY, clothesRect.width, HeaderSize);

        Widgets.BeginGroup(clothesTitleRect);
        Text.Font = GameFont.Medium;
        GUI.color = Color.white;
        Widgets.Label(clothesTitleRect, "AT_ClothesHeader".Translate().CapitalizeFirst());
        GUI.color = Color.white;
        Widgets.DrawLineHorizontal(clothesTitleRect.x + 1f, HeaderSize + 2f, clothesTitleRect.width - 1f);
        Widgets.EndGroup();

        for (int i = 0; i < clothesRecipes.Count; i++)
        {
            if (clothesRecipes[i] != null && clothesRecipes[i].products[i].thingDef.thingCategories.Contains(ThingCategoryDefOf.Apparel))
            {
                Rect clothesListRect = new Rect(clothesRect.x, clothesTitleRect.yMax + num, clothesRect.width, clothesRect.height);

                Widgets.BeginGroup(clothesListRect);
                Text.Font = GameFont.Small;
                GUI.color = Color.white;
                Widgets.LabelWithIcon(clothesListRect, clothesRecipes[i].products[i].thingDef.LabelCap, Widgets.GetIconFor(clothesRecipes[i].products[i].thingDef), 1f);
                Widgets.InfoCardButton(clothesListRect.width - 24f, clothesListRect.height / 2f, clothesRecipes[i].products[i].thingDef);
                Widgets.EndGroup();
                num += clothesRect.height;
            }
        }
        return clothesRect.height;
    }
    public static float WeaponCategory(Rect weaponRect)
    {
        float curY = ClothesCategory(sectionRect);

        List<RecipeDef> weaponRecipes = currentSectionRecipes;
        float num = 0;
        Rect weaponTitleRect = new Rect(weaponRect.x + 5f, curY, weaponRect.width, HeaderSize);

        Widgets.BeginGroup(weaponTitleRect);
        Text.Font = GameFont.Medium;
        GUI.color = Color.white;
        Widgets.Label(weaponTitleRect, "AT_WeaponHeader".Translate().CapitalizeFirst());
        GUI.color = Color.white;
        Widgets.DrawLineHorizontal(weaponTitleRect.x + 1f, HeaderSize + 2f, weaponTitleRect.width - 1f);
        Widgets.EndGroup();

        for (int i = 0; i < weaponRecipes.Count; i++)
        {
            if (weaponRecipes[i] != null && weaponRecipes[i].products[i].thingDef.thingCategories.Contains(ThingCategoryDefOf.Weapons))
            {
                Rect weaponListRect = new Rect(weaponRect.x, weaponTitleRect.yMax + num, weaponRect.width, weaponRect.height);

                Widgets.BeginGroup(weaponListRect);
                Text.Font = GameFont.Small;
                GUI.color = Color.white;
                Widgets.LabelWithIcon(weaponListRect, weaponRecipes[i].products[i].thingDef.LabelCap, Widgets.GetIconFor(weaponRecipes[i].products[i].thingDef), 1f);
                Widgets.InfoCardButton(weaponListRect.width - 24f, weaponListRect.height / 2f, weaponRecipes[i].products[i].thingDef);
                Widgets.EndGroup();
                num += weaponRect.height;
            }
        }
        return weaponRect.height;
    }
    public static float ArmorCategory(Rect armorRect)
    {
        float curY = WeaponCategory(sectionRect);

        List<RecipeDef> armorRecipes = currentSectionRecipes;
        float num = 0;
        Rect armorTitleRect = new Rect(armorRect.x + 5f, curY, armorRect.width, HeaderSize);

        Widgets.BeginGroup(armorTitleRect);
        Text.Font = GameFont.Medium;
        GUI.color = Color.white;
        Widgets.Label(armorTitleRect, "AT_ArmorHeader".Translate().CapitalizeFirst());
        GUI.color = Color.white;
        Widgets.DrawLineHorizontal(armorTitleRect.x + 1f, HeaderSize + 2f, armorTitleRect.width - 1f);
        Widgets.EndGroup();

        for (int i = 0; i < armorRecipes.Count; i++)
        {
            if (armorRecipes[i] != null && armorRecipes[i].products[i].thingDef.thingCategories.Contains(ThingCategoryDefOf.ApparelArmor) || 
                armorRecipes[i].products[i].thingDef.thingCategories.Contains(ThingCategoryDefOf.ArmorHeadgear))
            {
                Rect armorListRect = new Rect(armorRect.x, armorTitleRect.yMax + num, armorRect.width, armorRect.height);

                Widgets.BeginGroup(armorListRect);
                Text.Font = GameFont.Small;
                GUI.color = Color.white;
                Widgets.LabelWithIcon(armorListRect, armorRecipes[i].products[i].thingDef.LabelCap, Widgets.GetIconFor(armorRecipes[i].products[i].thingDef), 1f);
                Widgets.InfoCardButton(armorListRect.width - 24f, armorListRect.height / 2f, armorRecipes[i].products[i].thingDef);
                Widgets.EndGroup();
                num += armorRect.height;
            }
        }
        return armorRect.height;
    }
    public static float BuildingCategory(Rect buildingRect)
    {
        float curY = ArmorCategory(sectionRect);

        List<RecipeDef> buildingRecipes = currentSectionRecipes;
        float num = 0;
        Rect buildingTitleRect = new Rect(buildingRect.x + 5f, curY, buildingRect.width, HeaderSize);

        Widgets.BeginGroup(buildingTitleRect);
        Text.Font = GameFont.Medium;
        GUI.color = Color.white;
        Widgets.Label(buildingTitleRect, "AT_BuildingHeader".Translate().CapitalizeFirst());
        GUI.color = Color.white;
        Widgets.DrawLineHorizontal(buildingTitleRect.x + 1f, HeaderSize + 2f, buildingTitleRect.width - 1f);
        Widgets.EndGroup();

        for (int i = 0; i < buildingRecipes.Count; i++)
        {
            if (buildingRecipes[i] != null && buildingRecipes[i].products[i].thingDef.IsBuildingArtificial && (buildingRecipes[i].products[i].thingDef.thingCategories.
                Contains(ThingCategoryDefOf.Buildings) || buildingRecipes[i].products[i].thingDef.thingCategories.Contains(ThingCategoryDefOf.BuildingsArt) || 
                buildingRecipes[i].products[i].thingDef.thingCategories.Contains(ThingCategoryDefOf.BuildingsSpecial)))

            {
                Rect buildingListRect = new Rect(buildingRect.x, buildingTitleRect.yMax + num, buildingRect.width, buildingRect.height);

                Widgets.BeginGroup(buildingListRect);
                Text.Font = GameFont.Small;
                GUI.color = Color.white;
                Widgets.LabelWithIcon(buildingListRect, buildingRecipes[i].products[i].thingDef.LabelCap, Widgets.GetIconFor(buildingRecipes[i].products[i].thingDef), 1f);
                Widgets.InfoCardButton(buildingListRect.width - 24f, buildingListRect.height / 2f, buildingRecipes[i].products[i].thingDef);
                Widgets.EndGroup();
                num += buildingRect.height;
            }
        }
        return buildingRect.height;
    }
    public void MiscCategory(Rect miscRect)
    {
        float curY = BuildingCategory(sectionRect);

        List<RecipeDef> miscRecipes = currentSectionRecipes;
        float num = 0;
        Rect miscTitleRect = new Rect(miscRect.x + 5f, curY, miscRect.width, HeaderSize);

        Widgets.BeginGroup(miscTitleRect);
        Text.Font = GameFont.Medium;
        GUI.color = Color.white;
        Widgets.Label(miscTitleRect, "AT_MiscHeader".Translate().CapitalizeFirst());
        GUI.color = Color.white;
        Widgets.DrawLineHorizontal(miscTitleRect.x + 1f, HeaderSize + 2f, miscTitleRect.width - 1f);
        Widgets.EndGroup();

        for (int i = 0; i < miscRecipes.Count; i++)
        {
            if (miscRecipes[i] != null && miscRecipes[i].products[i].thingDef.thingCategories.Contains(ThingCategoryDefOf.Items))
            {
                Rect miscListRect = new Rect(miscRect.x, miscTitleRect.yMax + num, miscRect.width, miscRect.height);

                Widgets.BeginGroup(miscListRect);
                Text.Font = GameFont.Small;
                GUI.color = Color.white;
                Widgets.LabelWithIcon(miscListRect, miscRecipes[i].products[i].thingDef.LabelCap, Widgets.GetIconFor(miscRecipes[i].products[i].thingDef), 1f);
                Widgets.InfoCardButton(miscListRect.width - 24f, miscListRect.height / 2f, miscRecipes[i].products[i].thingDef);
                Widgets.EndGroup();
                num += miscRect.height;
            }
        }
    }
}

