using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace CACET.Models {
    [Table("tbl_socios")]
    public class Socio {
        [Key, Column("socio_id")] public int SocioId { get; set; }
        [Column("apellidos_nombres")] public string ApellidosNombres { get; set; }
    }
}
