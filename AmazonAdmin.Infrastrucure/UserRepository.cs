using AmazonAdmin.Application.Contracts;
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
    public class UserRepository : Reposatory<ApplicationUser,string>,IUserReposatory
    {
        private ApplicationContext _Context;
        private DbSet<ApplicationUser> _Dbset;

        public UserRepository(ApplicationContext context) : base(context)
        {
            _Context = context;
            _Dbset = _Context.Set<ApplicationUser>();
        }

        public async Task<ApplicationUser> LoginByPhoneNumber(string phone)
        {
             return await _Dbset.FirstOrDefaultAsync(u => u.Phone == phone);
        }
        public async Task<ApplicationUser> LoginByEmail(string email)
        {
            return await _Dbset.FirstOrDefaultAsync(u => u.EmailAddress == email);
        }
    }
}
