using Xamarin.Forms;
using CocktailAppV10.Services;
using CocktailAppV10.Views;

namespace CocktailAppV10.Models
{
	public class App : Application
	{
		public static bool IsUserLoggedIn { get; set; }
        static CocktailDatabase database;
		public App (string AchievementData , string AllergieDataString, string AllergieInfoDataString, 
                    string CategoryDataString, string CategoryInfoDataString ,string CocktailDataString,
                    string GlassDataString, string IngredientDataString, string IngredientInfoDataString
                   )
        {
            Database.FetchData(AchievementData , AllergieDataString , AllergieInfoDataString,
                               CategoryDataString, CategoryInfoDataString , CocktailDataString,
                               GlassDataString, IngredientDataString, IngredientInfoDataString
                              );
            MainPage = new NavigationPage (new LoginPage ());
		}
         public static CocktailDatabase Database
         {
            get
            {
                if (database == null)
                {
                    database = new CocktailDatabase(DependencyService.Get<ILocalFileHelper>().GetLocalFilePath("User.db3"));
                }
                return database;
            }
        }
		protected override void OnStart ()
		{
			// Handle when your app starts
		}

		protected override void OnSleep ()
		{
			// Handle when your app sleeps
		}

		protected override void OnResume ()
		{
			// Handle when your app resumes
		}
	}
}

