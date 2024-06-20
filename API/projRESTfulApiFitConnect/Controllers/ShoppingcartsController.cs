using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis;
using Microsoft.EntityFrameworkCore;
using projRESTfulApiFitConnect.DTO.Product;
using projRESTfulApiFitConnect.Models;

namespace projRESTfulApiFitConnect.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ShoppingcartsController : ControllerBase
    {
        private readonly GymContext _context;
        private readonly IWebHostEnvironment _env;

        public ShoppingcartsController(GymContext context, IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
        }

        // GET: api/Shoppingcarts
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProductShoppingCartDTO>>> GetTproductShoppingcarts(int id)
        {
            string filepath = "";

            List<ProductShoppingCartDTO> productShoppingCartDtos = new List<ProductShoppingCartDTO>();

            var shoppingCart = await _context.TproductShoppingcarts
                        .Where(x => x.MemberId == id)
                        .Include(x => x.Product)
                        .ToListAsync();

            foreach (var item in shoppingCart)
            {
                string base64Image = "";
                filepath = Path.Combine(_env.ContentRootPath, "Images", "ProductImages", item.Product.ProductImage);
                if (System.IO.File.Exists(filepath))
                {
                    byte[] bytes = await System.IO.File.ReadAllBytesAsync(filepath);
                    base64Image = Convert.ToBase64String(bytes);
                }

                ProductShoppingCartDTO productShoppingCartDto = new ProductShoppingCartDTO()
                {
                    shoppingCartId=item.ProductShoppingcartId,
                    productId = item.ProductId,
                    productName = item.Product.ProductName,
                    quantity=item.ProductQuantity,
                    unitPrice = item.Product.ProductUnitprice,
                    productImage = base64Image,
                };
                productShoppingCartDtos.Add(productShoppingCartDto);
            }

            return productShoppingCartDtos;
        }

        // GET: api/Shoppingcarts/5
        [HttpGet("{id}")]
        public async Task<ActionResult<TproductShoppingcart>> GetTproductShoppingcart(int id)
        {
            var tproductShoppingcart = await _context.TproductShoppingcarts.FindAsync(id);

            if (tproductShoppingcart == null)
            {
                return NotFound();
            }

            return tproductShoppingcart;
        }

        // PUT: api/Shoppingcarts/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTproductShoppingcart(int id, TproductShoppingcart tproductShoppingcart)
        {
            if (id != tproductShoppingcart.ProductShoppingcartId)
            {
                return BadRequest();
            }

            _context.Entry(tproductShoppingcart).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TproductShoppingcartExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Shoppingcarts
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<TproductShoppingcart>> PostTproductShoppingcart(int memberId, int productId,int quantity)
        {
            try
            {
                var added = _context.TproductShoppingcarts.Any(c => c.MemberId == memberId && c.ProductId == productId);
                if (!added)
                {
                    var theProduct=_context.Tproducts.Where(p=>p.ProductId==productId).FirstOrDefault();
                    TproductShoppingcart tproductShoppingcart = new TproductShoppingcart();
                    tproductShoppingcart.MemberId = memberId;
                    tproductShoppingcart.ProductId = productId;
                    tproductShoppingcart.ProductQuantity = quantity;
                    tproductShoppingcart.ProductUnitprice = theProduct.ProductUnitprice;
                    _context.TproductShoppingcarts.Add(tproductShoppingcart);
                    await _context.SaveChangesAsync();

                    return Ok("AddToCart");
                }
                else
                {
                    var alreadyAdd = _context.TproductShoppingcarts.Where(c => c.MemberId == memberId && c.ProductId == productId).FirstOrDefault();
                    alreadyAdd.ProductQuantity = alreadyAdd.ProductQuantity + quantity;
                    _context.Entry(alreadyAdd).State = EntityState.Modified;
                    await _context.SaveChangesAsync();
                    return Ok("AddToCart");
                }
            }
            catch
            {
                return Ok("Failed");
            }
        }

        // DELETE: api/Shoppingcarts/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTproductShoppingcart(int id)
        {
            var tproductShoppingcart = await _context.TproductShoppingcarts.FindAsync(id);
            if (tproductShoppingcart == null)
            {
                return NotFound();
            }

            _context.TproductShoppingcarts.Remove(tproductShoppingcart);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool TproductShoppingcartExists(int id)
        {
            return _context.TproductShoppingcarts.Any(e => e.ProductShoppingcartId == id);
        }
    }
}
