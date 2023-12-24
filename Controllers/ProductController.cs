using System.Collections;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WorkshopAPI.Models;

namespace WorkshopAPI.Controllers;

[Route("api/product")]
[ApiController]
public class ProductController : ControllerBase
{
    private readonly bdeContext _context;
    
    public ProductController(bdeContext context)
    {
        _context = context;
    }
    
    
    // Post: api/product/boisson/all
    [HttpGet("boisson/all")]
    public async Task<ActionResult<IEnumerable>> getAllBoisson()
    {
        var products = await _context.Produits
            .Where(products => products.Categorie == "BOISSON")
            .ToListAsync();
        return products;
    }
}