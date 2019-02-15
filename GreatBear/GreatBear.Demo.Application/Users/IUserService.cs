using GreatBear.Core.Application.Service;
using GreatBear.Core.Mvc.Paging;
using GreatBear.Demo.Application.Users.Dto;
using GreatBear.Demo.Entities.User;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace GreatBear.Demo.Application.Users
{
    /// <summary>
    /// 用户服务
    /// </summary>
    public interface IUserService : IApplicationService
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="otherPredicate"></param>
        /// <returns></returns>
        IPagedList<User> PageList(int pageIndex, int pageSize, Expression<Func<User, bool>> otherPredicate);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="username"></param>
        /// <returns></returns>
        User GetUserByUsername(string username);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="username"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        bool ValidateUser(string username, string password);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="username"></param>
        /// <returns></returns>
        bool CheckExistUserName(string username);

        /// <summary>
        /// 新增用户信息
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        User CreateUser(UserModel model);
    }
}
