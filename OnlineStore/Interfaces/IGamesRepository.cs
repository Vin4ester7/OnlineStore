using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;
using Dapper;
using OnlineStore.Models;

namespace OnlineStore.Interfaces
{
    public interface IGamesRepository
    {
        //void CreateProduct(Product product);
        //void UpdateProduct(Product product);
        Game GetProduct(int productID, string tableName);
        List<Game> GetGamesByCategory(string tableName);
        List<Game> GetAllGames();
        void CreateGame(Game newGame, string tableName);
        int GetFreeId();
        Game? GetGame(int id);
        void UpdateCategory(Game game);
        void Delete(int id);
        //public List<Product> FindWord(string search);

    }
}
