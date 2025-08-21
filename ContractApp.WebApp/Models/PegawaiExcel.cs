using System.Text.Json.Serialization;

namespace ContractApp.WebApp.Models
{
    public class PegawaiExcel
    {
        public string KodePegawai { get; set; } = null;
        public string NamaPegawai { get; set; } = null;
        public string KodeCabang { get; set; } = null;
        public string NamaCabang { get; set; } = null;
        public string KodeJabatan { get; set; } = null;
        public string NamaJabatan { get; set; } = null;
        public DateOnly? StartContract { get; set; }
        public DateOnly? EndContract { get; set; }
        [JsonConverter(typeof(JsonStringEnumConverter))]
        public string Status { get; set; } = null;
    }
}
