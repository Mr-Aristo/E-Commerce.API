using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Infrastructure.Filters
{
    /// <summary>
    /// Program.cs de validasyon filterlarinin bastirdik.
    /// Kendi custom filteremizi olusturduk.
    /// </summary>
    public class ValidationFilterService : IAsyncActionFilter
    {
        /// <summary>
        /// Filter yapilanmalari sirali sekilde calisirlar. Next bir sonraki filteri delege eder
        /// Ayni sekilde bu yapiyida middleware olarak mimariye program.cs de eklememiz gerekir.
        /// </summary>
        /// <param name="context"></param>
        /// <param name="next"></param>
        /// <returns></returns>
        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            if (!context.ModelState.IsValid)
            {
                var errors = context.ModelState
                    .Where(x => x.Value.Errors.Any())
                    .ToDictionary(e => e.Key, e => e.Value.Errors.Select(e => e.ErrorMessage))
                    .ToArray();

                context.Result = new BadRequestObjectResult(errors);
                return;

            }
            await next();
        }
    }
}
