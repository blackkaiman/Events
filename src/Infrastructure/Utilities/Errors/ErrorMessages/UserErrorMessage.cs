namespace Infrastructure.Utilities.Errors.ErrorMessages
{
    public static class UserErrorMessage
    {
        public static readonly string EmailLength = "Email should have between 7 and 74 characters";
        public static readonly string UniqueEmail = "User with that email already exists";
        public static readonly string EmailFormat = "Email should have a valid format {alphanumeric and/or underline}@{alpha}.com";

        public static readonly string FirstNameFormat = "First Name should have between 2 and 100 alpha characters, including '-' and ' '";

        public static readonly string LastNameFormat = "Last Name should have between 2 and 100 alpha characters, including '-' and ' '";

        public static readonly string PasswordFormat = "Password should have between 2 and 20 characters, excluding whitespaces";
        public static readonly string PasswordAndNewPassword = "The new and old password should be sent togheter";

        public static readonly string CompanyFormat = "Company should have between 2 and 100 alphanumeric characters";
       
        public static readonly string WrongCredentials = "Wrong credentials";

        public static readonly string UserNotFound = "User with that email does not exist";
        public static readonly string AccompanyingPersonNotFound = "Accompanying person with that email does not exist";
    }
}