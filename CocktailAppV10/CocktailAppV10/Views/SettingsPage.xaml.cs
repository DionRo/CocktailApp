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
	public partial class SettingsPage : ContentPage
	{
        public User UserGlobal;
		public SettingsPage (User user)
		{
			InitializeComponent ();

            UserGlobal = user;
		}

        async public void OnAchievementButtonClick(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new AchievementPage(UserGlobal));
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

        private void Switch_Toggled(object sender, ToggledEventArgs e)
        {

        }

        private void Switch_Toggled_1(object sender, ToggledEventArgs e)
        {

        }

        private void Switch_Toggled_2(object sender, ToggledEventArgs e)
        {

        }
    }
}