using AmazonAdmin.Application.Contracts;
using AmazonAdmin.DTO;
using AmazonAdmin.Context;
using AmazonAdmin.Domain;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace AmazonAdmin.Infrastructure

{
    public class OrderRepository : Reposatory<Order, int>, IOrderReposatory
    {
        private readonly ApplicationContext context;
        private readonly DbSet<Order> dbset;

        public OrderRepository(ApplicationContext context) : base(context)
        {
            this.context = context;
            dbset = this.context.Set<Order>();
        }
        public async Task<List<Order>> getAllOrdersByUserId(string id)
        {
            var orders =await dbset.Include(O=>O.OrderItems).Where(O => O.UserId == id).ToListAsync();
            return orders;
        }
    }
}
