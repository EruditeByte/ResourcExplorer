using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using RimWorld;
using Verse;

namespace ResourcExplorer
{
    public class ResourceMisc
    {
        public static float HeaderSize = 25f;

        public static float ThingInfoWidth = 500f;

        public static float ThingInfoHeight = 30f;

        public static Rect sectionRect = new Rect(0f, 0f, ThingInfoWidth, ThingInfoHeight);

        public static ThingDef rescToExamine;

        [MayRequireAnomaly]
        private static readonly CompProperties_EquippableAbilityReloadable ammoWeaponUsersProps = new CompProperties_EquippableAbilityReloadable();

        private static readonly CompProperties_ApparelReloadable ammoApparelUsersProps = new CompProperties_ApparelReloadable();

        public static IEnumerable<ThingDef> thingDefList = DefDatabase<ThingDef>.AllDefsListForReading;

        public ResourceMisc(Rect extraRect, Thing selectedResc)
        {
            Rect fuelInfoRect = new Rect(extraRect.x, extraRect.y, extraRect.width, extraRect.height);
            Rect ammoInfoRect = new Rect(extraRect.x, fuelInfoRect.height, extraRect.width, extraRect.height);
            rescToExamine = selectedResc.def;

            Widgets.BeginGroup(extraRect);
            IsFuelSource(fuelInfoRect);
            IsAmmoSource(ammoInfoRect);
            Widgets.EndGroup();
        }
        private static bool IsFuelSource(Rect fuelSection)
        {
            Rect fuelHeadingRect = new Rect(fuelSection.x + 1f, fuelSection.y + 2f, fuelSection.width, HeaderSize);
            String fuelSay = "";
            bool isFuel = false;

            Widgets.BeginGroup(fuelHeadingRect);
            Text.Font = GameFont.Medium;
            GUI.color = Color.white;
            Widgets.Label(fuelHeadingRect, fuelSay.CapitalizeFirst());
            GUI.color = Color.white;
            Widgets.DrawLineHorizontal(fuelHeadingRect.x + 1f, HeaderSize + 2f, fuelHeadingRect.width - 1f);
            Widgets.EndGroup();

            List<ThingDef> fuelUsers = (List<ThingDef>)thingDefList.Where((ThingDef thingDef) => thingDef.IsBuildingArtificial);
            CompProperties_Refuelable fuelUsersProps = new CompProperties_Refuelable();

            for (int i = 0; i < fuelUsers.Count(); i++)
            {
                if (fuelUsers[i] != null && fuelUsersProps.fuelFilter.AllowedThingDefs.Contains(rescToExamine) && (!fuelUsers[i].building.IsTurret || 
                    fuelUsers[i].building.hasFuelingPort || fuelUsers[i].EverTransmitsPower))
                {
                    isFuel = true;
                    fuelSay = "this resource is a fuel source for the following buildings: ".CapitalizeFirst();
                    Rect fuelUserListRect = new Rect(fuelHeadingRect.x, fuelHeadingRect.height + 5f, fuelHeadingRect.width, fuelHeadingRect.height);

                    Widgets.BeginGroup(fuelUserListRect);
                    Widgets.LabelWithIcon(fuelUserListRect, fuelUsers[i].LabelCap, Widgets.GetIconFor(fuelUsers[i]), 1f);
                    Text.Font = GameFont.Small;
                    GUI.color = Color.white;
                    Widgets.InfoCardButton(fuelUserListRect.width - 24f, fuelHeadingRect.height / 2f, fuelUsers[i]);
                    Widgets.EndGroup();
                }
                else
                {
                    fuelSay = "this resource is not a fuel source for any buildings.".CapitalizeFirst();
                }
            }
            return isFuel;
        }
        private static bool IsAmmoSource(Rect ammoSection)
        {

            Rect ammoHeadingRect = new Rect(ammoSection.x + 1f, ammoSection.y + 2f, ammoSection.width, HeaderSize);
            String ammoSay = "";
            bool isAmmo = false;

            Widgets.BeginGroup(ammoHeadingRect);
            Text.Font = GameFont.Medium;
            GUI.color = Color.white;
            Widgets.Label(ammoHeadingRect, ammoSay.CapitalizeFirst());
            GUI.color = Color.white;
            Widgets.DrawLineHorizontal(ammoHeadingRect.x + 1f, HeaderSize + 2f, ammoHeadingRect.width - 1f);
            Widgets.EndGroup();

            List<ThingDef> ammoUsers = (List<ThingDef>)thingDefList.Where((ThingDef thingDef) => thingDef.IsWithinCategory(ThingCategoryDefOf.Manufactured));

            for (int i = 0; 1 < ammoUsers.Count(); i++)
            {
                if (ammoUsers[i] != null && (ammoUsers[i].IsApparel || ammoUsers[i].IsWeapon) && (ammoApparelUsersProps.ammoDef == rescToExamine ||
                    ammoWeaponUsersProps.ammoDef == rescToExamine))
                {
                    isAmmo = true;
                    ammoSay = "this resource is used as ammunition for the following items: ".CapitalizeFirst();
                    Rect ammoUserListRect = new Rect(ammoHeadingRect.x, ammoHeadingRect.height + 5f, ammoHeadingRect.width, ammoHeadingRect.height);

                    Widgets.BeginGroup(ammoUserListRect);
                    Widgets.LabelWithIcon(ammoUserListRect, ammoUsers[i].LabelCap, Widgets.GetIconFor(ammoUsers[i]), 1f);
                    Text.Font = GameFont.Small;
                    GUI.color = Color.white;
                    Widgets.InfoCardButton(ammoUserListRect.width - 24f, ammoHeadingRect.height / 2f, ammoUsers[i]);
                    Widgets.EndGroup();
                }
                else
                {
                    ammoSay = "there are no apparel or weapons using this resource as ammunition.".CapitalizeFirst();
                }
            }
            return isAmmo;
        }
    }
}
