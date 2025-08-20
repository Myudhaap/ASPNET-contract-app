using ContractApp.API.Services;
using Microsoft.AspNetCore.Mvc;

namespace ContractApp.API.Controllers
{
    [ApiController]
    [Route("/api/v1/[controller]")]
    public class PegawaiController : ControllerBase
    {
        private readonly IPegawaiService _pegawaiService;

        public PegawaiController(IPegawaiService pegawaiService)
        {
            _pegawaiService = pegawaiService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll(
            [FromQuery(Name = "start_date")] DateOnly startDate,
            [FromQuery(Name = "end_date")] DateOnly endDate
        )
        {
            var data = await _pegawaiService.FindListPegawai(startDate, endDate);
            return Ok(data);
        }
    }
}
