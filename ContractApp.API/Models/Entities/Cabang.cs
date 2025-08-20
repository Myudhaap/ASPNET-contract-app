using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ContractApp.API.Models.Entities
{
    [Table("mst_cabang")]
    public class Cabang
    {
        [Key]
        [Column("kode_cabang")]
        public string KodeCabang { get; set; }

        [Column("nama_cabang")]
        public string NamaCabang { get; set; }

        public ICollection<Pegawai> PegawaiList { get; set; }
    }
}
