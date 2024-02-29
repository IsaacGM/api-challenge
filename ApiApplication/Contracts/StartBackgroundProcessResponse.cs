namespace ApiApplication.Contracts
{
    public class StartBackgroundProcessResponse
    {
        public bool Result { get; set; }

        public StartBackgroundProcessResponse(bool result)
        {
            Result = result;
        }
    }
}
