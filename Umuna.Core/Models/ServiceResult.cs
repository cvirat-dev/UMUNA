namespace Umuna.Core.Contracts.Models
{
    public class ServiceResult<T> where T : class
    {
        public bool Success { get; set; }
        public string? Message { get; set; }
        public T? Data { get; set; }

        public static ServiceResult<T> Ok(T data)
        {
            return new ServiceResult<T>
            {
                Success = true,
                Data = data
            };
        }

        public static ServiceResult<T> Fail(string message)
        {
            return new ServiceResult<T>
            {
                Success = false,
                Message = message
            };
        }
    }
    public class ServiceResult
    {
        public bool Success { get; set; }
        public string? Message { get; set; }
        public static ServiceResult Ok()
        {
            return new ServiceResult
            {
                Success = true
            };
        }
        public static ServiceResult Fail(string message)
        {
            return new ServiceResult
            {
                Success = false,
                Message = message
            };
        }
    }
}
