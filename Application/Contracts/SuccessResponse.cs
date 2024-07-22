using Application.Interfaces;

namespace Application.Contracts
{
    public class SuccessResponse : IGenericResponse
    {
        public bool IsSuccessful { get; set; }
        public string Message { get; set; }
        public SuccessResponse()
        {
            IsSuccessful = true;
            Message = "Success.";
        }
    }
    public class SuccessResponse<T> : IGenericResponse<T>
    {
        public T Result { get; set; }
        public bool IsSuccessful { get; set; }
        public string Message { get; set; }
        public SuccessResponse(T value)
        {
            Result = value;
            IsSuccessful = true;
            Message = "Success.";
        }
    }
}
