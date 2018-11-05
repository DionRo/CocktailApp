using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using CocktailAppV10.Droid;
using CocktailAppV10.Services;
using Xamarin.Forms;

[assembly: Dependency(typeof(LocalFileHelper))]
namespace CocktailAppV10.Droid
{
    class LocalFileHelper : ILocalFileHelper
    {
        public string GetLocalFilePath(string fileName)
        {
            string libFolder = Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.LocalApplicationData), fileName);

            if (!Directory.Exists(libFolder)) {
                Directory.CreateDirectory(libFolder);
            }
            //return Path.Combine(libFolder, fileName);
            return Path.Combine(libFolder, fileName);
        }
    }
}