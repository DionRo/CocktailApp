using CocktailAppV10.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace CocktailAppV10.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class CocktailDetail : ContentPage
	{
		public CocktailDetail (Cocktail cocktail)
		{
		   InitializeComponent ();

            var GlassName = App.Database.GetGlassName(cocktail.GlassId);
            var Allergies = App.Database.FetchCocktailAllergies(cocktail.Id);
            var Ingredients = App.Database.FetchCocktailIngredientsAsync(cocktail.Id);

            AllergiesListView.ItemsSource = Allergies;
            IngredientsListView.ItemsSource = Ingredients;
            GlassN.Text = "Glass: " + GlassName.Result.Name;
            BindingContext = cocktail;
    

        }
	}
}