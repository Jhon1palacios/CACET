using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace CACET.Models {
    [Table("tbl_movimientos")]
    public class Movimiento {
        [Key, Column("movimiento_id")] public int MovimientoId { get; set; }
        [Column("proceso_id")] public int? ProcesoId { get; set; }
        [Column("fecha_movimiento")] public DateTime? FechaMovimiento { get; set; }
        [Column("descripcion_movimiento")] public string DescripcionMovimiento { get; set; }
    }
}
