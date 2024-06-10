using System.Globalization;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
namespace BookingApp;

[ApiController]
[Authorize]
[Route("categories")]
public class CategoryController() : ControllerBase
{
    [HttpGet]
    public IActionResult GetCategories()
    {
        return Ok(CategoryManager.GetCategoriesForServices());
    }
}