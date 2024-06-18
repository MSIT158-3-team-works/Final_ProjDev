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
    public class ProductsController : ControllerBase
    {
        private readonly GymContext _context;
        private readonly IWebHostEnvironment _env;

        public ProductsController(GymContext context, IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
        }
        private async Task<List<ProductDetailDTO>> LoadProducts()
        {
            string filepath = "";

            List<ProductDetailDTO> productDetailDtos = new List<ProductDetailDTO>();

            var products = await _context.Tproducts
                        .Where(x => x.ProductSupplied == true)
                        .Include(x => x.Category)
                        .Include(x => x.TorderDetails)
                        .Include(x => x.TproductImages)
                        .ToListAsync();

            foreach (var item in products)
            {
                var images = item.TproductImages.Select(img => new ProductImagesDTO
                {
                    productImages = img.ProductImages
                }).ToList();
                string base64Image = "";
                filepath = Path.Combine(_env.ContentRootPath, "Images", "ProductImages", item.ProductImage);
                if (System.IO.File.Exists(filepath))
                {
                    byte[] bytes = await System.IO.File.ReadAllBytesAsync(filepath);
                    base64Image = Convert.ToBase64String(bytes);
                }

                int sold = item.TorderDetails.Sum(od => od.OrderQuantity);

                ProductDetailDTO productDetailDto = new ProductDetailDTO()
                {
                    productId = item.ProductId,
                    productName = item.ProductName,
                    productCategoryId = (int)(item.CategoryId),
                    productCategory = item.Category.CategoryName,
                    unitPrice = item.ProductUnitprice,
                    productDetail = item.ProductDetail,
                    productImage = base64Image,
                    productSold = sold,
                    Images = images
                };
                productDetailDtos.Add(productDetailDto);
            }

            return productDetailDtos;
        }

        private async Task<string> GetBase64Image(string productImage)
        {
            string filepath = Path.Combine(_env.ContentRootPath, "Images", "ProductImages", productImage);
            if (System.IO.File.Exists(filepath))
            {
                byte[] bytes = await System.IO.File.ReadAllBytesAsync(filepath);
                return Convert.ToBase64String(bytes);
            }
            return string.Empty;
        }

        // GET: api/Products
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProductDetailDTO>>> GetTproducts()
        {
            //return await _context.Tproducts.ToListAsync();
            List<ProductDetailDTO> productDetailDtos = await LoadProducts();
            return Ok(productDetailDtos);
        }

        // GET: api/Products/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ProductDetailDTO>> GetTproducts(int id)
        {
            var product = await _context.Tproducts
                .Where(x => x.ProductId == id && x.ProductSupplied == true)
                .Include(x => x.Category)
                .Include(x => x.TorderDetails)
                .Include(x => x.TproductImages)
                .FirstOrDefaultAsync();

            if (product == null)
            {
                return NotFound();
            }

            var images = product.TproductImages.Select(img => new ProductImagesDTO
            {
                productImages = img.ProductImages
            }).ToList();

            string base64Image = await GetBase64Image(product.ProductImage);
            int sold = product.TorderDetails.Sum(od => od.OrderQuantity);

            ProductDetailDTO productDetailDto = new ProductDetailDTO()
            {
                productId = product.ProductId,
                productName = product.ProductName,
                productCategoryId = (int)product.CategoryId,
                productCategory = product.Category.CategoryName,
                unitPrice = product.ProductUnitprice,
                productDetail = product.ProductDetail,
                productImage = base64Image,
                productSold = sold,
                Images = images,
                Base64Images = new List<string>()
            };

            if (images.Count > 0)
            {
                foreach (var image in images)
                {
                    if (!string.IsNullOrEmpty(image.productImages))
                    {
                        string base64Img = await GetBase64Image(image.productImages);
                        productDetailDto.Base64Images.Add(base64Img);
                    }
                }
            }

            return Ok(productDetailDto);
        }

        [HttpPost("search")]
        public async Task<ActionResult<ProductsPagingDTO>> PosttTproduct(/*[FromQuery]*/ ProductSearchDTO productSearchDTO)
        {
            List<ProductDetailDTO> productDetailDtos = await LoadProducts();
            //根據分類編號搜尋資料
            var products = productSearchDTO.categoryId == 0 ? productDetailDtos : productDetailDtos.Where(s => s.productCategoryId == productSearchDTO.categoryId);

            //根據關鍵字搜尋資料(title、desc)
            if (!string.IsNullOrEmpty(productSearchDTO.keyword))
            {
                products = products.Where(p => p.productName.Contains(productSearchDTO.keyword) || p.productCategory.Contains(productSearchDTO.keyword) || p.productDetail.Contains(productSearchDTO.keyword));
            }

            //排序
            switch (productSearchDTO.sortBy)
            {
                case "popular":
                    products = productSearchDTO.sortType == "asc" ? products.OrderByDescending(p => p.productSold) : products.OrderBy(p => p.productSold);
                    break;
                case "unitPrice":
                    products = productSearchDTO.sortType == "asc" ? products.OrderBy(p => p.unitPrice) : products.OrderByDescending(p => p.unitPrice);
                    break;
                case "categoryId":
                    products = productSearchDTO.sortType == "asc" ? products.OrderByDescending(p => p.productCategoryId) : products.OrderBy(p => p.productCategoryId);
                    break;
                default:
                    products = productSearchDTO.sortType == "asc" ? products.OrderBy(p => p.productId) : products.OrderByDescending(p => p.productId);
                    break;
            }

            //總共有多少筆資料
            int totalCount = products.Count();
            //每頁要顯示幾筆資料
            int pageSize = productSearchDTO.pageSize;
            //目前第幾頁
            int page = productSearchDTO.page;

            //計算總共有幾頁
            int totalPages = (int)Math.Ceiling((decimal)totalCount / pageSize);

            //分頁
            products = products.Skip((page - 1) * pageSize).Take(pageSize);


            //包裝要傳給client端的資料
            ProductsPagingDTO productsPaging = new ProductsPagingDTO();
            productsPaging.TotalCount = totalCount;
            productsPaging.TotalPages = totalPages;
            productsPaging.ProductsResult = products.ToList();


            return Ok(productsPaging);
        }


        [HttpPut("{id}")]
        public async Task<IActionResult> PutTproduct(int id, AddProductDTO addProductDTO)
        {
            var product = await _context.Tproducts
                .Where(x => x.ProductId == id)
                .Include(x => x.TproductImages)
                .FirstOrDefaultAsync();

            if (product == null)
            {
                return NotFound("Product not found");
            }

            if (!string.IsNullOrEmpty(addProductDTO.ProductName))
                product.ProductName = addProductDTO.ProductName;

            if (addProductDTO.CategoryId.HasValue && addProductDTO.CategoryId != 0)
                product.CategoryId = addProductDTO.CategoryId.Value;

            if (addProductDTO.ProductUnitprice != 0)
                product.ProductUnitprice = addProductDTO.ProductUnitprice;

            if (!string.IsNullOrEmpty(addProductDTO.ProductDetail))
                product.ProductDetail = addProductDTO.ProductDetail;

            if (!string.IsNullOrEmpty(addProductDTO.ProductImage) && !string.IsNullOrEmpty(addProductDTO.ImageBase64))
            {
                byte[] imageBytes = Convert.FromBase64String(addProductDTO.ImageBase64);
                string filepath = Path.Combine(_env.ContentRootPath, "Images", "ProductImages", addProductDTO.ProductImage);
                await System.IO.File.WriteAllBytesAsync(filepath, imageBytes);

                product.ProductImage = addProductDTO.ProductImage;
            }

            _context.Entry(product).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            // Save additional images if any
            if (addProductDTO.Images != null && addProductDTO.moreBase64Images != null && addProductDTO.Images.Count == addProductDTO.moreBase64Images.Count)
            {
                for (int i = 0; i < addProductDTO.Images.Count; i++)
                {
                    byte[] additionalImageBytes = Convert.FromBase64String(addProductDTO.moreBase64Images[i]);
                    string additionalFilePath = Path.Combine(_env.ContentRootPath, "Images", "ProductImages", addProductDTO.Images[i].productImages);
                    await System.IO.File.WriteAllBytesAsync(additionalFilePath, additionalImageBytes);

                    TproductImage tproductImage = new TproductImage
                    {
                        ProductId = product.ProductId,
                        ProductImages = addProductDTO.Images[i].productImages
                    };

                    _context.TproductImages.Add(tproductImage);
                }

                await _context.SaveChangesAsync();
            }

            return Ok("EditSuccess");

        }



        // POST: api/Products
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Tproduct>> PostTproduct(AddProductDTO addProductDTO)
        {
            // Decode the base64 image string
            byte[] imageBytes = Convert.FromBase64String(addProductDTO.ImageBase64);
            // Save the image to the server
            string filepath = Path.Combine(_env.ContentRootPath, "Images", "ProductImages", addProductDTO.ProductImage);
            await System.IO.File.WriteAllBytesAsync(filepath, imageBytes);

            Tproduct tproduct = new Tproduct
            {
                ProductName = addProductDTO.ProductName,
                CategoryId = addProductDTO.CategoryId,
                ProductUnitprice = addProductDTO.ProductUnitprice,
                ProductDetail = addProductDTO.ProductDetail,
                ProductImage = addProductDTO.ProductImage,
                ProductSupplied = true
            };
            _context.Tproducts.Add(tproduct);
            await _context.SaveChangesAsync();

            // Save additional images if any
            if (addProductDTO.Images != null && addProductDTO.moreBase64Images != null && addProductDTO.Images.Count == addProductDTO.moreBase64Images.Count)
            {
                for (int i = 0; i < addProductDTO.Images.Count; i++)
                {
                    byte[] additionalImageBytes = Convert.FromBase64String(addProductDTO.moreBase64Images[i]);
                    string additionalFilePath = Path.Combine(_env.ContentRootPath, "Images", "ProductImages", addProductDTO.Images[i].productImages);
                    await System.IO.File.WriteAllBytesAsync(additionalFilePath, additionalImageBytes);

                    TproductImage tproductImage = new TproductImage
                    {
                        ProductId = tproduct.ProductId,
                        ProductImages = addProductDTO.Images[i].productImages
                    };

                    _context.TproductImages.Add(tproductImage);
                }

                await _context.SaveChangesAsync();
            }

            return Ok("product add");
        }


        // DELETE: api/Products/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTproduct(int id)
        {
            var tproduct = await _context.Tproducts.FindAsync(id);
            if (tproduct == null)
            {
                return NotFound();
            }

            //_context.Tproducts.Remove(tproduct);
            tproduct.ProductSupplied = false;
            await _context.SaveChangesAsync();

            return Ok("已停售");
        }

        [HttpPut("image")]
        public async Task<IActionResult> DeleteTproductImage(int id, string productImage)
        {
            var productimgs = await _context.TproductImages
                .Where(x => x.ProductId == id)
                .Where(y=>y.ProductImages== productImage)
                .FirstOrDefaultAsync();

            if (productimgs == null)
            {
                return NotFound("Product not found");
            };
            _context.TproductImages.Remove(productimgs);
            await _context.SaveChangesAsync();
            return Ok("Removed");
        }

        private bool TproductExists(int id)
        {
            return _context.Tproducts.Any(e => e.ProductId == id);
        }
    }
}
