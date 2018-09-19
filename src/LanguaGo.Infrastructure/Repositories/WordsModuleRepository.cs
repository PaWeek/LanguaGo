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
    public class WordsModuleRepository : IWordsModuleRepository
    {
        public SqlConnection connection = new SqlConnection("");
        
        public async Task<WordsModule> GetAsync(Guid userId, string name)
        {
            string query = "SELECT * FROM WordsModules WHERE UserId = @UserId AND Name = @Name;";
            string sql = "SELECT * FROM Terms WHERE WordsModuleId = @Id;";

            connection.Open();

            var wordsModule = connection.QueryFirstOrDefaultAsync<WordsModule>(query, new {UserId = userId, Name = name}).Result;

            if (wordsModule != null)
            {
                using (var multi = connection.QueryMultiple(sql, new {Id = wordsModule.Id}))
                {
                    var terms = multi.Read<Term>().AsEnumerable();
                    wordsModule.GetTerms(terms);
                }
            }

            connection.Close();

            return await Task.FromResult(wordsModule);
        }

        public async Task<WordsModule> GetAsync(Guid userId, Guid id)
        {
            string query = "SELECT * FROM WordsModules WHERE UserId = @UserId AND Id = @Id;";
            string sql = "SELECT * FROM Terms WHERE WordsModuleId = @Id;";
            
            connection.Open();

            var wordsModule = connection.QueryFirstOrDefaultAsync<WordsModule>(query, new {UserId = userId, Id = id}).Result;


            if (wordsModule != null)
            {
                using (var multi = connection.QueryMultiple(sql, new {Id = id}))
                {
                    var terms = multi.Read<Term>().AsEnumerable();
                    wordsModule.GetTerms(terms);                
                }
            }

            connection.Close();

            return await Task.FromResult(wordsModule);
        }

        public async Task<IEnumerable<WordsModule>> BrowseAsync(Guid userId, string name = "")
        {
            string sql = "SELECT * FROM WordsModules WHERE UserId = @UserId;";

            connection.Open();

            using (var multi = connection.QueryMultiple(sql, new {UserId = userId}))
            {
                var wordsModules = multi.Read<WordsModule>().AsEnumerable();
                
                connection.Close();

                if (!string.IsNullOrWhiteSpace(name))
                {
                    wordsModules = wordsModules.Where(x => x.Name.ToLowerInvariant()
                        .Contains(name.ToLowerInvariant()));
                }

                return await Task.FromResult(wordsModules);
            }
        }

        public async Task AddAsync(WordsModule module)
        {
            string sql = "INSERT INTO WordsModules Values(@Id, @UserId, @Name, @Description, @CreatedAt);";
            
            connection.Open();
            
            connection.Execute(sql, new {
                Id = module.Id, 
                UserId = module.UserId,
                Name = module.Name,
                Description = module.Description,
                CreatedAt = module.CreatedAt
                });
                
            connection.Close();
            
            await Task.CompletedTask;
        }

        public async Task UpdateAsync(WordsModule module)
        {
            string sql = "UPDATE WordsModules SET Name = @Name, Description = @Description WHERE Id = @Id AND UserId = @UserId;";

            connection.Open();            

            connection.Execute(sql, new {
                Name = module.Name,
                Description = module.Description,
                Id = module.Id,
                UserId = module.UserId
            });

            connection.Close();

            await Task.CompletedTask;
        }

        public async Task DeleteAsync(WordsModule module)
        {
            string sql = "DELETE FROM WordsModule WHERE Id = @Id AND UserId = @UserId;";

            connection.Open();

            connection.Execute(sql, new {
                Id = module.Id,
                UserId = module.UserId
            });

            connection.Close();

            await Task.CompletedTask;
        }

        public async Task<Term> GetTermAsync(Guid moduleId, Guid id)
        {
            string query = "SELECT * FROM Terms WHERE UserId = @ModuleId AND Id = @Id;";

            connection.Open();

            var term = connection.QueryFirstOrDefaultAsync<Term>(query, new {ModuleId = moduleId, Id = id}).Result;

            connection.Close();

            return await Task.FromResult(term);
        }

        public async Task AddTermAsync(Term term)
        {
            string sql = "INSERT INTO Terms Values(@Id, @WordsModuleId, @Word, @Translation);";
            
            connection.Open();
            
            connection.Execute(sql, new {
                Id = term.Id, 
                WordsModuleId = term.WordsModuleId,
                Word = term.Word,
                Translation = term.Translation
                });
                
            connection.Close();

            await Task.CompletedTask;
        }

        public async Task DeleteTermAsync(Term term)
        {
            string sql = "DELETE FROM Terms WHERE Id = @Id AND WordsModuleId = @WordsModuleId;";

            connection.Open();

            connection.Execute(sql, new {
                Id = term.Id,
                WordsModuleId = term.WordsModuleId
            });

            connection.Close();

            await Task.CompletedTask;
        }
    }
}