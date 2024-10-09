using Microsoft.AspNetCore.Mvc;

namespace TestTask1.Web.Controllers;

public class SalaryReportController : Controller
{
    // GET
    public IActionResult Index()
    {
        return View();
    }
}