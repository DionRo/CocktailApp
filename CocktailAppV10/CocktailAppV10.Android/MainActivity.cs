using System;

using Android.App;
using Android.Content.PM;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using CocktailAppV10.Models;
using System.IO;
using Newtonsoft.Json;

namespace CocktailAppV10.Droid
{
    [Activity(Label = "CocktailAppV10", Icon = "@mipmap/icon", Theme = "@style/MainTheme", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            TabLayoutResource = Resource.Layout.Tabbar;
            ToolbarResource = Resource.Layout.Toolbar;

            base.OnCreate(savedInstanceState);

     
            string AchievementDataString = LoadFile(Resource.Raw.achievement);
            string AllergieDataString = LoadFile(Resource.Raw.allergie);
            string AllergieInfoDataString = LoadFile(Resource.Raw.allergieinfo);
            string CategoryDataString = LoadFile(Resource.Raw.category);
            string CategoryInfoDataString = LoadFile(Resource.Raw.categoryinfo);
            string CocktailDataString = LoadFile(Resource.Raw.cocktail);
            string GlassDataString = LoadFile(Resource.Raw.glass);
            string IngredientDataString = LoadFile(Resource.Raw.ingredient);
            string IngredientInfoDataString = LoadFile(Resource.Raw.ingredientinfo);

            global::Xamarin.Forms.Forms.Init(this, savedInstanceState);
            LoadApplication(new App(AchievementDataString , AllergieDataString, AllergieInfoDataString, 
                                    CategoryDataString , CategoryInfoDataString , CocktailDataString,
                                    GlassDataString , IngredientDataString , IngredientInfoDataString
                                   ));
        }

        public string LoadFile(int ResourceId) {
            var InputStream = Resources.OpenRawResource(ResourceId);
            string StringContent = string.Empty;

            using (StreamReader sr = new StreamReader(InputStream)) {
                StringContent = sr.ReadToEnd();
            }
        
            return StringContent;
        }
    }
}