using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

#pragma warning disable 1591
namespace DDomain.Override
{
    /// <summary>
    /// 重新中文的出错信息
    /// https://docs.microsoft.com/en-us/aspnet/core/api/microsoft.aspnetcore.identity.identityerrordescriber
    /// </summary>
    public class ChineseIdentityErrorDescriber : IdentityErrorDescriber
    {
        public override IdentityError DefaultError()
        {
            return new IdentityError()
            {
                Code = nameof(DefaultError),
                Description = "错误"
            };
        }

        public override IdentityError ConcurrencyFailure()
        {
            return new IdentityError()
            {
                Code = "ConcurrencyFailure",
                Description = ""
            };
        }

        public override IdentityError PasswordMismatch()
        {
            return new IdentityError()
            {
                Code = "PasswordMismatch",
                Description = "密码不匹配。"
            };
        }

        public override IdentityError InvalidToken()
        {
            return new IdentityError()
            {
                Code = "InvalidToken",
                Description = "无效令牌。"
            };
        }

        public override IdentityError LoginAlreadyAssociated()
        {
            return new IdentityError()
            {
                Code = "LoginAlreadyAssociated",
                Description = "Login Already Associated。"
            };
        }

        public override IdentityError InvalidUserName(string userName)
        {
            IdentityError identityError = new IdentityError();
            identityError.Code = "InvalidUserName";
            string str = $"Brkernavnet '{userName}' er ikke gyldig. Det kan kun inneholde bokstaver og tall.";
            identityError.Description = str;
            return identityError;
        }

        public override IdentityError InvalidEmail(string email)
        {
            IdentityError identityError = new IdentityError();
            identityError.Code = "InvalidEmail";

            String result = "";
            if(string.IsNullOrEmpty(email))
            {
                result = $"邮箱不能为空无效。";
            }
            else
            {
                result = $"邮箱({email})无效。";
            }
            identityError.Description = result;
            return identityError;
        }

        public override IdentityError DuplicateUserName(string userName)
        {
            IdentityError identityError = new IdentityError();
            identityError.Code = "DuplicateUserName";
            string str = $"'{userName}' 用户名已存在。";
            identityError.Description = str;
            return identityError;
        }

        public override IdentityError DuplicateEmail(string email)
        {
            IdentityError identityError = new IdentityError();
            identityError.Code = "DuplicateEmail";
            string str = $"'{email}' 邮箱已存在。";
            identityError.Description = str;
            return identityError;
        }

        public override IdentityError InvalidRoleName(string role)
        {
            IdentityError identityError = new IdentityError();
            identityError.Code = "InvalidRoleName";
            string str = $"'{role}' 角色无效.";
            identityError.Description = str;
            return identityError;
        }

        public override IdentityError DuplicateRoleName(string role)
        {
            IdentityError identityError = new IdentityError();
            identityError.Code = "DuplicateRoleName";
            string str = $"'{role}' 角色名称已经存在。";
            identityError.Description = str;
            return identityError;
        }

        public new virtual IdentityError UserAlreadyHasPassword()
        {
            return new IdentityError()
            {
                Code = "UserAlreadyHasPassword",
                Description = "用户已经设置密码。"
            };
        }

        public override IdentityError UserLockoutNotEnabled()
        {
            return new IdentityError()
            {
                Code = "UserLockoutNotEnabled",
                Description = "没有开启用户锁定。"
            };
        }

        public override IdentityError UserAlreadyInRole(string role)
        {
            IdentityError identityError = new IdentityError();
            identityError.Code = "UserAlreadyInRole";
            string str = $"用户已经属于 '{role}'.";
            identityError.Description = str;
            return identityError;
        }

        public override IdentityError UserNotInRole(string role)
        {
            IdentityError identityError = new IdentityError();
            identityError.Code = "UserNotInRole";
            string str = $"用户不属于 '{role}'.";
            identityError.Description = str;
            return identityError;
        }

        public override IdentityError PasswordTooShort(int length)
        {
            IdentityError identityError = new IdentityError();
            identityError.Code = "PasswordTooShort";
            string str = $"密码长度必须大于{length}位。";
            identityError.Description = str;
            return identityError;
        }

        public override IdentityError PasswordRequiresNonAlphanumeric()
        {
            return new IdentityError()
            {
                Code = "PasswordRequiresNonAlphanumeric",
                Description = "密码必须包含非字母内容。"
            };
        }

        public override IdentityError PasswordRequiresDigit()
        {
            return new IdentityError()
            {
                Code = "PasswordRequiresDigit",
                Description = "密码必须包含数字。"
            };
        }

        public override IdentityError PasswordRequiresLower()
        {
            return new IdentityError()
            {
                Code = "PasswordRequiresLower",
                Description = "密码必须包含小写字母 (a-z)."
            };
        }

        public override IdentityError PasswordRequiresUpper()
        {
            return new IdentityError()
            {
                Code = "PasswordRequiresUpper",
                Description = "密码必须包含大写字母 (A-Z)."
            };
        }
    }
}