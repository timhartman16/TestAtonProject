namespace Infrastructure.Exceptions
{
    public class ErrorText
    {
        public static string IncorrectLogin = "Incorrect symbols in login. Login must consist only of Latin letters and numbers.";
        public static string IncorrectPassword = "Incorrect symbols in password. Password must consist only of Latin letters and numbers.";
        public static string IncorrectUserName = "Incorrect symbols in name. Name must consist only of Latin or Cyrillic letters.";
        public static string UserAlreadyExists = "User with this login already exists.";
        public static string ThisUserHasBeenDeleted = "This user has been deleted.";
        public static string UserNotFound = "User not found.";
        public static string CorrectLoginIncorrectPassword = "Correct login, incorrect password.";
        public static string ForbidActionForThisUser = "Forbidden action for this user.";
    }
}
