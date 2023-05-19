using Data;
using Domain.UserAggregate;
using Infrastructure.DTO.Request;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using TestAtonProject;

namespace TestAtonProjectTests
{
    [TestFixture]
    public class UserControllerTests
    {
        WebApplicationFactory<Startup> WebHost { get; set; }
        TestAtonDbContext Db { get; set; }
        HttpClient Client { get; set; }

        List<User> users = new() {new User(Guid.Parse("2d7a96eb-6e11-483e-876a-07ce8a15e707"), "admin", "8C6976E5B5410415BDE908BD4DEE15DFB167A9C873FC4BB8A81F6F2AB448A918", "admin", 1,
                                           DateTime.Parse("1980-04-20 00:00:00"), true, DateTime.Parse("2020-04-20 00:00:00"), "admin"),
                                  new User(Guid.Parse("14f3daae-cee5-4065-a6df-44b2f1de2705"), "userok", "42C460C1185591F25C0F0BB5AC06D66E113FFB108C78709E464F8850140C1C15", "userok", 1,
                                           DateTime.Parse("1990-05-10 00:00:00"), false, DateTime.Parse("2020-04-20 00:00:00"), "admin"),
                                  new User(Guid.Parse("bcccbd19-a7f4-4237-95df-1bde5f029726"), "userka", "A76B7A9E9D566FB059034242FA272EF5BA7E47875EB0F1F9CA0778FE4A66667D", "userka", 0,
                                           DateTime.Parse("2000-09-01 00:00:00"), false, DateTime.Parse("2020-04-20 00:00:00"), "admin")};

        [OneTimeSetUp]
        protected void OneTimeSetUp()
        {
            WebHost = new WebApplicationFactory<Startup>().WithWebHostBuilder(builder =>
            {
                builder.ConfigureTestServices(services =>
                {
                    var dbContextDescriptor = services.SingleOrDefault(d =>
                        d.ServiceType == typeof(DbContextOptions<TestAtonDbContext>));

                    services.Remove(dbContextDescriptor);

                    services.AddDbContext<TestAtonDbContext>(options =>
                    {
                        options.UseInMemoryDatabase("users");
                    });
                });
            });

            Client = WebHost.CreateClient();
        }

        [SetUp]
        public void Setup()
        {
            Db = WebHost.Services.CreateScope().ServiceProvider.GetService<TestAtonDbContext>();
            Db.AddRange(users);
            Db.SaveChanges();
        }

        [TearDown]
        public void TearDown()
        {
            Db.Database.EnsureDeleted();
            Db.Database.EnsureCreated();
        }

        [TestCase("admi", "admin", ExpectedResult = HttpStatusCode.NotFound)]
        [TestCase("lala", "lala", ExpectedResult = HttpStatusCode.NotFound)]
        [TestCase("admin", "admi", ExpectedResult = HttpStatusCode.Unauthorized)]
        [TestCase("userok", "userok", ExpectedResult = HttpStatusCode.Forbidden)]
        [TestCase("юзер", "userok", ExpectedResult = HttpStatusCode.BadRequest)]
        [TestCase("userok", "userokю", ExpectedResult = HttpStatusCode.BadRequest)]
        public async Task<HttpStatusCode> CheckStatus_CreateUser(string login, string password)
        {
            string str = UserControllerRoutes.CreateUser + "?login=" + login + "&password=" + password;
            CreateUserRequestDto userToCreate = new CreateUserRequestDto()
            { 
                Login = "newUser",
                Password = "newUser",
                Name = "newUser",
                Gender = 0,
                Birthday = DateTime.Parse("1999-04-21 00:00:00"),
                IsAdmin = false
            };

            HttpResponseMessage responseMessage = await Client.PostAsJsonAsync(str, userToCreate);

            return responseMessage.StatusCode;
        }

        [TestCase("admin", "admin")]
        public async Task CheckStatus_CreateUser_ShouldReturnsOk(string login, string password)
        {
            string str = UserControllerRoutes.CreateUser + "?login=" + login + "&password=" + password;
            CreateUserRequestDto userToCreate = new CreateUserRequestDto()
            {
                Login = "newUser",
                Password = "newUser",
                Name = "newUser",
                Gender = 0,
                Birthday = DateTime.Parse("1999-04-21 00:00:00"),
                IsAdmin = false
            };

            HttpResponseMessage responseMessage = await Client.PostAsJsonAsync(str, userToCreate);

            Assert.AreEqual(HttpStatusCode.OK, responseMessage.StatusCode);
            Assert.AreEqual(4, Db.User.Count());
        }


        [TestCase("admi", "admin", "userok", "newUserName", ExpectedResult = HttpStatusCode.NotFound)]
        [TestCase("lala", "lala", "userok", "newUserName", ExpectedResult = HttpStatusCode.NotFound)]
        [TestCase("admin", "admi", "userok", "newUserName", ExpectedResult = HttpStatusCode.Unauthorized)]
        public async Task<HttpStatusCode> CheckStatus_UpdateUser(string login, string password, string userLoginToUpdate, string newUserName)
        {
            string str = UserControllerRoutes.UpdateUser + "?login=" + login + "&password=" + password;

            UpdateUserRequestDto dto = new UpdateUserRequestDto()
            {
                Login = userLoginToUpdate,
                Name = newUserName,
                Gender = 0,
                Birthday = DateTime.Now
            };

            HttpResponseMessage responseMessage = await Client.PutAsJsonAsync(str, dto);

            return responseMessage.StatusCode;
        }

        [TestCase("admin", "admin", "userok", "newUserName")]
        [TestCase("userok", "userok", "userok", "newUserName")]
        public async Task CheckStatus_UpdateUser_ShouldReturnsOk(string login, string password, string userLoginToUpdate, string newUserName)
        {
            string str = UserControllerRoutes.UpdateUser + "?login=" + login + "&password=" + password;

            UpdateUserRequestDto dto = new UpdateUserRequestDto()
            {
                Login = userLoginToUpdate,
                Name = newUserName,
                Gender = 0,
                Birthday = DateTime.Now
            };

            HttpResponseMessage responseMessage = await Client.PutAsJsonAsync(str, dto);

            Assert.AreEqual(HttpStatusCode.OK, responseMessage.StatusCode);
        }

        [TestCase("admi", "admin", ExpectedResult = HttpStatusCode.NotFound)]
        [TestCase("admin", "admi", ExpectedResult = HttpStatusCode.Unauthorized)]
        [TestCase("userok", "userok", ExpectedResult = HttpStatusCode.Forbidden)]
        public async Task<HttpStatusCode> CheckStatus_GetAllUsers(string login, string password)
        {
            string str = UserControllerRoutes.GetAllUsers + "?login=" + login + "&password=" + password;
            HttpResponseMessage responseMessage = await Client.GetAsync(str);

            return responseMessage.StatusCode;
        }

        [TestCase("admin", "admin")]
        public async Task CheckStatusAndUsersCount_GetAllUsers_ShouldReturnsOk(string login, string password)
        {
            string str = UserControllerRoutes.GetAllUsers + "?login=" + login + "&password=" + password;
            HttpResponseMessage responseMessage = await Client.GetAsync(str);

            var rez = await responseMessage.Content.ReadAsStringAsync();
            int usersCount = JsonConvert.DeserializeObject<List<User>>(rez).Count;

            Assert.AreEqual(HttpStatusCode.OK, responseMessage.StatusCode);
            Assert.AreEqual(users.Count, usersCount);
        }

        [TestCase("admin", "admi", "admin", ExpectedResult = HttpStatusCode.NotFound)]
        [TestCase("admin", "admin", "admi", ExpectedResult = HttpStatusCode.Unauthorized)]
        [TestCase("admin", "userok", "userok", ExpectedResult = HttpStatusCode.Forbidden)]

        public async Task<HttpStatusCode> CheckStatus_GetUserByLogin(string loginToFind, string login, string password)
        {
            string str = UserControllerRoutes.GetUserByLogin + "/" + loginToFind + "?login=" + login + "&password=" + password;
            HttpResponseMessage responseMessage = await Client.GetAsync(str);

            return responseMessage.StatusCode;
        }

        [TestCase("admin", "admin", "admin")]
        public async Task CheckStatus_GetUserByLogin_ShouldReturnOk(string loginToFind, string login, string password)
        {
            string str = UserControllerRoutes.GetUserByLogin + "/" + loginToFind + "?login=" + login + "&password=" + password;
            HttpResponseMessage responseMessage = await Client.GetAsync(str);

            var rez = await responseMessage.Content.ReadAsStringAsync();
            User user = JsonConvert.DeserializeObject<User>(rez);

            Assert.AreEqual(HttpStatusCode.OK, responseMessage.StatusCode);
            Assert.AreEqual("admin", user.Name);
        }


        [TestCase("admi", "admin", ExpectedResult = HttpStatusCode.NotFound)]
        [TestCase("lala", "lala", ExpectedResult = HttpStatusCode.NotFound)]
        [TestCase("admin", "admi", ExpectedResult = HttpStatusCode.Unauthorized)]
        public async Task<HttpStatusCode> CheckStatus_GetCurrentUserInfo(string login, string password)
        {
            string str = UserControllerRoutes.GetCurrentUserInfo + "?login=" + login + "&password=" + password;
            HttpResponseMessage responseMessage = await Client.GetAsync(str);

            return responseMessage.StatusCode;
        }

        [TestCase("admin", "admin")]
        [TestCase("userok", "userok")]
        public async Task CheckStatusAndUsersCount_GetCurrentUserInfo_ShouldReturnsOk(string login, string password)
        {
            string str = UserControllerRoutes.GetCurrentUserInfo + "?login=" + login + "&password=" + password;
            HttpResponseMessage responseMessage = await Client.GetAsync(str);

            var rez = await responseMessage.Content.ReadAsStringAsync();
            var user = JsonConvert.DeserializeObject<User>(rez);

            Assert.AreEqual(HttpStatusCode.OK, responseMessage.StatusCode);
            Assert.AreEqual(login, user.Login);
        }

        [TestCase(30, "admi", "admin", ExpectedResult = HttpStatusCode.NotFound)]
        [TestCase(30, "lala", "lala", ExpectedResult = HttpStatusCode.NotFound)]
        [TestCase(20, "admin", "admi", ExpectedResult = HttpStatusCode.Unauthorized)]
        [TestCase(30, "userok", "userok", ExpectedResult = HttpStatusCode.Forbidden)]
        public async Task<HttpStatusCode> CheckStatus_GetElderUsers(int age, string login, string password)
        {
            string str = UserControllerRoutes.GetElderUsers + "/" + age + "?login=" + login + "&password=" + password;
            HttpResponseMessage responseMessage = await Client.GetAsync(str);

            return responseMessage.StatusCode;
        }

        [TestCase(30, "admin", "admin")]
        [TestCase(20, "admin", "admin")]
        public async Task CheckStatusAndUsersCount_GetElderUsers_ShouldReturnsOk(int age, string login, string password)
        {
            string str = UserControllerRoutes.GetElderUsers + "/" + age + "?login=" + login + "&password=" + password;
            HttpResponseMessage responseMessage = await Client.GetAsync(str);

            var rez = await responseMessage.Content.ReadAsStringAsync();
            var user = JsonConvert.DeserializeObject<List<User>>(rez);

            Assert.AreEqual(HttpStatusCode.OK, responseMessage.StatusCode);
            Assert.AreEqual(users.Where(x => x.Birthday.Value < DateTime.Now.AddYears((-1) * age)).Count(), user.Count);
        }
    }
}
