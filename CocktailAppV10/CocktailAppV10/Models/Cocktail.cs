using System;
using System.Collections.Generic;
using System.Text;
using SQLite;

namespace CocktailAppV10.Models
{
    public class Cocktail
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public int GlassId { get; set; }
        public int AlcoholPercentage { get; set; }
        public string Name { get; set; }
        public string ImageLocation { get; set; }
        public string Description { get; set; }
        public string WayToMake { get; set; }
        public int HasAlcohol { get; set; }
    }
}
