﻿using System;
using System.Collections.Generic;
using System.Text;
using SQLite;

namespace CocktailAppV10.Models
{
    public class Allergie
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public string Name { get; set; }
    }
}
