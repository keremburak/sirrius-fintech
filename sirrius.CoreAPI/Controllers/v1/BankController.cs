using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using sirrius.Data.Entity;
using sirrius.Model;
using sirrius.Model.DataModel;
using sirrius.Service.Interfaces;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Utilities.Helper;

namespace sirrius.CoreAPI.Controllers.v1
{
    [Authorize("SuperAdmin,Admin,User")]
    [Route("api/v{version:apiVersion}/[controller]")]
    //[ApiVersion("1.0", Deprecated = true)]
    [ApiVersion("1.0")]
    [ApiController]
    public class BankController : BaseController
    {
        private readonly IBankService bankService;
        private readonly ILogService logService;
        private readonly ICountryService countryService;
        private readonly IEmailService emailService;
        private readonly IWebHostEnvironment hostingEnvironment;

        public BankController(IBankService bankService,
                              ILogService logService,
                              ICountryService countryService,
                              IEmailService emailService,
                              IWebHostEnvironment hostingEnvironment)
        {
            this.bankService = bankService;
            this.logService = logService;
            this.countryService = countryService;
            this.emailService = emailService;
            this.hostingEnvironment = hostingEnvironment;
        }
        [HttpPost("list")]
        //[Route("list")]
        public async Task<ActionResult<IEnumerable<Bank>>> GetBankListAsync([FromBody] Parameters parameters)
        {
            IEnumerable<Bank> banks = null;

            var model = await bankService.GetAllAsync();

            if (!string.IsNullOrEmpty(parameters.search))
                model = model.Where(q => SirriusHelper.stringContains(q.Code, parameters.search) ||
                                         SirriusHelper.stringContains(q.Name, parameters.search) ||
                                         SirriusHelper.stringContains(q.Description, parameters.search));

            if (!string.IsNullOrEmpty(parameters.orderBy))
            {
                if (parameters.orderBy.Contains(':'))
                    banks = model.OrderByDescending(q => q.GetType().GetProperty(parameters.orderBy.Split(':')[0]).GetValue(q, null));
                else
                    banks = model.OrderBy(q => q.GetType().GetProperty(parameters.orderBy).GetValue(q, null));
            }

            banks = banks.Skip(parameters.index * parameters.size).Take(parameters.size);

            return Ok(new GridResultModel<Bank> { items = banks.ToList(), count = model.Count() });
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetBankAsync(int id)
        {
            var bank = await bankService.GetByIdAsync(id);
            if (bank == null)
            {
                logService.LogInfo($"Bank with id: {id} doesn't exist in the database.");
                return NotFound();
            }

            return Ok(bank);
        }

        [HttpPost("create")]
        public async Task<IActionResult> CreateBankAsync([FromBody] Bank bank)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var result = await bankService.IsExist(bank.Code, "code");

                    if (result.exist)
                    {
                        logService.LogWarn(result.message);

                        return Conflict();
                    }

                    result = await bankService.IsExist(bank.Name);

                    if (result.exist)
                    {
                        logService.LogWarn(result.message);

                        return Conflict();
                    }

                    var createdBank = await bankService.CreateAsync(bank);

                    if (createdBank.Id > 0)
                    {
                        return Ok(createdBank.Id);
                    }
                    else
                    {
                        logService.LogError("Bank not created");
                        return NotFound();
                    }
                }
                catch (Exception ex)
                {
                    logService.LogError(ex.Message);
                    return BadRequest();
                }
            }

            logService.LogError("There is an error while creating Bank creation.");

            return BadRequest();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBankAsync(int id)
        {
            var bank = await bankService.GetByIdAsync(id);

            if (bank == null)
            {
                logService.LogError($"Bank with id: {id}, hasn't been found in db.");
                return NotFound();
            }

            await bankService.DeleteAsync(id);

            return Ok(new Model.DataModel.NoContentResult { StatusCode = StatusCodes.Status204NoContent, Mode = false, Message = "Delete" });
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateBankAsync(int id, [FromBody] Bank bank)
        {
            if (bank == null)
            {
                logService.LogError($"Bank with id: {id}, hasn't been found in db.");
                return NotFound();
            }

            if (!ModelState.IsValid)
            {
                logService.LogError("Invalid bank object sent from client.");
                return BadRequest("Invalid model object");
            }

            var currentBank = await bankService.GetByIdAsync(id);

            if (currentBank == null)
            {
                logService.LogError($"Bank with id: {id}, hasn't been found in db.");
                return NotFound();
            }

            await bankService.UpdateAsync(id, bank);

            return Ok(new Model.DataModel.NoContentResult { StatusCode = StatusCodes.Status204NoContent, Message = "Update" });
        }


        [HttpGet("entities/{id}")]
        public async Task<IActionResult> GetEntitiesById(int id)
        {
            var bank = await bankService.GetByIdAsync(id)
;
            if (bank == null)
            {
                logService.LogInfo($"Bank with id: {id} doesn't exist in the database.");
                return NotFound();
            }

            var countries = await countryService.GetAllAsync();

            var items = new Dictionary<string, List<SelectListItem>>();
            var list = new List<SelectListItem>();

            countries.ToList().ForEach(x => list.Add(new SelectListItem { Value = x.Id.ToString(), Text = x.Name }));
            items.Add("country", list);

            var selectedIds = new List<int>();
            selectedIds.Add(bank.CountryId);

            MultiSelectModel<Bank, string> model = new MultiSelectModel<Bank, string>
            {
                Item = bank,
                SelectedIds = selectedIds,
                Items = items
            };
            return Ok(model);
        }
        [HttpGet("entities")]
        public async Task<IActionResult> GetEntities()
        {
            var countries = await countryService.GetAllAsync();

            var items = new Dictionary<string, List<SelectListItem>>();
            var list = new List<SelectListItem>();

            countries.ToList().ForEach(x => list.Add(new SelectListItem { Value = x.Id.ToString(), Text = x.Name }));
            items.Add("country", list);

            var selectedIds = new List<int> { 0 };

            MultiSelectModel<Bank, string> model = new MultiSelectModel<Bank, string>
            {
                Item = new Bank(),
                SelectedIds = selectedIds,
                Items = items
            };

            return Ok(model);
        }


        [HttpGet("list2")]
        public async Task<ActionResult<IEnumerable<Bank>>> GetBankListAsync()
        {
            var model = await bankService.GetAllAsync();

            return Ok(new GridResultModel<Bank> { items = model.ToList(), count = model.Count() });
        }
    }
}
