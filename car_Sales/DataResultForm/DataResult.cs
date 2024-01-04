namespace car_Sales.DataResultForm
{
    public class DataResult<T>
    {
        public T Data { get; private set; }
        public string Message { get; private set; }
        public bool Success { get; private set; }

        public DataResult() { }

        public static DataResult<T> SuccessResult(T data, string message = "")
        {
            return new DataResult<T>
            {
                Data = data,
                Message = message,
                Success = true
            };
        }

        public static DataResult<T> FailureResult(string message)
        {
            return new DataResult<T>
            {
                Message = message,
                Success = false
            };
        }
    }


}
