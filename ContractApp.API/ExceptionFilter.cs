using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace ContractApp.API
{
    public class ApplicationException : Exception
    {
        public ApplicationException(string message, int statusCode) : base(message)
        {
            this.statusCode = statusCode;
        }

        public int statusCode { get; set; }
    }

    public class ExceptionFilter : IExceptionFilter
    {
        public void OnException(ExceptionContext context)
        {
            int statusCode = 500;
            string message = context.Exception.Message;

            if (context.Exception is ApplicationException appException)
                statusCode = appException.statusCode;

            context.Result = new JsonResult(new
            {
                message,
                status = statusCode
            })
            {
                StatusCode = statusCode
            };

            context.ExceptionHandled = true;
        }
    }
}
