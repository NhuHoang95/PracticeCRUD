using Microsoft.AspNetCore.Mvc;
using PracticeCRUD.Data.Services;
using PracticeCRUD.Enums;
using PracticeCRUD.Extentions;
using PracticeCRUD.Models;
using PracticeCRUD.Response;

namespace PracticeCRUD.Controllers
{
    public class ProductController : Controller
    {
        private readonly IProductService _service;
        public ProductController(IProductService service)
        {
            _service = service;
        }
        [HttpGet]
        [Route("api/products")]
        public async Task<IActionResult> GetAllProduct()
        {
            ResponseType<Product> res = new ResponseType<Product>();

            try
            {
                var products = await _service.GetAllAsync();

                if (products == null)
                {
                    res.Code = (int)ErrorEnum.DataNotFound;
                    res.Message = ErrorEnum.DataNotFound.Description();
                    return BadRequest(res);
                }

                res.Code = (int)ErrorEnum.Success;
                res.Message = ErrorEnum.Success.Description();
                res.Data = products as List<Product>;
                return Ok(res);
            }
            catch (Exception)
            {
                res.Code = (int)ErrorEnum.Failed;
                res.Message = ErrorEnum.Failed.Description();
                return BadRequest(res);
            }
        }

        [HttpGet]
        [Route("api/products/{id}")]
        public async Task<IActionResult> GetProductById(int id)
        {
            ResponseType<Product> res = new ResponseType<Product>();

            try
            {
                var catelory = await _service.GetByIdAsync(id);

                if (catelory == null)
                {
                    res.Code = (int)ErrorEnum.DataNotFound;
                    res.Message = ErrorEnum.DataNotFound.Description();
                    return BadRequest(res);
                }

                res.Code = (int)ErrorEnum.Success;
                res.Message = ErrorEnum.Success.Description();
                res.Data = new List<Product>() { catelory };
                return Ok(res);
            }
            catch (Exception)
            {
                res.Code = (int)ErrorEnum.Failed;
                res.Message = ErrorEnum.Failed.Description();
                return BadRequest(res);
            }
        }

        [HttpPost]
        [Route("api/products")]
        public async Task<IActionResult> AddProduct([FromBody] Product Product)
        {
            ResponseType<Product> res = new ResponseType<Product>();
            try
            {
                await _service.AddAsync(Product);
                res.Code = (int)ErrorEnum.Success;
                res.Message = ErrorEnum.Success.Description();
                res.Data = new List<Product>() { Product };
                return Ok(res);
            }
            catch (Exception)
            {
                res.Code = (int)ErrorEnum.Failed;
                res.Message = ErrorEnum.Failed.Description();
                return BadRequest(res);

            }
        }

        [HttpPut]
        [Route("api/products/{id}")]
        public async Task<IActionResult> UpdateProduct(int id, [FromBody] Product Product)
        {
            ResponseType<Product> res = new ResponseType<Product>();
            try
            {
                bool isUpdate = await _service.UpdateAsync(id, Product);
                if (!isUpdate)
                {
                    res.Code = (int)ErrorEnum.DataNotFound;
                    res.Message = ErrorEnum.DataNotFound.Description();
                    return BadRequest(res);
                }
                res.Code = (int)ErrorEnum.Success;
                res.Message = ErrorEnum.Success.Description();
                res.Data = new List<Product>() { Product };
                return Ok(res);
            }
            catch (Exception)
            {
                res.Code = (int)ErrorEnum.Failed;
                res.Message = ErrorEnum.Failed.Description();
                return BadRequest(res);

            }
        }

        [HttpDelete]
        [Route("api/products/{id}")]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            ResponseType<Product> res = new ResponseType<Product>();
            try
            {
                bool isDelete = await _service.DeleteAsync(id);
                if (!isDelete)
                {
                    res.Code = (int)ErrorEnum.DataNotFound;
                    res.Message = ErrorEnum.DataNotFound.Description();
                    return BadRequest(res);
                }
                res.Code = (int)ErrorEnum.Success;
                res.Message = ErrorEnum.Success.Description();
                return Ok(res);
            }
            catch (Exception)
            {
                res.Code = (int)ErrorEnum.Failed;
                res.Message = ErrorEnum.Failed.Description();
                return BadRequest(res);

            }
        }
    }
}
