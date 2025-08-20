using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ContractApp.API.Models.Entities
{
    [Table("mst_pegawai")]
    public class Pegawai
    {
        [Key]
        [Column("kode_pegawai")]
        public int KodePegawai { get; set; }

        [Column("nama_pegawai")]
        [MaxLength(255)]
        public string NamaPegawai { get; set; }

        [Column("start_contract")]
        public DateOnly StartContract { get; set; }

        [Column("end_contract")]
        public DateOnly EndContract { get; set; }

        [Column("kode_cabang")]
        public string KodeCabang { get; set; }

        [ForeignKey("KodeCabang")]
        public Cabang Cabang { get; set; }

        [Column("kode_jabatan")]
        public string KodeJabatan { get; set; }

        [ForeignKey("KodeJabatan")]
        public Jabatan Jabatan { get; set; }
    }
}
