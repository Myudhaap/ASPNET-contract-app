using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ContractApp.API.Models.Entities
{
    [Table("mst_jabatan")]
    public class Jabatan
    {
        [Key]
        [Column("kode_jabatan")]
        public string KodeJabatan { get; set; }

        [Column("nama_jabatan")]
        public string NamaJabatan { get; set; }
        public ICollection<Pegawai> PegawaiList { get; set; }
    }
}
