using Application.Interfaces;

namespace Application.Contracts
{
    public class FailureResponse<T> : IGenericResponse<T>
    {
        public T Result { get; set; }
        public bool IsSuccessful { get; set; }
        public string Message { get; set; }
        public FailureResponse(string message)
        {
            IsSuccessful = false;
            Message = message;
        }
    }
    public class FailureResponse : IGenericResponse
    {
        public bool IsSuccessful { get; set; }
        public string Message { get; set; }
        public static string EXCEPTION_OCURRED { get; set; } = "Exception occurred.";
        public FailureResponse(string message)
        {
            IsSuccessful = false;
            Message = message;
        }
    }
}
