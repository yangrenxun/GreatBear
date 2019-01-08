using GreatBear.Demo.Application.Users.Dto;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace GreatBear.Demo.Application.Users
{
    /// <summary>
    /// 用户服务
    /// </summary>
    public interface IUserService
    {

        /// <summary>
        /// 新增用户信息
        /// </summary>
        Task<GetUserOutput> Add(AddUserInput input);
    }
}
