namespace ContractApp.API.Commons
{
    public class BaseResponse<T>
    {
        public string message { get; set; }
        public int statusCode { get; set; }
        public T data { get; set; }

        public BaseResponse() { }

        public BaseResponse(string message, int statusCode, T data)
        {
            this.message = message;
            this.statusCode = statusCode;
            this.data = data;
        }
    }
}
