using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;
using BankingSolution.Domain.Exceptions;

namespace BankingSolution.Extensions
{
    public class ExceptionFilter : IExceptionFilter
    {
        public void OnException(ExceptionContext context)
        {
            switch (context.Exception)
            {
                case InvalidIbanException:
                    context.Result = new BadRequestObjectResult(context.Exception.Message); break;
                case DuplicateIbanException:
                    context.Result = new BadRequestObjectResult(context.Exception.Message); break;
                case BankAccountNotFoundException:
                    context.Result = new NotFoundObjectResult(context.Exception.Message); break;
                case InvalidAccountBalanceException:
                    context.Result = new BadRequestObjectResult(context.Exception.Message); break;
                default:
                    context.Result = new ObjectResult(context.Exception.Message)
                    {
                        StatusCode = 500
                    };
                    break;
            }

            // Prevent other exception filters from being executed.
            context.ExceptionHandled = true;
        }
    }
}
