using System;
using System.Collections.Generic;
using System.Text;

namespace CocktailAppV10.Models
{
    public class IngredientInfo
    {
        public int CocktailId { get; set; }
        public int IngredientId { get; set; }
        public string AmountOfIngredient { get; set; }
    }
}
