using System;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using Xamarin.Forms;
using CocktailAppV10.Models;

namespace CocktailAppV10.Views
{
    public partial class SignUpPage : ContentPage
	{
		public SignUpPage ()
		{
			InitializeComponent ();
		}

		async void OnSignUpButtonClicked (object sender, EventArgs e)
		{
            var user = new User{
                FirstName = fnameEntry.Text,
                SurName = snameEntry.Text,
                Password = Encrypt(passwordEntry.Text),
                Email = emailEntry.Text.ToLower(),
                BirthDay = birthDay.Date
			};
            
            if (ToS.Checked == true)
            {
                bool MailExists = App.Database.EmailExists(emailEntry.Text);

                if (MailExists == false)
                {
                    await App.Database.SaveUserAsync(user);

                    var signUpSucceeded = AreDetailsValid(user);
                    if (signUpSucceeded)
                    {
                        var rootPage = Navigation.NavigationStack.FirstOrDefault();
                        if (rootPage != null)
                        {
                            App.IsUserLoggedIn = true;
                            var FetchedUser = await App.Database.GetUserAsync(user.Email);
                            Navigation.InsertPageBefore(new MainPage(FetchedUser), Navigation.NavigationStack.First());
                            await Navigation.PopToRootAsync();
                        }
                    }
                    else
                    {
                        messageLabel.Text = "Sign up failed";
                    }
                }
                else {
                    messageLabel.Text = "Email already exists in the server!";
                }
            }
            else {
                messageLabel.Text = "You need to agree with our TOS";
            }
		}

        static readonly string PasswordHash = "P@@Sw0rd";
        static readonly string SaltKey = "S@LT&KEY";
        static readonly string VIKey = "@1B2c3D4e5F6g7H8";
        public static string Encrypt(string plainText)
        {
            byte[] plainTextBytes = Encoding.UTF8.GetBytes(plainText);

            byte[] keyBytes = new Rfc2898DeriveBytes(PasswordHash, Encoding.ASCII.GetBytes(SaltKey)).GetBytes(256 / 8);
            var symmetricKey = new RijndaelManaged() { Mode = CipherMode.CBC, Padding = PaddingMode.Zeros };
            var encryptor = symmetricKey.CreateEncryptor(keyBytes, Encoding.ASCII.GetBytes(VIKey));

            byte[] cipherTextBytes;

            using (var memoryStream = new MemoryStream())
            {
                using (var cryptoStream = new CryptoStream(memoryStream, encryptor, CryptoStreamMode.Write))
                {
                    cryptoStream.Write(plainTextBytes, 0, plainTextBytes.Length);
                    cryptoStream.FlushFinalBlock();
                    cipherTextBytes = memoryStream.ToArray();
                    cryptoStream.Close();
                }
                memoryStream.Close();
            }
            return Convert.ToBase64String(cipherTextBytes);
        }
        bool AreDetailsValid (User user)
		{
			return (!string.IsNullOrWhiteSpace (user.Password) && !string.IsNullOrWhiteSpace (user.Email) && user.Email.Contains ("@"));
		}

        async void OnEulaClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new EulaPage());
        }
    }
}
