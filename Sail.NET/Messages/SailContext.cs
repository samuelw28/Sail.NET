namespace Sail.NET
{
    /// <summary>
    /// The context for sending a message that allows for error handling
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class SailContext<T>
    {
        public T Result { get; set; }
        public bool Success { get; set; }
        public string Exception { get; set; }

        public SailContext(T result, bool success, string exception = "")
        {
            Result = result;
            Success = success;
            Exception = exception;
        }
    }
}
