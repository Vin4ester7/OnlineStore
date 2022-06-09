using System.Data.SqlClient;
using System.Data;
using Dapper;
using OnlineStore.Interfaces;
using OnlineStore.Models;
using Microsoft.AspNetCore.Mvc;

namespace OnlineStore.Mocks
{
    public class GamesRepository : IGamesRepository
    {
        static public string TableName { get; set; }
        static public int ProductID { get; set; }

        string jsonString;
        public GamesRepository(string json)
        {
            jsonString = json;
        }

        Game? IGamesRepository.GetProduct(int productID, string tableName)
        {
            TableName = tableName;
            ProductID = productID;
            try
            {
                using (IDbConnection db = new SqlConnection(jsonString))
                {
                    string request = $"SELECT * FROM {tableName} WHERE Id=@productID";
                    return db.Query<Game>(request, new { productID }).FirstOrDefault();
                }
            }
            catch (SqlException)
            {
                return null;
            }
        }

        public List<Game> GetGamesByCategory(string tableName)
        {
            TableName = tableName;
            try
            {
                using (IDbConnection db = new SqlConnection(jsonString))
                {
                    string request = $"SELECT * FROM {tableName}";
                    return db.Query<Game>(request).ToList();
                }
            }
            catch (SqlException)
            {
                return null;
            }
        }

        public List<Game> GetAllGames()
        {
            try
            {
                using (IDbConnection db = new SqlConnection(jsonString))
                {
                    string request = $"SELECT * FROM Zombies UNION SELECT * FROM Survival UNION SELECT * FROM Strategy " +
                                     $"UNION SELECT * FROM Shooters UNION SELECT * FROM Horror UNION SELECT * FROM Fighting " +
                                     $"UNION SELECT * FROM Adventure UNION SELECT * FROM Russian";
                     return db.Query<Game>(request).ToList();
                }
            }
            catch
            {
                return null;
            }
        }

        [HttpPost]
        public void CreateGame(Game newGame, string tableName)
        {
            try
            {
                using (IDbConnection db = new SqlConnection(jsonString))
                {
                    string sqlQuery = $"Insert Into {tableName} (Id, Name, ShortDesc, LongDesc, Image, price, Avaliable, TotalViews, TotalSales) " +
                                      $"Values(@Id, @Name, @ShortDesc, @LongDesc, @Image, @price, @Avaliable, 0, 0)";
                    int rowsAffected = db.Execute(sqlQuery, newGame);
                }
            }
            catch (SqlException)
            {

            }
        }

        Game? IGamesRepository.GetGame(int id)
        {
            try
            {
                using (IDbConnection db = new SqlConnection(jsonString))
                {
                    return db.Query<Game>($"SELECT * FROM {TableName} WHERE Id=@id", new { id }).FirstOrDefault();
                }
            }
            catch (SqlException)
            {
                return null;
            }
        }

        public int GetFreeId()
        {
            using (IDbConnection db = new SqlConnection(jsonString))
            {
                var id = db.Query<int>($"SELECT MAX(Id) FROM {TableName}").FirstOrDefault();
                return id + 1;
            }
        }

        public void UpdateCategory(Game game)
        {
            try
            {
                using (IDbConnection db = new SqlConnection(jsonString))
                {
                    string updateQuery = $"UPDATE {TableName} SET Id=@Id, Name=@Name, ShortDesc=@ShortDesc, LongDesc=@LongDesc, Image=@Image, " +
                                         $"Price=@Price, Avaliable=@Avaliable, TotalViews=@TotalViews, TotalSales=@TotalSales  WHERE Id = @Id";
                    var result = db.Execute(updateQuery, game);
                }
            }
            catch (SqlException)
            {

            }
        }

        public void Delete(int id)
        {
            try
            {
                using (IDbConnection db = new SqlConnection(jsonString))
                {
                    var category = db.Query<Game>($"SELECT * FROM {TableName} WHERE Id=@id", new { id }).FirstOrDefault();
                    var sqlQueryCategory = $"DELETE FROM {TableName} WHERE Id = @id";
                    db.Execute(sqlQueryCategory, new { id });
                }
            }
            catch (SqlException)
            {

            }
        }
    }
}
