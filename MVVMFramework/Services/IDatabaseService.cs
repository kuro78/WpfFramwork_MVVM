using System.Collections.Generic;
using System.Threading.Tasks;

namespace MVVMFramework.Services;

/// <summary>
/// IDatabaseService
/// </summary>
public interface IDatabaseService
{
    /// <summary>
    /// ConnectionString
    /// </summary>
    string ConnectionString { get; }

    /// <summary>
    /// GetDataAsync
    /// </summary>
    /// query를 실행해서 IList&lt;<typeparamref name="T"/>&gt;를 반환한다.
    /// </remarks>
    Task<IList<T>> GetDatasAsync<T>(string query) where T : class;
}
