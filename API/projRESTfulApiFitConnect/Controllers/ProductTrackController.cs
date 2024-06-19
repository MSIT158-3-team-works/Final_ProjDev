using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using projRESTfulApiFitConnect.DTO.Product;
using projRESTfulApiFitConnect.Models;

namespace projRESTfulApiFitConnect.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductTrackController : ControllerBase
    {
        private readonly GymContext _context;
        private readonly IWebHostEnvironment _env;

        public ProductTrackController(GymContext context, IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
        }

        // GET: api/ProductTrack
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProductShoppingCartDTO>>> GetTproductTracks(int id)
        {
            //return await _context.TproductTracks.ToListAsync();
            string filepath = "";

            List<ProductShoppingCartDTO> productTrackDtos = new List<ProductShoppingCartDTO>();

            var track = await _context.TproductTracks
                .Where(x => x.MemberId == id)
                        .Include(x => x.Product)
                        .ToListAsync();

            foreach (var item in track)
            {
                string base64Image = "";
                filepath = Path.Combine(_env.ContentRootPath, "Images", "ProductImages", item.Product.ProductImage);
                if (System.IO.File.Exists(filepath))
                {
                    byte[] bytes = await System.IO.File.ReadAllBytesAsync(filepath);
                    base64Image = Convert.ToBase64String(bytes);
                }

                ProductShoppingCartDTO productTrackDto = new ProductShoppingCartDTO()
                {
                    shoppingCartId = item.ProductTrackId,
                    productId = item.ProductId,
                    productName = item.Product.ProductName,
                    quantity = 1,
                    unitPrice = item.Product.ProductUnitprice,
                    productImage = base64Image,
                };
                productTrackDtos.Add(productTrackDto);
            }

            return productTrackDtos;
        }


        // PUT: api/ProductTrack/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTproductTrack(int id, TproductTrack tproductTrack)
        {
            if (id != tproductTrack.ProductTrackId)
            {
                return BadRequest();
            }

            _context.Entry(tproductTrack).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TproductTrackExists(id))
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

        // POST: api/ProductTrack
        [HttpPost]
        public async Task<ActionResult<TproductTrack>> PostTproductTrack(int memberId,int productId)
        {
            var tracked = _context.TproductTracks.Any(t=>t.MemberId==memberId&&t.ProductId==productId);
            if (!tracked)
            {
                TproductTrack tproductTrack = new TproductTrack();
                tproductTrack.MemberId = memberId;
                tproductTrack.ProductId = productId;
                _context.TproductTracks.Add(tproductTrack);
                await _context.SaveChangesAsync();

                return Ok("Tracked");
            }
            else return Ok("Failed");
        }

        // DELETE: api/ProductTrack/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTproductTrack(int id)
        {
            var tproductTrack = await _context.TproductTracks.FindAsync(id);
            if (tproductTrack == null)
            {
                return NotFound();
            }

            _context.TproductTracks.Remove(tproductTrack);
            await _context.SaveChangesAsync();

            return Ok("Deleted");
        }

        private bool TproductTrackExists(int id)
        {
            return _context.TproductTracks.Any(e => e.ProductTrackId == id);
        }
    }
}
