using System;
using SQLite;

namespace CocktailAppV10.Models
{
    [Table(nameof(User))]
    public class User
	{
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        [Column(nameof(FirstName))]
        public string FirstName { get; set; }
        [Column(nameof(SurName))]
        public string SurName { get; set; }
        [Column(nameof(Password))]
        public string Password { get; set; }
        [Unique,Column(nameof(Email))]
        public string Email { get; set; }
        [Column(nameof(BirthDay))]
        public DateTime BirthDay { get; set; }
	}
}
