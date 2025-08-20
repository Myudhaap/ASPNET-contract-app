using ContractApp.API.Commons;
using ContractApp.API.Models.DTOs;
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
            var response = new BaseResponse<IEnumerable<PegawaiRes>>("Success get pegawai", 200, data);
            return Ok(response);
        }

        [HttpPost("upload")]
        public async Task<IActionResult> UploadExcel(
            IFormFile file
        )
        {
            var data = await _pegawaiService.SaveExcelPegawai(file);
            var response = new BaseResponse<IEnumerable<PegawaiExcelRes>>("Success upload excel pegawai", 201, data);
            return Ok(response);
        }
    }
}
