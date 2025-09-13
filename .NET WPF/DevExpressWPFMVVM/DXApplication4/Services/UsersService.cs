using System.Collections.Generic;
using System.Threading.Tasks;
using Dapper;
using DXApplication4.Infrastructure;
using Microsoft.Data.SqlClient;

namespace DXApplication4.Services
{
    public interface IUsersService
    {
        Task<IEnumerable<UserDto>> GetAllAsync();
        Task<int> InsertAsync(UserDto u);
        Task<int> UpdateAsync(UserDto u);
        Task<int> DeleteAsync(int userId);
    }
    public record UserDto(int User_Id, string Username, string Password);

    public class UsersService : IUsersService
    {
        private readonly ISqlConnectionFactory _factory;
        public UsersService(ISqlConnectionFactory factory) => _factory = factory;

        public async Task<IEnumerable<UserDto>> GetAllAsync()
        {
            using var con = _factory.Create() as SqlConnection;
            await con!.OpenAsync();
            const string sql = @"SELECT user_id AS User_Id, username, password FROM dbo.Users ORDER BY user_id DESC";
            return await con.QueryAsync<UserDto>(sql);
        }
        public async Task<int> InsertAsync(UserDto u)
        {
            using var con = _factory.Create() as SqlConnection; await con!.OpenAsync();
            const string sql = @"INSERT INTO dbo.Users(username, password) VALUES(@Username, @Password)";
            return await con.ExecuteAsync(sql, u);
        }
        public async Task<int> UpdateAsync(UserDto u)
        {
            using var con = _factory.Create() as SqlConnection; await con!.OpenAsync();
            const string sql = @"UPDATE dbo.Users SET username=@Username, password=@Password WHERE user_id=@User_Id";
            return await con.ExecuteAsync(sql, u);
        }
        public async Task<int> DeleteAsync(int userId)
        {
            using var con = _factory.Create() as SqlConnection; await con!.OpenAsync();
            const string sql = @"DELETE FROM dbo.Users WHERE user_id=@userId";
            return await con.ExecuteAsync(sql, new { userId });
        }
    }
}
