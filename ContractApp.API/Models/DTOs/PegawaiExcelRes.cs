using ContractApp.API.Constants;
using System.Text.Json.Serialization;

namespace ContractApp.API.Models.DTOs
{
    public class PegawaiExcelRes
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
        public EStatus Status { get; set; }
    }
}
