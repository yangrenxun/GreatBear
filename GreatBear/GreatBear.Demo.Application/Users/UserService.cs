using GreatBear.AutoMapper;
using GreatBear.Core.Application.Service;
using GreatBear.Core.Domain.Repositories;
using GreatBear.Demo.Application.Users.Dto;
using GreatBear.Demo.Entities.User;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace GreatBear.Demo.Application.Users
{
    public class UserService : ApplicationService, IUserService
    {
        private readonly IRepository<User, int> _userRepository;

        /// <inheritdoc />
        public UserService(IRepository<User, int> userRepository)
        {
            _userRepository = userRepository;
        }

        /// <inheritdoc />
        public async Task<GetUserOutput> Add(AddUserInput input)
        {
            var user = new User()
            {
                UserName = input.UserName,
                Password = input.Password
            };
            user = await _userRepository.InsertAsync(user);

            return user.MapTo<GetUserOutput>();
        }
    }
}
