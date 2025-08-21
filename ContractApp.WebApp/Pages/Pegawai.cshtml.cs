using ContractApp.WebApp.Models;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ContractApp.WebApp.Pages
{
    public class PegawaiModel : PageModel
    {
        public IEnumerable<Pegawai> pegawaiList { get; set; }

        public async Task OnGet()
        {

        }
    }
}
