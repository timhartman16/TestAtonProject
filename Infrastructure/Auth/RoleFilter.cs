using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using Infrastructure.Exceptions;

namespace Infrastructure.Authorization
{
    public class RoleAttribute : TypeFilterAttribute
    {
        public RoleAttribute(string roles) : base(typeof(RoleFilter))
        {
            Arguments = new object[] { roles };
        }
    }

    public class RoleFilter : IAuthorizationFilter
    {
        readonly string _roles;

        public RoleFilter(string roles)
        {
            _roles = roles;
        }

        public void OnAuthorization(AuthorizationFilterContext context)
        {
            string[] rls = _roles.Split(',');
            if (!rls.Contains(context.HttpContext.User.Claims.FirstOrDefault().Value))
                throw new HttpErrorException(403, ErrorText.ForbidActionForThisUser);
        }
    }
}
