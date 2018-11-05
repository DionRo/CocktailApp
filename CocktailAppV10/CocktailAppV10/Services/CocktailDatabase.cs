using SQLite;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CocktailAppV10.Models;
using CocktailAppV10.ViewModel;
using Newtonsoft.Json;

namespace CocktailAppV10.Services
{
    public class CocktailDatabase
    {
        readonly SQLiteAsyncConnection database;
        public CocktailDatabase(string dbPath)
        {
            database = new SQLiteAsyncConnection(dbPath);

            try
            {
                database.CreateTableAsync<User>().Wait();
                database.CreateTableAsync<Achievement>().Wait();
                database.CreateTableAsync<AchievementInfo>().Wait();
                database.CreateTableAsync<Allergie>().Wait();
                database.CreateTableAsync<AllergieInfo>().Wait();
                database.CreateTableAsync<Category>().Wait();
                database.CreateTableAsync<CategoryInfo>().Wait();
                database.CreateTableAsync<Cocktail>().Wait();
                database.CreateTableAsync<Glass>().Wait();
                database.CreateTableAsync<Ingredient>().Wait();
                database.CreateTableAsync<IngredientInfo>().Wait();
            }
            catch (Exception e)
            {
                int a = 1;
            }
        }
        public void FetchData(string achievementDataString, string allergieDataString, string allergieInfoDataString, string categoryDataString, string categoryInfoDataString, string cocktailDataString,string glassDataString, string ingredientDataString, string ingredientInfoDataString)
        {
            bool DataExists = DataCount();

            if (DataExists != true)
            {
                
                Achievement[] AchievementData = JsonConvert.DeserializeObject<Achievement[]>(achievementDataString);
                Allergie[] AllergieData = JsonConvert.DeserializeObject<Allergie[]>(allergieDataString);
                AllergieInfo[] AllergieInfoData = JsonConvert.DeserializeObject<AllergieInfo[]>(allergieInfoDataString);
                Category[] CategoryData = JsonConvert.DeserializeObject<Category[]>(categoryDataString);
                CategoryInfo[] CategoryInfoData = JsonConvert.DeserializeObject<CategoryInfo[]>(categoryInfoDataString);
                Cocktail[] CocktailData = JsonConvert.DeserializeObject<Cocktail[]>(cocktailDataString);
                Glass[] GlassData = JsonConvert.DeserializeObject<Glass[]>(glassDataString);
                Ingredient[] IngredientData = JsonConvert.DeserializeObject<Ingredient[]>(ingredientDataString);
                IngredientInfo[] IngredientInfoData = JsonConvert.DeserializeObject<IngredientInfo[]>(ingredientInfoDataString);
                ImportAllData(AchievementData , AllergieData , AllergieInfoData,
                              CategoryData , CategoryInfoData , CocktailData,
                              GlassData , IngredientData , IngredientInfoData
                              );
            }
        }
        async public void ImportAllData(Achievement[] achievements, Allergie[] allergies, AllergieInfo[] allergieInfos , Category[] categories, CategoryInfo[] categoryInfos ,Cocktail[] cocktails,Glass[] glasses, Ingredient[] ingredients , IngredientInfo[] ingredientInfos) {
           
            await database.InsertAllAsync(achievements);
            await database.InsertAllAsync(allergies);
            await database.InsertAllAsync(allergieInfos);
            await database.InsertAllAsync(categories);
            await database.InsertAllAsync(categoryInfos);
            await database.InsertAllAsync(cocktails);
            await database.InsertAllAsync(glasses);
            await database.InsertAllAsync(ingredients);
            await database.InsertAllAsync(ingredientInfos);
     
        }
        public bool DataCount()
        {
            var Query = database.Table<Cocktail>().CountAsync();
            Query.Wait();

            var result = Query.Result;
            return result != 0;
        }
        public Task<List<User>> GetUsersAsync()
        {
            return database.Table<User>().ToListAsync();
        }
        public bool EmailExists(string email) {
            var Query = database.Table<User>().Where(i => i.Email == email).CountAsync();
            Query.Wait();

            var result = Query.Result;

            return result != 0;
        }
        public Task<User> GetUserAsync(string email)
        {
            return database.Table<User>().Where(i => i.Email == email).FirstOrDefaultAsync();
        }
        public Task<int> SaveUserAsync(User user)
        {
            if (user.Id == 0)
            {
                 database.InsertAsync(user).Wait();
                return database.UpdateAsync(user);    
            }
            else
            {
                return database.UpdateAsync(user);
            }
        }
        public Task<int> DeleteUser(User user)
        {
            return database.DeleteAsync(user);
        }
        public Task<List<Cocktail>> FetchAllCocktails(bool allowAllCocktails) {
            if (allowAllCocktails)
            {
                return database.Table<Cocktail>().ToListAsync();
            }
            else
            {
                return database.Table<Cocktail>().Where(i => i.HasAlcohol == 0).ToListAsync();
            }
        }
        public  List<CocktailIngredientsViewModel> FetchCocktailIngredientsAsync(int id)
        {
            var  q = database.QueryAsync<CocktailIngredientsViewModel>(
          "select Name , AmountOfIngredient from Ingredient"
          + " inner join IngredientInfo"
          + " on Ingredient.Id = IngredientInfo.IngredientId where IngredientInfo.CocktailId = ?", id);
            q.Wait();

            return q.Result;
        }
        public List<CocktailAllergieViewModel> FetchCocktailAllergies(int id)
        {
            var q = database.QueryAsync<CocktailAllergieViewModel>(
            "select Name from Allergie"
            + " inner join AllergieInfo"
            + " on Allergie.Id = AllergieInfo.AllergieId where AllergieInfo.CocktailId = ?", id);
            q.Wait();

            return q.Result;
        }
        public Task<Glass> GetGlassName(int glassId)
        {
            return database.Table<Glass>().Where(i => i.Id == glassId).FirstOrDefaultAsync();
        }
        public List<Cocktail> SearchCocktail(string input , bool allowAllCocktails) {

            if (allowAllCocktails)
            {
                var q = database.QueryAsync<Cocktail>("SELECT * FROM Cocktail WHERE Name LIKE ?", $"%{input}%");
                q.Wait();
                return q.Result;
            }
            else
            {
                var q = database.QueryAsync<Cocktail>("SELECT * FROM Cocktail WHERE Name LIKE ? AND hasAlcohol = 0", $"%{input}%");
                q.Wait();
                return q.Result;
            }
        }
    }
}
