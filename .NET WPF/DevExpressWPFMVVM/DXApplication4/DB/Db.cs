// Infrastructure/SqlConnectionFactory.cs
using System.Data;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;

namespace DXApplication4.Infrastructure
{
    public interface ISqlConnectionFactory
    {
        IDbConnection Create();   // 필요 시 Open()은 호출부에서
    }

    public class SqlConnectionFactory : ISqlConnectionFactory
    {
        private readonly string _cs;
        public SqlConnectionFactory(IConfiguration cfg)
        {
            _cs = cfg.GetConnectionString("PDB")
                 ?? throw new System.Exception("ConnectionString 'PDB' not found.");
        }
        public IDbConnection Create() => new SqlConnection(_cs);
    }
}
