
// 파일: Services/ChatLogsService.cs
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Dapper;
using DXApplication4.Infrastructure;
using Microsoft.Data.SqlClient;

namespace DXApplication4.Services
{
    public interface IChatLogsService
    {
        Task<IEnumerable<ChatLogDto>> QueryAsync(int? userId, string? logType, DateTime? from, DateTime? to);
        Task<int> DeleteAsync(int chatId);
        Task<IEnumerable<UserFilterDto>> GetUsersForFilterAsync();
    }
    public record ChatLogDto(
      int Chat_Id, int? User_Id, string Username, string Log_Type,
      string? Message, string? Response, string? Diagnosis_Result, DateTime Timestamp);
    public record UserFilterDto(int User_Id, string Username);

    public class ChatLogsService : IChatLogsService
    {
        private readonly ISqlConnectionFactory _factory;
        public ChatLogsService(ISqlConnectionFactory factory) => _factory = factory;

        public async Task<IEnumerable<ChatLogDto>> QueryAsync(int? userId, string? logType, DateTime? from, DateTime? to)
        {
            using var con = _factory.Create() as SqlConnection; await con!.OpenAsync();
            const string sql = @"
SELECT cl.chat_id AS Chat_Id, cl.user_id AS User_Id, ISNULL(u.username,'') AS Username,
       cl.log_type AS Log_Type, cl.message AS Message, cl.response AS Response,
       cl.diagnosis_result AS Diagnosis_Result, cl.[timestamp] AS [Timestamp]
FROM dbo.chat_logs cl
LEFT JOIN dbo.users u ON u.user_id = cl.user_id
WHERE (@UserId IS NULL OR cl.user_id = @UserId)
  AND (@LogType IS NULL OR cl.log_type = @LogType)
  AND (@From   IS NULL OR cl.[timestamp] >= @From)
  AND (@To     IS NULL OR cl.[timestamp] < DATEADD(day, 1, @To))
ORDER BY cl.chat_id DESC";
            return await con.QueryAsync<ChatLogDto>(sql, new
            {
                UserId = userId,
                LogType = string.IsNullOrWhiteSpace(logType) ? null : logType,
                From = from,
                To = to
            });
        }
        public async Task<int> DeleteAsync(int chatId)
        {
            using var con = _factory.Create() as SqlConnection; await con!.OpenAsync();
            const string sql = @"DELETE FROM dbo.chat_logs WHERE chat_id=@chatId";
            return await con.ExecuteAsync(sql, new { chatId });
        }
        public async Task<IEnumerable<UserFilterDto>> GetUsersForFilterAsync()
        {
            using var con = _factory.Create() as SqlConnection; await con!.OpenAsync();
            const string sql = @"SELECT user_id AS User_Id, username FROM dbo.Users ORDER BY username";
            return await con.QueryAsync<UserFilterDto>(sql);
        }
    }
}
