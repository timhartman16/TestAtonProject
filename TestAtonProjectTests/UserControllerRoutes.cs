using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestAtonProjectTests
{
    internal class UserControllerRoutes
    {
        public static string GetAllUsers = "/getAllUsers";
        public static string GetUserByLogin = "/getByLogin";
        public static string GetCurrentUserInfo = "/getCurrentUserInfo";
        public static string GetElderUsers = "/getElderUsers";
        public static string CreateUser = "/createUser";
        public static string UpdateUser = "/updateUser";
    }
}
