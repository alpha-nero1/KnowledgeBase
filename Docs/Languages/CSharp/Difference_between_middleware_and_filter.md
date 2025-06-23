# Difference between middleware and filter

## 🔁 Middleware
Middleware is part of the ASP.NET Core request pipeline. Each middleware component handles a request and can choose to:

Act on the request

Pass it on to the next component (via await next())

Act on the response on the way back

🧱 Example:

csharp
```
app.Use(async (context, next) =>
{
    Console.WriteLine("Before");
    await next.Invoke(); // Call next middleware
    Console.WriteLine("After");
});
```
🧠 Think of middleware as global plumbing — it affects all requests.

🔍 Common uses:

- Logging
- Exception handling
- CORS
- Authentication
- Static files
- Routing

📌 Middleware works with HttpContext and runs early in the pipeline.

## 🧪 Filters
Filters are scoped to controller actions or Razor Pages. They let you run code:
- Before or after action methods
- Before or after results (e.g. views, JSON)

🧱 Example:

csharp
```
public class LogActionFilter : IActionFilter
{
    public void OnActionExecuting(ActionExecutingContext context)
    {
        Console.WriteLine("Before action");
    }

    public void OnActionExecuted(ActionExecutedContext context)
    {
        Console.WriteLine("After action");
    }
}
```
Apply via attribute or globally:

csharp
```
[ServiceFilter(typeof(LogActionFilter))]
public class MyController : Controller
{
    public IActionResult Index() => View();
}
```

🔍 Common uses:
- Validation
- Authorization
- Logging (per action)
- Caching
- Modifying result/response metadata

📌 Filters have access to **ControllerContext**, **ModelState**, and action arguments.