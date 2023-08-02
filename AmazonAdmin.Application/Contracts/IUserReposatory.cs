using AmazonAdmin.Application.Contracts;
using AmazonAdmin.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AmazonAdmin.Application.Contracts
{
    public interface IUserReposatory:IReposatory<ApplicationUser,string>
    {
        Task<ApplicationUser> LoginByPhoneNumber(string phone);
        Task<ApplicationUser> LoginByEmail(string phoneNumber);
    }
}
