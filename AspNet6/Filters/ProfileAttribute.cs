using Microsoft.AspNetCore.Mvc.Filters;
using System.Diagnostics;

namespace AspNet6.Filters
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = false, Inherited = true)]
    public sealed class ProfileAttribute : ActionFilterAttribute
    {
        public override async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            Stopwatch timer = Stopwatch.StartNew();
            await next();
            timer.Stop();
            string result = $"Elapsed time: {timer.Elapsed.TotalMilliseconds} ms";
            Console.WriteLine(result);
        }
    }
}
