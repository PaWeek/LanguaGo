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
    public class UserRepository : IUserRepository
    {
        public SqlConnection connection = new SqlConnection(@"Data Source=DESKTOP-ST1FCME\SQLEXPRESS;" +
            "Initial Catalog=LanguaGoDatabase;Integrated Security=true;");

        public async Task<User> GetAsync(string email)
        {
            string query = "SELECT * FROM Users WHERE Email = @Email;";

            connection.Open();

            var user = connection.QueryFirstOrDefaultAsync<User>(query, new {Email = email}).Result;

            connection.Close();

            return await Task.FromResult(user);
        }

        public async Task<User> GetAsync(Guid id)
        {
            string query = "SELECT * FROM Users WHERE Id = @Id;";

            connection.Open();

            var user = connection.QueryFirstOrDefaultAsync<User>(query, new {Id = id}).Result;

            connection.Close();

            return await Task.FromResult(user);
        }

        public async Task AddAsync(User user)
        {
            await connection.OpenAsync();

            DapperPlusManager.Entity<User>().Table("Users");

            connection.BulkInsert(user);

            connection.Close();

            await Task.CompletedTask;
        }

        public async Task UpdateAsync(User user)
        {
            await connection.OpenAsync();

            DapperPlusManager.Entity<User>().Table("Users");

            connection.BulkUpdate(user);

            connection.Close();

            await Task.CompletedTask;
        }

        public async Task DeleteAsync(User user)
        {
            await connection.OpenAsync();
            
            string query = "SELECT * FROM Users WHERE Email = @Email;";

            DapperPlusManager.Entity<User>().Table("Users").Key("Email");

            connection.BulkDelete(connection.Query<User>(query, new {Email = user.Email}).ToList());

            connection.Close();

            await Task.CompletedTask;
        }
    }
}