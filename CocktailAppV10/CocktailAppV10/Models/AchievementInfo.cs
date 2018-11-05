using System;
using System.Collections.Generic;
using System.Text;
using SQLite;

namespace CocktailAppV10.Models
{
    class AchievementInfo
    {
        public int AchievementId { get; set; }
        public int UserId { get; set; }
        public int RequirementTotal { get; set; }
        public bool IsAchieved { get; set; }
    }
}
