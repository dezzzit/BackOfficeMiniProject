using System.Collections.Generic;
using System.Threading.Tasks;
using BackOfficeMiniProject.DataAccess.Repository;
using BackOfficeMiniProject.Reports.Models;
using Microsoft.AspNetCore.Mvc;

namespace BackOfficeMiniProjectCross.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [FormatFilter] // api/[controller]?format=xml
    public class SumOfInventoryController : ControllerBase
    {
        private readonly ISumOfInventoryRepository _sumOfInventoryRepository;

        public SumOfInventoryController(ISumOfInventoryRepository sumOfInventoryRepository)
        {
            _sumOfInventoryRepository = sumOfInventoryRepository;
        }

        // GET api/SumOfInventory
        [HttpGet(Name = "GetSumOfInventory")]
        [FormatFilter] // api/[controller]?format=xml
        public async Task<IActionResult> GetSumOfInventory()
        {
            return await Task.Run(
                () =>
                {
                    IEnumerable<SumOfInventory> sumOfInventory= _sumOfInventoryRepository.SumOfInventory;

                    if (sumOfInventory == null)
                    {
                        return BadRequest();
                    }

                    IActionResult actionResult = Ok(sumOfInventory);

                    return actionResult;
                });
        }
        
    }
}
