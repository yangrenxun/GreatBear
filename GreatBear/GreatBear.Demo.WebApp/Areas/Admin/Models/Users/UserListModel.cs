using GreatBear.Demo.Entities.User;
using GreatBear.Demo.WebApp.Mvc.UI.Paging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GreatBear.Demo.WebApp.Areas.Admin.Models.Users
{
    public class UserListModel : BasePageableModel<User>
    {
    }
}
