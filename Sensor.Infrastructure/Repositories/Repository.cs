using Microsoft.EntityFrameworkCore;
using Sensor.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Sensor.Infrastructure.Repositories
{
    public class Repository<T> : IRepository<T> where T : EntityBase
    {
        public SensorDBContext context { get; }

        public Repository(SensorDBContext context)
        {
            this.context = context;
        }

        public async Task<IReadOnlyCollection<T>> GetAllAsync(CancellationToken cancellationToken = default)
        {
            var query = context.Set<T>().AsQueryable();
            return await query.ToListAsync(cancellationToken)
                            .ConfigureAwait(false);
        }

        public async Task<IReadOnlyCollection<T>> GetLatestRecordsAsync(int count, CancellationToken cancellationToken = default)
        {
            var query = context.Set<T>().AsQueryable();
            return await query.OrderByDescending(t => t.Id).Take(count).ToListAsync(cancellationToken)
                            .ConfigureAwait(false);
        }

        public async Task<T> GetLatestRecordAsync(CancellationToken cancellationToken = default)
        {
            var query = context.Set<T>().AsQueryable();
            return await query.OrderByDescending(t => t.Id).FirstOrDefaultAsync()
                            .ConfigureAwait(false);
        }

        public async Task<T> AddAsync(T entity, CancellationToken cancellationToken = default)
        {
           var newTemperatureHistory = await context.Set<T>().AddAsync(entity, cancellationToken).ConfigureAwait(false);
            return newTemperatureHistory.Entity;
        }

        public async Task<int> SaveAsync(CancellationToken cancellationToken = default)
        {
            return await context.SaveChangesAsync(cancellationToken).ConfigureAwait(false);
        }
    }
}
