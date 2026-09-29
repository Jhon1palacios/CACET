using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace CACET.Models {
    [Table("tbl_abogados")]
    public class Abogado {
        [Key, Column("abogado_id")] public int AbogadoId { get; set; }
        [Column("siglas")] public string Siglas { get; set; }
    }
}
