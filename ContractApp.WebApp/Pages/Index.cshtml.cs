using ContractApp.WebApp.Config;
using ContractApp.WebApp.Models;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Options;

namespace ContractApp.WebApp.Pages
{
    public class IndexModel : PageModel
    {
        private readonly HttpClient _httpClient;
        private readonly ILogger<IndexModel> _logger;
        private readonly ApiSettings _apiSettings;

        public PegawaiCount PegawaiCount { set; get; }

        public IndexModel(ILogger<IndexModel> logger, IHttpClientFactory httpClientFactory, IOptions<ApiSettings> apiSettings)
        {
            _logger = logger;
            _httpClient = httpClientFactory.CreateClient();
            _apiSettings = apiSettings.Value;
        }

        public async Task OnGetAsync()
        {
            try
            {
                var res = await _httpClient.GetFromJsonAsync<BaseResponse<PegawaiCount>>(
                    $"{_apiSettings.BaseUrl}/api/v1/pegawai/count"
                );

                PegawaiCount = new PegawaiCount
                {
                    Active = res.data.Active,
                    NotActive = res.data.NotActive
                };

            } catch (Exception e)
            {
                PegawaiCount = new PegawaiCount
                {
                    Active = 0,
                    NotActive = 0
                };
            }
        }
    }
}
