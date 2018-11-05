using CocktailAppV10.Models;
using CocktailAppV10.Resources;
using Plugin.Multilingual;
using System;
using System.Collections.ObjectModel;
using System.Globalization;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using Xamarin.Forms;

namespace CocktailAppV10.Views
{
    public partial class LoginPage : ContentPage
    {
        public ObservableCollection<Language> Languages { get; }
        public LoginPage()
        { 
            InitializeComponent();

            Languages = new ObservableCollection<Language>()
            {
                new Language { DisplayName =  "Dutch", ShortName = "nl" },
                new Language { DisplayName =  "English", ShortName = "en" },
            };

            BindingContext = this;

            PickerLanguages.SelectedIndexChanged += PickerLanguages_SelectedIndexChanged;
        }

        private void PickerLanguages_SelectedIndexChanged(object sender, EventArgs e)
        {
            var language = Languages[PickerLanguages.SelectedIndex];

            try
            {
                var culture = new CultureInfo(language.ShortName);
                AppResources.Culture = culture;
                CrossMultilingual.Current.CurrentCultureInfo = culture;
            }
            catch (Exception)
            {
            }
            
            labelUser.Text = AppResources.Email;
            labelPassword.Text = AppResources.Password;
            usernameEntry.Placeholder = AppResources.Email;
            passwordEntry.Placeholder = AppResources.Password;
            
        }

        async void OnSignUpButtonClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new SignUpPage());
        }

        async void OnLoginButtonClicked(object sender, EventArgs e)
        {
            var user = await App.Database.GetUserAsync(usernameEntry.Text.ToLower());
            if (user != null)
            {
                var DecryptedPassword = Decrypt(user.Password);
                var GivenPassword = passwordEntry.Text;

                bool isValid = DecryptedPassword.Equals(GivenPassword);
                if (isValid)
                {
                    App.IsUserLoggedIn = true;
                    Navigation.InsertPageBefore(new MainPage(user), this);
                    await Navigation.PopAsync();
                }
                else
                {
                    messageLabel.Text = "Credentials do not match!";
                    passwordEntry.Text = string.Empty;
                }
            }
            else
            {
                messageLabel.Text = "User does not exist";
                usernameEntry.Text = string.Empty;
                passwordEntry.Text = string.Empty;
            }

        }

        static readonly string PasswordHash = "P@@Sw0rd";
        static readonly string SaltKey = "S@LT&KEY";
        static readonly string VIKey = "@1B2c3D4e5F6g7H8";
        public static string Decrypt(string encryptedText)
        {
            byte[] cipherTextBytes = Convert.FromBase64String(encryptedText);
            byte[] keyBytes = new Rfc2898DeriveBytes(PasswordHash, Encoding.ASCII.GetBytes(SaltKey)).GetBytes(256 / 8);
            var symmetricKey = new RijndaelManaged() { Mode = CipherMode.CBC, Padding = PaddingMode.None };

            var decryptor = symmetricKey.CreateDecryptor(keyBytes, Encoding.ASCII.GetBytes(VIKey));
            var memoryStream = new MemoryStream(cipherTextBytes);
            var cryptoStream = new CryptoStream(memoryStream, decryptor, CryptoStreamMode.Read);
            byte[] plainTextBytes = new byte[cipherTextBytes.Length];

            int decryptedByteCount = cryptoStream.Read(plainTextBytes, 0, plainTextBytes.Length);
            memoryStream.Close();
            cryptoStream.Close();
            return Encoding.UTF8.GetString(plainTextBytes, 0, decryptedByteCount).TrimEnd("\0".ToCharArray());
        }
    }
}
