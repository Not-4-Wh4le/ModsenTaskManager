using Microsoft.AspNetCore.Mvc;
using ResultPattern;

namespace WebApi.Extensions
{
    public static class ApiControllerExtensions
    {
        public static ActionResult HandleFailure(this ControllerBase controller, Result result)
        {
            if(result.IsSuccess)
            {
                throw new InvalidOperationException("You cannot treat a successful result as an error");
            }

            var statusCode = result.Error.ErrorType switch
            {
                ErrorType.Unauthorized => StatusCodes.Status401Unauthorized,
                ErrorType.NotFound => StatusCodes.Status404NotFound,
                ErrorType.Conflict => StatusCodes.Status409Conflict,
                ErrorType.Validation => StatusCodes.Status400BadRequest,
                _ => StatusCodes.Status400BadRequest
            };

            return controller.Problem(
                statusCode: statusCode,
                title: result.Error.Code,
                detail: result.Error.Description);
        }
    }
}
