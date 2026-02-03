namespace ChordSense.Api.Models.Responses
{
    public class ApiResponse<T>
    {
        public bool Success { get; set; }
        public T? Data { get; set; }
        public string? Error {  get; set; }
        public string? Message { get; set; } = string.Empty;

        public static ApiResponse<T> Ok(T data, string Message) => new ApiResponse<T> { Success = true, Message = Message, Data = data };
        public static ApiResponse<T> Fail(string Error, string Message) => new ApiResponse<T> { Success = false, Message = Message, Error = Error };
    }
}
