using System;
using Xamarin.Forms;
using CocktailAppV10.Models;
using System.Text.RegularExpressions;

namespace CocktailAppV10.Views
{
    public partial class MainPage : ContentPage
    {
       public bool AllowAllCocktails;
       public User UserGlobal;

       public MainPage(User user)
        {
            InitializeComponent();
            //WelcomeMessage.Text = String.Format("Hello {0} {1}", user.FirstName, user.SurName);
            SearchCocktail.IsVisible = false;

            UserGlobal = user;
            // Check alleen op jaar
            var today = DateTime.Today;
            var age = today.Year - user.BirthDay.Year;

            OnAppearing(user);
        }
       protected virtual void OnAppearing(User user) {

            base.OnAppearing();
            var today = DateTime.Today;
            var age = today.Year - user.BirthDay.Year;

            if (age < 18)
            {
                AllowAllCocktails = false;
            }
            else
            {
                AllowAllCocktails = true;
            }

            GetCocktails();
        }
       async private void GetCocktails()
        {
            ItemsListView.ItemsSource = await App.Database.FetchAllCocktails(AllowAllCocktails);
            SearchCocktail.IsVisible = true;
        }
       async private void ItemsListView_ItemSelected(object sender, SelectedItemChangedEventArgs e) {

            var Cocktail = e.SelectedItem as Cocktail;
            if (Cocktail == null)
                return;

            await Navigation.PushAsync(new CocktailDetail(Cocktail));

            ItemsListView.SelectedItem = null;

        }
       async private void SearchChanged(object sender, TextChangedEventArgs e)
        {
           // var replaced = Regex.Replace(e.NewTextValue, "[^a-zA-Z]", "");
            SearchCocktail.Text = Regex.Replace(e.NewTextValue, "[^a-zA-Z\\s]", "");

            if (e.NewTextValue.Length > 4)
            {
                var DataSource = App.Database.SearchCocktail(e.NewTextValue.ToString(), AllowAllCocktails);
                ItemsListView.ItemsSource = DataSource;
            }
            else if (e.NewTextValue.Length == 0) {
                var DataSource = await App.Database.FetchAllCocktails(AllowAllCocktails);
                ItemsListView.ItemsSource = DataSource;
            }
        }
       // Navigation Handlers
       async public void OnSettingButtonClick(object sender, EventArgs e) {
            await Navigation.PushAsync(new SettingsPage(UserGlobal));
        }
       async public void OnAchievementButtonClick(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new AchievementPage(UserGlobal));
        }
       async public void OnEulaButtonClick(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new EulaPage(UserGlobal));
        }
       async  void OnLogoutButtonClicked(object sender, EventArgs e)
        {
            App.IsUserLoggedIn = false;
            Navigation.InsertPageBefore(new LoginPage(), this);
            await Navigation.PopAsync();
        }

    }
}
