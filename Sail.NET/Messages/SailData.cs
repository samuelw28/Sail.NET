namespace Sail.NET
{
    public class SailData<T>
    {
        public T Result { get; set; }
        public bool Success { get; set; }
        public string Exception { get; set; }

        public SailData(T result, bool success, string exception = "")
        {
            Result = result;
            Success = success;
            Exception = exception;
        }
    }
}
