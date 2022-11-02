namespace AirQuality.Core.Integration
{
    public class IntegrationResult<T>
    {
        public IntegrationResult()
        {

        }

        public IntegrationResult(bool isSuccess, T data, string errorMessage = null)
        {
            IsSuccess = isSuccess;
            Data = data;
            ErrorMessage = errorMessage;
        }

        public bool IsSuccess { get; set; }
        public T Data { get; set; }
        public string ErrorMessage { get; set; }

    }
}
