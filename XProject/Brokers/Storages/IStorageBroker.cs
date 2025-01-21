using System.Linq;
using System.Threading.Tasks;
using System;

namespace XProject.Brokers.Storages
{
    public partial interface IStorageBroker
    {
        public ValueTask<T> InsertAsync<T>(T entity) where T : class;
        public IQueryable<T> SelectAll<T>() where T : class;
        public ValueTask<T> SelectByIdAsync<T>(Guid id) where T : class;
        public ValueTask<T> UpdateAsync<T>(T entity) where T : class;
        public ValueTask<T> DeleteAsync<T>(T entity) where T : class;
    }
}