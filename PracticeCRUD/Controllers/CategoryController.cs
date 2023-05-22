using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.OpenApi.Extensions;
using Newtonsoft.Json;
using PracticeCRUD.Data.Services;
using PracticeCRUD.Enums;
using PracticeCRUD.Extentions;
using PracticeCRUD.Models;
using PracticeCRUD.Response;

namespace PracticeCRUD.Controllers
{
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly ICateloryService _service;
        public CategoryController(ICateloryService service)
        {
            _service = service;
        }

        [HttpGet]
        [Route("api/categories")]
        public async Task<IActionResult> GetAllCategory()
        {
            ResponseType<Category> res = new ResponseType<Category>();

            try
            {
                var categories = await _service.GetAllAsync();

                if (categories == null)
                {
                    res.Code = (int)ErrorEnum.DataNotFound;
                    res.Message = ErrorEnum.DataNotFound.Description();
                    return BadRequest(res);
                }

                res.Code = (int)ErrorEnum.Success;
                res.Message = ErrorEnum.Success.Description();
                res.Data = categories as List<Category>;
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
        [Route("api/categories/{id}")]
        public async Task<IActionResult> GetCategoryById(int id) 
        {
            ResponseType<Category> res = new ResponseType<Category>();

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
                res.Data = new List<Category>() { catelory };
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
        [Route("api/categories")]
        public async Task<IActionResult> AddCategory([FromBody] Category category)
        {
            ResponseType<Category> res = new ResponseType<Category>();
            try
            {
                await _service.AddAsync(category);
                res.Code = (int)ErrorEnum.Success;
                res.Message = ErrorEnum.Success.Description();
                res.Data = new List<Category>() { category };
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
        [Route("api/categories/{id}")]
        public async Task<IActionResult> UpdateCategory(int id, [FromBody]Category category)
        {
            ResponseType<Category> res = new ResponseType<Category>();
            try
            {
                bool isUpdate = await _service.UpdateAsync(id, category);
                if(!isUpdate)
                {
                    res.Code = (int)ErrorEnum.DataNotFound;
                    res.Message = ErrorEnum.DataNotFound.Description();
                    return BadRequest(res);
                }
                res.Code = (int)ErrorEnum.Success;
                res.Message = ErrorEnum.Success.Description();
                res.Data = new List<Category>() { category };
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
        [Route("api/categories/{id}")]
        public async Task<IActionResult> DeleteCategory(int id)
        {
            ResponseType<Category> res = new ResponseType<Category>();
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
