namespace Infrastructure.Utilities.RegEx
{
    public static class RegularExpression
    {
        public static readonly string EmailFormat = @"^\s*\w+@\w+\.[Cc][Oo][Mm]\s*$";
        public static readonly string AlphaWhiteSpacesDash = @"^(\s*([A-Za-z]+\s*-?))*((\s)*[A-Za-z]+\s*)$";
        public static readonly string Alphanumeric = @"^[A-Za-z0-9 ]*$";
        public static readonly string NoWhiteSpaces = @"^\S*$";
        public static readonly string NumericFormat = @"^(0|[1-9][0-9]*)$";
        public static readonly string AlphanumericWhiteSpaceDash = @"^[A-Za-z0-9-]*$";
    }
}