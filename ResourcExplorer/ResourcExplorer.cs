
using System;
using ResourcExplorer;
using RimWorld;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Verse;



namespace ResourcExplorer
{

    public class ITab_ResourcExplorer : ITab
    {
        private static readonly IEnumerable<RecipeDef> BaseRecipeList = DefDatabase<RecipeDef>.AllDefsListForReading;
        public static IEnumerable<RecipeDef> RescRecipes => BaseRecipeList;


        private Thing ResourceToExamine
        {
            get
            {
                Thing resourceThing = new Thing();
                resourceThing = base.SelThing;
                if (resourceThing != null && resourceThing.stackCount > 0)
                {
                    return resourceThing;

                }
                return null;
            }
        }

        public override bool IsVisible
        {
            get
            {
                Thing rescBase = ResourceToExamine;
                IEnumerable<RecipeDef> checkRecipes = ITab_ResourcExplorer.RescRecipes.Where((RecipeDef recipe) => recipe.IsIngredient(rescBase.def));
                foreach (RecipeDef item in checkRecipes)
                {
                    if (rescBase != null)
                    { 
                        return true;
                    }
                }
                return false;
            }

        }
        

        public ITab_ResourcExplorer()
        {
            size = ResourcExplorerCardUtility.RescCardSize + new Vector2(17f, 17f);
            labelKey = "AT_TabResourcExplorer";
            tutorTag = "Explore Me!";

        }

        protected override void FillTab()
        {
            Rect rect = new Rect(17f, 17f, ResourcExplorerCardUtility.RescCardSize.x, ResourcExplorerCardUtility.RescCardSize.y);
            ResourcExplorerCardUtility.DrawResourceCard(rect, ResourceToExamine);
        }
    } 
}


internal class ResourcExplorerCardUtility
{
    private static string resourceName;

    private static string sourceSay;

    private static Vector2 rescCardSize = default(Vector2);

    public static Vector2 scrollPosition = Vector2.zero;

    public static float rectPadding = 2f;

    private static float HeaderSize = 30f;

    private static float TextSize = 25f;

    private static float WorkbenchColumnHeight = 100f;

    private static float WorkbenchColumnWidth = 300f;

    public static Vector2 RescCardSize
    {
        get
        {
            if (rescCardSize == default(Vector2))
            {
                rescCardSize = new Vector2(500f, 356f);
                if (Screen.currentResolution.height <= 800)
                {
                    rescCardSize = new Vector2(500f, 276f);
                }
            }
            return rescCardSize;
        }
    }

    public static void DrawResourceCard(Rect rect, Thing selectedResc)
    {
        float x = Text.CalcSize(selectedResc.Label.Translate()).x;
        resourceName = selectedResc.Label.Translate().CapitalizeFirst();
        Rect rect2 = new Rect((rescCardSize.x / 2 - x), rect.height - HeaderSize, resourceName.Length, HeaderSize);
        Rect rect3 = new Rect(rect.x + 5f, rect.y + 5f, rect.width - 5f, rect2.y - 5f);
        Rect rect4 = new Rect(rect.x + 2f, rect3.y + rect3.height + 2f, rect3.width, rect.height);
        Rect rect5 = new Rect(rect.x + 2f, rect4.height + 5f, rect.width - 5f, rect.height);

        Widgets.BeginGroup(rect2);
        Text.Font = GameFont.Medium;
        GUI.color = Color.white;
        Widgets.Label(rect2, resourceName);
        Widgets.DrawLineHorizontal(rect2.x + 5f, HeaderSize - 2f, rect.width - 5f);
        Widgets.EndGroup();

        //These are the various sections of ResourcExplorer.
        Widgets.BeginGroup(rect3);
        ResourceSources(rect3, selectedResc);
        Widgets.EndGroup();

        Widgets.BeginGroup(rect4);
        Widgets.BeginScrollView(rect4, ref scrollPosition, (new Rect(rect4.x + rectPadding,rect4.y + rectPadding, rect4.width - rectPadding, rect4.height - rectPadding)), showScrollbars:true);
        new ResourceCategories(rect4, selectedResc);
        Widgets.EndScrollView();
        Widgets.EndGroup();

        //These are miscellaneous information of the selected resource.
        Widgets.BeginGroup(rect5);
        new ResourceMisc(rect5, selectedResc);
        Widgets.EndGroup();
    }

    public static void ResourceSources(Rect sourceRect, Thing sourcedThing)
    {
        ThingDef sourcedThingDef = sourcedThing.def;
        Rect sourceRect2 = new Rect(sourceRect.x + 2f, sourceRect.y - 30f, sourceRect.width - 2f, TextSize);
        Rect workbenchRect = new Rect(sourceRect2.x + 10f, sourceRect2.y + 5f, WorkbenchColumnWidth, WorkbenchColumnHeight);

        Widgets.BeginGroup(sourceRect2);
        Text.Font = GameFont.Small;
        GUI.color = Color.white;
        Widgets.Label(sourceRect2, sourceSay);
        Widgets.EndGroup();



        if (sourcedThingDef.IsMetal || sourcedThingDef.thingCategories.Contains(ThingCategoryDefOf.StoneChunks))
        {
           sourceSay = "AT_Mine".Translate().CapitalizeFirst();
        }
        else if (sourcedThingDef.thingCategories.Contains(ThingCategoryDefOf.PlantFoodRaw) || sourcedThingDef.thingCategories.Contains(ThingCategoryDefOf.PlantMatter)
            || sourcedThingDef.IsFungus || sourcedThingDef.stuffCategories.Contains(StuffCategoryDefOf.Woody) || sourcedThingDef.defName == "Cloth" || sourcedThingDef.defName == "DevilstrandCloth"
            || sourcedThingDef.defName == "MedicineHerbal")
        {
            sourceSay = "AT_Plant".Translate().CapitalizeFirst();
        }
        else if (sourcedThingDef.IsAnimalProduct || sourcedThingDef.thingCategories.Contains(ThingCategoryDefOf.Wools)
            || sourcedThingDef.IsLeather || sourcedThingDef.IsEgg)
        {
            sourceSay = "AT_Animal".Translate().CapitalizeFirst();
        }
        else if (sourcedThingDef.thingCategories.Contains(ThingCategoryDefOf.Manufactured) || sourcedThingDef.thingCategories.Contains(ThingCategoryDefOf.Foods)
            || sourcedThingDef.thingCategories.Contains(ThingCategoryDefOf.Apparel) || sourcedThingDef.thingCategories.Contains(ThingCategoryDefOf.ApparelArmor)
            || sourcedThingDef.thingCategories.Contains(ThingCategoryDefOf.ArmorHeadgear) || sourcedThingDef.thingCategories.Contains(ThingCategoryDefOf.Drugs)
            || sourcedThingDef.thingCategories.Contains(ThingCategoryDefOf.Items) || sourcedThingDef.thingCategories.Contains(ThingCategoryDefOf.StoneBlocks)
            || sourcedThingDef.thingCategories.Contains(ThingCategoryDefOf.Weapons))
        {
            sourceSay = "AT_CraftSource".Translate().CapitalizeFirst();
            ResourcExplorerCardUtility.RescWorkTables(workbenchRect, sourcedThingDef);
        }
        else
        {
            sourceSay = "AT_Misc".Translate().CapitalizeFirst();
        }

    }

    public static void RescWorkTables(Rect sourceWorkbenchRect, ThingDef rescThingDef)
    {
        float num = 0f;
        string sourceSayMore = "";
        List<ThingDef> rescWorktables = (List<ThingDef>)DefDatabase<ThingDef>.AllDefsListForReading.Where((ThingDef thingDef) => thingDef.IsWorkTable);
        Rect worktableRect = new Rect(sourceWorkbenchRect.x, sourceWorkbenchRect.y, WorkbenchColumnWidth - 2f, TextSize + num);
        
        for (int i = 0; i < rescWorktables.Count(); i++)
        {
            if (rescWorktables[i].recipes != null && rescWorktables[i].recipes[i].IsIngredient(rescThingDef))
            {

                Widgets.BeginGroup(worktableRect);
                Text.Font = GameFont.Small;
                GUI.color = Color.white;
                Widgets.LabelWithIcon(worktableRect, rescWorktables[i].LabelCap, Widgets.GetIconFor(rescWorktables[i]), 1f);
                Widgets.InfoCardButton(worktableRect.width - 24f, worktableRect.height/2f, rescWorktables[i]);
                num += 27;
                Text.Font = GameFont.Small;
                GUI.color = Color.white;
                Widgets.Label(new Rect(sourceWorkbenchRect.x, sourceWorkbenchRect.y - 30f, sourceWorkbenchRect.width, TextSize), sourceSayMore);
                sourceSayMore = "AT_CraftProducts".Translate().CapitalizeFirst();
                Widgets.EndGroup();
            }
            else
            {
                Widgets.BeginGroup(worktableRect);
                Text.Font = GameFont.Small;
                GUI.color = Color.white;
                Widgets.Label(new Rect(sourceWorkbenchRect.x, sourceWorkbenchRect.y - 30f, sourceWorkbenchRect.width, TextSize), sourceSayMore);
                sourceSayMore = "...which unfortunately doesn't include this resource.".CapitalizeFirst();
                Widgets.EndGroup();
            }
            
        }
    }
}


