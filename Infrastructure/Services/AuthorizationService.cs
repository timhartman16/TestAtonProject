using Domain.UserAggregate;
using Infrastructure.Exceptions;
using Infrastructure.Hash;
using Infrastructure.Queries.GetUserByLogin;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;

namespace Infrastructure.Services
{
    public class AuthorizationService : IAuthorizationService
    {
        private readonly IUserRepository _userRepository;
        private readonly IGetUserByLoginQuery _getuserByLoginQuery;

        public AuthorizationService(IUserRepository userRepository, IGetUserByLoginQuery getUserByLoginQuery)
        {
            _userRepository = userRepository;
            _getuserByLoginQuery = getUserByLoginQuery;
        }

        public void isInSystem(HttpContext context)
        {
            var login = context.Request.Query["login"];
            var password = context.Request.Query["password"];

            if (!User.LatinOrNumbersValidation(login))
                throw new HttpErrorException(400, ErrorText.IncorrectLogin);
            if (!User.LatinOrNumbersValidation(password))
                throw new HttpErrorException(400, ErrorText.IncorrectPassword);

            var user = _getuserByLoginQuery.Execute(login);

            if (user == null)
                throw new HttpErrorException(404, ErrorText.UserNotFound);
            if (user.Password != HashConverter.ConvertPasswordToHash(password)) 
                throw new HttpErrorException(401, ErrorText.CorrectLoginIncorrectPassword);

            var identity = (ClaimsIdentity)context.User.Identity;
            if (user.Admin == true)
                identity.AddClaim(new Claim(ClaimTypes.Role, "admin"));
            else
                identity.AddClaim(new Claim(ClaimTypes.Role, "user"));
        }
    }
}
