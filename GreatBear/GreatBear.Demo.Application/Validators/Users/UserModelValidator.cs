using FluentValidation;
using GreatBear.Demo.Application.Users.Dto;
using System;
using System.Collections.Generic;
using System.Text;

namespace GreatBear.Demo.Application.Validators.Users
{
    public class UserModelValidator : AbstractValidator<UserModel>
    {
        public UserModelValidator()
        {
            RuleFor(x=>x.UserName).NotEmpty().WithMessage("不能为空");
            RuleFor(x => x.Password).NotEmpty().WithMessage("不能为空");
            RuleFor(x => x.Phone).Length(11).WithMessage("必须11位");
        }
    }
}
