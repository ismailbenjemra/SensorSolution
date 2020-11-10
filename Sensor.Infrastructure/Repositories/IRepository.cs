using Sensor.Domain;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Sensor.Infrastructure.Repositories
{
    public interface IRepository<T> where T : EntityBase
    {
        Task<IReadOnlyCollection<T>> GetAllAsync(CancellationToken cancellationToken = default);
        Task<T> AddAsync(T entity, CancellationToken cancellationToken = default);
        Task<IReadOnlyCollection<T>> GetLatestRecordsAsync(int count, CancellationToken cancellationToken = default);
        Task<T> GetLatestRecordAsync(CancellationToken cancellationToken = default);
        Task<int> SaveAsync(CancellationToken cancellationToken = default);
    }
}