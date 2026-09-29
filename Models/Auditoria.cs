using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CACET.Models
{
    [Table("tbl_auditoria")]
    public class Auditoria
    {
        [Key]
        public int Id { get; set; }
        
        [MaxLength(50)]
        public string Accion { get; set; } = string.Empty; // Crear, Actualizar, Eliminar
        
        [MaxLength(100)]
        public string Tabla { get; set; } = string.Empty;
        
        [MaxLength(50)]
        public string RegistroId { get; set; } = string.Empty;
        
        public string? ValoresAntiguos { get; set; }
        
        public string? ValoresNuevos { get; set; }
        
        public DateTime FechaHora { get; set; } = DateTime.Now;
        
        [MaxLength(100)]
        public string Usuario { get; set; } = "Sistema";
    }
}
