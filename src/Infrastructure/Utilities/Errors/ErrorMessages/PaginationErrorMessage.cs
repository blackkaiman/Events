namespace Infrastructure.Utilities.Errors.ErrorMessages
{
    public static class PaginationErrorMessage
    {
        public static readonly string InvalidPageNumber = "Page number should exist and be greater than 0";
        public static readonly string InvalidPageSize = "Page size should exist and be greater than 0";
    }
}