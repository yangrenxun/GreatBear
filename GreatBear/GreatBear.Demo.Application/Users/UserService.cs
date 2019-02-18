using AutoMapper;
using GreatBear.AutoMapper;
using GreatBear.Core.Application.Service;
using GreatBear.Core.Domain.Repositories;
using GreatBear.Core.Mvc.Paging;
using GreatBear.Core.Security;
using GreatBear.Demo.Application.Users.Dto;
using GreatBear.Demo.Entities.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace GreatBear.Demo.Application.Users
{
    public class UserService : ApplicationService, IUserService
    {
        private readonly IRepository<User, int> _userRepository;
        private readonly IEncryptionService _encryptionService;
        private readonly IMapper _mapper;

        /// <inheritdoc />
        public UserService(IRepository<User, int> userRepository, IEncryptionService encryptionService, IMapper mapper)
        {
            _userRepository = userRepository;
            _encryptionService = encryptionService;
            _mapper = mapper;
        }

        public IPagedList<User> PageList(int pageIndex, int pageSize, Expression<Func<User, bool>> otherPredicate)
        {
            var query = _userRepository.GetQueryable(otherPredicate);

            return new PagedList<User>(query.OrderByDescending(x => x.Id), pageIndex, pageSize);

        }

        public User GetUserByUsername(string username)
        {
            if (string.IsNullOrEmpty(username))
                return null;

            return _userRepository.FirstOrDefault(x => x.UserName == username);
        }

        public bool ValidateUser(string username, string password)
        {
            if (string.IsNullOrEmpty(username))
                return false;

            var user = _userRepository.FirstOrDefault(x => x.UserName == username);

            if(user == null)
                return false;

            var encryptPassword = _encryptionService.CreatePasswordHash(password, user.PasswordSalt);

            if (encryptPassword != user.Password)
                return false;

            return true;
        }

        public bool CheckExistUserName(string username)
        {
            if (string.IsNullOrEmpty(username))
                return false;

            var user = _userRepository.FirstOrDefault(x => x.UserName == username);
            if (user != null)
                return true;

            return false;
        }

        /// <inheritdoc />
        public User CreateUser(UserModel model)
        {
            var user = _mapper.Map<User>(model);

            user.CreateTime = DateTime.Now;
            var saltKey = _encryptionService.CreateSaltKey(5);
            user.PasswordSalt = saltKey;
            user.Password = _encryptionService.CreatePasswordHash(model.Password, saltKey);

            user = _userRepository.Insert(user);

            return user;
        }


    }
}
