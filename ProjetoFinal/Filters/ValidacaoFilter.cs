// Valida os dados antes de chegar no Controller

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace ProjetoFinal.Filters;

public class ValidacaoFilter : IActionFilter
{
    public void OnActionExecuting(ActionExecutingContext context)
    {
        if (!context.ModelState.IsValid)
        {
            var erros = context.ModelState
                .Where(x => x.Value?.Errors.Count > 0)
                .ToDictionary(x => x.Key, x => x.Value?.Errors.Select(e => e.ErrorMessage));
            context.Result = new BadRequestObjectResult(new { erros });
        }
    }
    public void OnActionExecuted(ActionExecutedContext context) { }
}