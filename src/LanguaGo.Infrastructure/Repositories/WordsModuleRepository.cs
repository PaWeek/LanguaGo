using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using LanguaGo.Core.Domain;
using LanguaGo.Core.Repositories;
using Z.Dapper.Plus;

namespace LanguaGo.Infrastructure.Repositories
{
    public class WordsModuleRepository : IWordsModuleRepository
    {
        public SqlConnection connection = new SqlConnection(@"Data Source=DESKTOP-ST1FCME\SQLEXPRESS;" +
            "Initial Catalog=LanguaGoDatabase;Integrated Security=true;");

        public async Task<IEnumerable<WordsModule>> GetAllAsync(Guid userId)
        {
            string sql = "SELECT * FROM WordsModules WHERE UserId = @UserId;";

            connection.Open();

            using (var multi = connection.QueryMultiple(sql, new {UserId = userId}))
            {
                var wordsModules = multi.Read<WordsModule>().ToList();
                
                connection.Close();

                return await Task.FromResult(wordsModules);
            }
        }

        public async Task<WordsModule> GetAsync(Guid userId, string name)
        {
            string query = "SELECT * FROM WordsModules WHERE UserId = @UserId AND Name = @Name;";

            connection.Open();

            var wordsModule = connection.QueryFirstOrDefaultAsync<WordsModule>(query, new {UserId = userId, Name = name}).Result;

            connection.Close();

            return await Task.FromResult(wordsModule);
        }

        public async Task AddAsync(WordsModule module)
        {
            await connection.OpenAsync();

            DapperPlusManager.Entity<WordsModule>().Table("WordsModules");

            connection.BulkInsert(module);

            connection.Close();

            await Task.CompletedTask;
        }

        public async Task UpdateAsync(WordsModule module)
        {
            await connection.OpenAsync();

            DapperPlusManager.Entity<WordsModule>().Table("WordsModules");

            connection.BulkUpdate(module);

            connection.Close();

            await Task.CompletedTask;
        }

        public async Task DeleteAsync(WordsModule module)
        {
            connection.Open();
            
            string query = "SELECT * FROM WordsModules WHERE Name = @Name;";

            DapperPlusManager.Entity<WordsModule>().Table("WordsModules").Key("Name");

            connection.BulkDelete(connection.Query<WordsModule>(query, new {Name = module.Name}).ToList());

            connection.Close();

            await Task.CompletedTask;
        }

        public Task AddTermAsync(Term term)
        {
            throw new NotImplementedException();
        }
        
        public Task UpdateTermAsync(Term term)
        {
            throw new NotImplementedException();
        }

        public Task DeleteTermAsync(Term term)
        {
            throw new NotImplementedException();
        }
    }
}