using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using LanguaGo.Core.Domain;
using LanguaGo.Core.Repositories;

namespace LanguaGo.Infrastructure.Repositories
{
    public class UserRepository : IUserRepository
    {
        public SqlConnection connection = new SqlConnection("");

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
            string sql = "INSERT INTO Users Values(@Id, @Email, @Password, @Role, @CreatedAt);";
            
            connection.Open();
            
            connection.Execute(sql, new {
                Id = user.Id, 
                Email = user.Email,
                Password = user.Password,
                Role = user.Role,
                CreatedAt = user.CreatedAt
                });
                
            connection.Close();

            await Task.CompletedTask;
        }

        public async Task UpdateAsync(User user)
        {
            string sql = "UPDATE Users SET Email = @Email, Password = @Password WHERE Id = @Id;";

            connection.Open();            

            connection.Execute(sql, new {
                Email = user.Email,
                Password = user.Password,
                Id = user.Id
            });

            connection.Close();

            await Task.CompletedTask;
        }

        public async Task DeleteAsync(User user)
        {
            string sql = "DELETE FROM Users WHERE Id = @Id;";

            connection.Open();            

            connection.Execute(sql, new {
                Id = user.Id
            });

            connection.Close();

            await Task.CompletedTask;
        }
    }
}