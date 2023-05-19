using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Xml.Linq;
using static System.Net.WebRequestMethods;

namespace Domain.UserAggregate
{
    public class User
    {
        public Guid Guid { get; private set; }
        public string Login { get; private set; }
        public string Password { get; private set; }
        public string Name { get; private set; }
        public int Gender { get; private set; }
        public DateTime? Birthday { get; private set; }
        public bool Admin { get; private set; }
        public DateTime CreatedOn { get; private set; }
        public string CreatedBy { get; private set; }
        public DateTime? ModifiedOn { get; private set; }
        public string ModifiedBy { get; private set; }
        public DateTime? RevokedOn { get; private set; }
        public string RevokedBy { get; private set; }

        private User() { }

        public User(Guid guid, string login, string password, string name, int gender, 
                    DateTime? birthday, bool admin, DateTime createdOn, string createdBy, 
                    DateTime? modifiedOn = null, string modifiedBy = null, DateTime? revokedOn = null, string revokedBy = null)
        {
            Guid = guid;
            Login = login;
            Password = password;
            Name = name;
            Gender = gender;
            Birthday = birthday;
            Admin = admin;
            CreatedOn = createdOn;
            CreatedBy = createdBy;
            ModifiedOn = modifiedOn;
            ModifiedBy = modifiedBy;
            RevokedOn = revokedOn;
            RevokedBy = revokedBy;
        }

        public void UpdateUserProperties(string name, int gender, DateTime? birthday, string modifiedBy)
        {
            Name = name;
            Gender = gender;
            Birthday = birthday;
            ModifiedBy = modifiedBy;
            ModifiedOn = DateTime.Now;
        }

        public void UpdateUserPassword(string password, string modifiedBy)
        {
            Password = password;
            ModifiedBy = modifiedBy;
            ModifiedOn = DateTime.Now;
        }

        public void UpdateUserLogin(string newLogin, string modifiedBy)
        {
            Login = newLogin;
            ModifiedBy = modifiedBy;
            ModifiedOn = DateTime.Now;
        }

        public void SoftDelete(string revokedByLogin)
        {
            RevokedBy = revokedByLogin;
            RevokedOn = DateTime.Now;
        }

        public void RestoreUser(string modifiedBy)
        {
            ModifiedBy = modifiedBy;
            ModifiedOn = DateTime.Now;
            RevokedBy = null;
            RevokedOn = null;
        }

        public static bool LatinOrNumbersValidation(string input)
        {
            if (Regex.IsMatch(input, @"^[a-zA-Z0-9]+$"))
                return true;
            return false;
        }

        public static bool LatinOrCyrillicValidation(string input)
        {
            if (Regex.IsMatch(input, @"^[a-zA-Zа-яА-Я]+$"))
                return true;
            return false;
        }
    }
}
