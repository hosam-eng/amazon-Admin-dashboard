using AmazonAdmin.Application.Contracts;
using AmazonAdmin.Context;
using AmazonAdmin.Domain;
using Microsoft.EntityFrameworkCore;

namespace AmazonAdmin.Infrastructure
{
    public class Reposatory<T, Tid> : IReposatory<T, Tid>where T : class
    {
        readonly ApplicationContext _Context;
        readonly DbSet<T> _Dbset;
        public Reposatory(ApplicationContext context) 
        {
            _Context = context;
            _Dbset = _Context.Set<T>();
        }

		public async Task<T> CreateAsync(T item)
		{
			var res = (await _Dbset.AddAsync(item)).Entity;
			//await SaveChangesAsync();
			return res;
		}



		public Task<IQueryable<T>> GetAllAsync()
        {
            return (Task.FromResult(_Dbset.Select(p => p)));
        }

        public async Task<T> GetByIdAsync(Tid id)
        {
            return (await _Dbset.FindAsync(id));
        }
		public async Task<bool> UpdateAsync(T Entity)
		{
			var res = _Dbset.Update(Entity);
			return res != null ? true : false;
		}
        public async Task<bool> DeleteAsync(Tid id)
        {
            var item = await _Dbset.FindAsync(id);
            var res = _Dbset.Remove(item);
            return res != null ? true : false;
        }
        public async Task<int> SaveChangesAsync()
        {
            return (await _Context.SaveChangesAsync());
        }

    }
}