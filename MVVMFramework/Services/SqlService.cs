using System.Data.SqlClient;

namespace MVVMFramework.Services;

/// <summary>
/// MS SQL 전용 서비스
/// </summary>
public class SqlService : DatabaseService
{
    public SqlService(string connectionString) : base(connectionString)
    {
        // 기본 Connection을 MS SqlConnection으로 생성
        Connection = new SqlConnection(ConnectionString);
        // 기본 Command를 MS SqlCommand로 생성
        Command = new SqlCommand();
    }
}
