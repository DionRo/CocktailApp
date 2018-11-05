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
	public partial class AchievementPage : ContentPage
	{
        public User UserGlobal;
		public AchievementPage (User user)
		{
			InitializeComponent ();
            UserGlobal = user;
		}

        async public void OnSettingButtonClick(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new SettingsPage(UserGlobal));
        }
        async public void OnCocktailButtonClick(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new MainPage(UserGlobal));
        }
        async public void OnEulaButtonClick(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new EulaPage(UserGlobal));
        }
        async void OnLogoutButtonClicked(object sender, EventArgs e)
        {
            App.IsUserLoggedIn = false;
            Navigation.InsertPageBefore(new LoginPage(), this);
            await Navigation.PopAsync();
        }
    }
}