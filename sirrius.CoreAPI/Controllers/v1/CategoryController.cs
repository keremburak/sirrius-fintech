using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using sirrius.Data.Entity;
using sirrius.Model.DataModel;
using sirrius.Service.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Utilities.Helper;

namespace sirrius.CoreAPI.Controllers.v1
{
    [Authorize("SuperAdmin,Admin")]
    [Route("api/v{version:apiVersion}/[controller]")]
    //[ApiVersion("1.0", Deprecated = true)]
    [ApiVersion("1.0")]
    [ApiController]
    public class CategoryController : BaseController
    {
        private readonly ICategoryService categoryService;
        private readonly ILogService logService;
        public CategoryController(ICategoryService categoryService, ILogService logService)
        {
            this.categoryService = categoryService;
            this.logService = logService;
        }

        [HttpPost("list")]
        //[Route("list")]
        public async Task<ActionResult<IEnumerable<Category>>> GetCategoryListAsync([FromBody] Parameters parameters)
        {
            IEnumerable<Category> categories = null;

            var model = await categoryService.GetAllAsync();

            if (!string.IsNullOrEmpty(parameters.search))
                model = model.Where(q => //CRMHelper.stringContains(q.Code, parameters.search) ||
                                         SirriusHelper.stringContains(q.Name, parameters.search));
            //CRMHelper.stringContains(q.Description, parameters.search));

            if (!string.IsNullOrEmpty(parameters.orderBy))
            {
                if (parameters.orderBy.Contains(':'))
                    categories = model.OrderByDescending(q => q.GetType().GetProperty(parameters.orderBy.Split(':')[0]).GetValue(q, null));
                else
                    categories = model.OrderBy(q => q.GetType().GetProperty(parameters.orderBy).GetValue(q, null));
            }

            categories = categories.Skip(parameters.index * parameters.size).Take(parameters.size);

            return Ok(new GridResultModel<Category> { items = categories.ToList(), count = model.Count() });
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetCategoryAsync(int id)
        {
            var category = await categoryService.GetByIdAsync(id);
            if (category == null)
            {
                logService.LogInfo($"Category with id: {id} doesn't exist in the database.");
                return NotFound();
            }

            return Ok(category);
        }

        [HttpPost("create")]
        public async Task<IActionResult> CreateCategoryAsync([FromBody] Category category)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var result = await categoryService.IsExist(category.Name);

                    if (result.exist)
                    {
                        logService.LogWarn(result.message);

                        return Conflict();
                    }

                    var createdCategory = await categoryService.CreateAsync(category);

                    if (createdCategory.Id > 0)
                    {
                        return Ok(createdCategory.Id);
                    }
                    else
                    {
                        logService.LogError("Category not created");
                        return NotFound();
                    }
                }
                catch (Exception ex)
                {
                    logService.LogError(ex.Message);
                    return BadRequest();
                }
            }

            logService.LogError("There is an error while creating Category creation.");

            return BadRequest();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCategoryAsync(int id)
        {
            var category = await categoryService.GetByIdAsync(id);

            if (category == null)
            {
                logService.LogError($"Category with id: {id}, hasn't been found in db.");
                return NotFound();
            }

            await categoryService.DeleteAsync(id);

            return Ok(new Model.DataModel.NoContentResult { StatusCode = StatusCodes.Status204NoContent, Mode = false, Message = "Delete" });
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateCategoryAsync(int id, [FromBody] Category category)
        {
            if (category == null)
            {
                logService.LogError($"Category with id: {id}, hasn't been found in db.");
                return NotFound();
            }

            if (!ModelState.IsValid)
            {
                logService.LogError("Invalid category object sent from client.");
                return BadRequest("Invalid model object");
            }

            var currentCategory = await categoryService.GetByIdAsync(id);

            if (currentCategory == null)
            {
                logService.LogError($"Category with id: {id}, hasn't been found in db.");
                return NotFound();
            }

            await categoryService.UpdateAsync(id, category);

            return Ok(new Model.DataModel.NoContentResult { StatusCode = StatusCodes.Status204NoContent, Message = "Update" });
        }

    }
}
