using System;
using System.Collections.Generic;
using System.Text;

namespace CocktailAppV10.Services
{
    public interface ILocalFileHelper {
        string GetLocalFilePath(string fileName);  
    }
}
