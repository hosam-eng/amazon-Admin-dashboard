using AmazonAdmin.Application.Contracts;
using AmazonAdmin.Domain;
using AmazonAdmin.DTO;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AmazonAdmin.Application.Services
{
    public class UserService : IUserService
    {
        private readonly IUserReposatory _repo;
        private readonly IMapper _mapper;
        private readonly UserManager<ApplicationUser> _UserManager;
        public UserService(UserManager<ApplicationUser> UserManager, IUserReposatory repo, IMapper mapper)
        {
            _UserManager=UserManager;
            _repo = repo;
            _mapper = mapper;
        }

        public Task<UserRegisterDTO> Login(string phone)
        {
            throw new NotImplementedException();
        }

        public async Task<string> UserName(string id)
        {
            var User = await _UserManager.FindByIdAsync(id);
            string name = User.UserName;
            return name;
        }
    }
}
