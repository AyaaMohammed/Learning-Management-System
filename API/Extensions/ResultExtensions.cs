using Application.Results;
using Microsoft.AspNetCore.Mvc;

namespace API.Extensions
{
    public static class ResultExtensions
    {
        public static IActionResult ToActionResult<T>(
            this Result<T> result,
            ControllerBase controller)
        {
            if (result.IsSuccess)
            {
                return controller.Ok(new
                {
                    code = StatusCodes.Status200OK,
                    message = "Success",
                    data = result.Value
                });
            }

            return result.Error!.Type switch
            {
                ErrorType.Validation =>
                    controller.BadRequest(new
                    {
                        code = StatusCodes.Status400BadRequest,
                        message = result.Error.Message
                    }),

                ErrorType.NotFound =>
                    controller.NotFound(new
                    {
                        code = StatusCodes.Status404NotFound,
                        message = result.Error.Message
                    }),

                ErrorType.Unauthorized =>
                    controller.Unauthorized(new
                    {
                        code = StatusCodes.Status401Unauthorized,
                        message = result.Error.Message
                    }),

                ErrorType.Conflict =>
                    controller.Conflict(new
                    {
                        code = StatusCodes.Status409Conflict,
                        message = result.Error.Message
                    }),

                ErrorType.Failure =>
                    controller.StatusCode(
                        StatusCodes.Status500InternalServerError,
                        new
                        {
                            code = StatusCodes.Status500InternalServerError,
                            message = result.Error.Message
                        }),

                _ =>
                    controller.StatusCode(
                        StatusCodes.Status500InternalServerError,
                        new
                        {
                            code = StatusCodes.Status500InternalServerError,
                            message = result.Error.Message
                        })
            };
        }
    }
}