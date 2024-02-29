namespace ApiApplication.Infrastructure.Common.Errors
{
    public readonly struct Error
    {
        public string Code { get; }
        public string Description { get; }
        public ErrorType Type { get; }

        private Error(string errorCode, string description, ErrorType type)
        {
            Description = description;
            Type = type;
            Code = errorCode;
        }
        public static Error BadRequest(
            string code = DefaultErrorCodes.BadRequest, 
            string description = DefaultErrorDescriptions.BadRequest) =>
            new Error(code, description, ErrorType.BadRequest);

        public static Error Conflict(
            string code = DefaultErrorCodes.Conflict, 
            string description = DefaultErrorDescriptions.Conflict) => 
            new Error(code, description, ErrorType.Conflict);

        public static Error Internal(
            string code = DefaultErrorCodes.Internal, 
            string description = DefaultErrorDescriptions.Internal) => 
            new Error(code, description, ErrorType.Internal);

        public static Error NotFound(
            string code = DefaultErrorCodes.NotFound, 
            string description = DefaultErrorDescriptions.NotFound) => 
            new Error(code, description, ErrorType.NotFound);

        public static Error Validation(
            string code = DefaultErrorCodes.Validation, 
            string description = DefaultErrorDescriptions.Validation) =>
            new Error(code, description, ErrorType.Validation);

    }
}
