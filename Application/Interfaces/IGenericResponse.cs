namespace Application.Interfaces
{
    public interface IGenericResponse
    {
        public bool IsSuccessful { get; set; }
        public string Message { get; set; }
    }
    public interface IGenericResponse<T> : IGenericResponse
    {
        public T Result { get; set; }
        public bool IsSuccessful { get; set; }
        public string Message { get; set; }
        public static string EXCEPTION_OCURRED { get; set; }
    }
}
