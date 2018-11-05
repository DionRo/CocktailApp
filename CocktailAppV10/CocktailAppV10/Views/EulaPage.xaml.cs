using CocktailAppV10.Models;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace CocktailAppV10.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class EulaPage : ContentPage
    {
        public User UserGlobal;
        public EulaPage()
        {
            InitializeComponent();
        }
        public EulaPage(User user)
		{
			InitializeComponent ();

            UserGlobal = user;
		}
        async public void OnSettingButtonClick(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new SettingsPage(UserGlobal));
        }
        async public void OnAchievementButtonClick(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new AchievementPage(UserGlobal));
        }
        async public void OnCocktailButtonClick(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new MainPage(UserGlobal));
        }
        async void OnLogoutButtonClicked(object sender, EventArgs e)
        {
            App.IsUserLoggedIn = false;
            Navigation.InsertPageBefore(new LoginPage(), this);
            await Navigation.PopAsync();
        }
    }
}