using CarRentalSystem.Core.Shared;
using Microsoft.AspNetCore.Mvc;

namespace CarRentalSystem.Web.Controllers;

public abstract class AbstractController : Controller
{
    protected void HandleErrors(Result result)
    {
        if (result.IsSuccess)
        {
            throw new InvalidOperationException(nameof(HandleErrors) + " must not be called on success");
        }

        if (result is IValidationResult validationResult)
        {
            foreach (var error in validationResult.Errors)
            {
                ModelState.AddModelError(error.Code, error.Message);
            }
        }
        else
        {
            ModelState.AddModelError(result.Error.Code, result.Error.Message);
        }
    }
}