using DesafioDevApi.Domain.Common;
using Microsoft.AspNetCore.Mvc;
using Serilog;

namespace DesafioDevApi.Controllers
{
    public static class BaseControllerExtensions
    {
        public static IActionResult ValidateResponse(this ControllerBase controllerBase, int statusCode, Response response)
        {
            if (response == null)
                return controllerBase.NoContent();

            if (response.HasMessages)
            {
                var property = response?.Messages?.OrderByDescending(x => x.Property)?.FirstOrDefault();
                switch (property?.Property)
                {
                    case "302":
                        return controllerBase.Redirect(property?.Message);
                    case "400":
                        return controllerBase.BadRequest(response?.Messages);
                    case "403":
                        return controllerBase.Forbid(property?.Message);
                    case "404":
                        return controllerBase.NotFound(response?.Messages);
                    case "500":
                        return controllerBase.StatusCode(500, response?.Messages);
                    default:
                        Log.Error($"Response Error: {System.Text.Json.JsonSerializer.Serialize(response)}");
                        return controllerBase.BadRequest(response?.Messages);
                }
            }

            return controllerBase.StatusCode(statusCode, response.Value);
        }
    }
}
