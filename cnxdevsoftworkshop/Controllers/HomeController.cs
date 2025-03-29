using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using cnxdevsoftworkshop.Models;

namespace cnxdevsoftworkshop.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;

    public HomeController(ILogger<HomeController> logger)
    {
        _logger = logger;
    }


    public IActionResult Index()
    {
       
        
        
        return View();
    }
    


    

    private static List<string> calculationHistory = new List<string>();

    [HttpPost]
    public IActionResult Index(double x, double y, string operation)
    {
        try
        {
            double result = 0;
            string calculationResult = "";
        
            switch (operation)
            {
                case "+":
                    result = x + y;
                    calculationResult = $"{x} + {y} = {result}";
                    break;
                case "-":
                    result = x - y;
                    calculationResult = $"{x} - {y} = {result}";
                    break;
                case "*":
                    result = x * y;
                    calculationResult = $"{x} * {y} = {result}";
                    break;
                case "/":
                    if (y != 0)
                    {
                        result = x / y;
                        calculationResult = $"{x} / {y} = {result}";
                    }
                    else
                    {
                        ViewData["Result"] = "Cannot divide by zero.";
                        return View();
                    }
                    break;
                default:
                    ViewData["Result"] = "Invalid operation.";
                    return View();
            }
            calculationHistory.Add(calculationResult);
            Console.WriteLine("result : " + result);
            ViewData["Result"] = result;
            ViewData["History"] = calculationHistory;
            return View();
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }


    [HttpPost]
    public IActionResult ClearHistory()
    {
      
        calculationHistory.Clear();

       
        return RedirectToAction("Index");
    }


    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
