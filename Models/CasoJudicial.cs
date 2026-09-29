using System;

namespace CACET.Models
{
    public class CasoJudicial
    {
        public int Id { get; set; }

        // Iniciales del abogado asignado (columna "ABG." del Excel). Ej: "F.Z"
        public string AbogadoAsignado { get; set; } = string.Empty;

        // Columna "SOCIO"
        public int NumeroSocio { get; set; }

        // Columna "APELLIDOS Y NOMBRES"
        public string ApellidosNombres { get; set; } = string.Empty;

        // Columna "NÚMERO DEL PAGARÉ"
        public string NumeroPagare { get; set; } = string.Empty;

        // Columna "MONTO DEL PAGARÉ"
        public decimal MontoPagare { get; set; }

        // Columna "NÚMERO/TIPO DE PROCESO JUDICIAL" -> número
        public string NumeroProceso { get; set; } = string.Empty;

        // Tipo de proceso como TEXTO (no enum): EJECUTIVO, CONCURSAL, ORDINARIO, MONITORIO, VOLUNTARIO.
        // La jerarquía de color (naranja fuerte -> suave) la decide la Vista según este texto,
        // igual que ya hace _DetalleCaso.cshtml con GetBadgeProceso(string tipo).
        public string TipoProceso { get; set; } = "EJECUTIVO";

        // Columna "CALIFICACIÓN DE LA DEMANDA"
        public DateTime? FechaCalificacion { get; set; }

        // Columna "MEDIDA CAUTELAR"
        public string MedidaCautelar { get; set; } = "NINGUNA";

        // Columna "RESOLUCIÓN"
        public string Resolucion { get; set; } = "EN TRAMITE";

        // Columna "MANDAMIENTO DE EJECUCIÓN"
        public decimal? MandamientoEjecucion { get; set; }

        // Columna "ÚLTIMO MOVIMIENTO"
        public string UltimoMovimiento { get; set; } = string.Empty;

        // Agrupación interna: "Procesos activos", "Procesos finalizados", "Acuerdos de pago"
        public string Clasificacion { get; set; } = "Procesos activos";

        // Fecha en que el caso se registró en el sistema (no confundir con FechaCalificacion)
        public DateTime FechaRegistro { get; set; } = DateTime.Now;

        // "Activo", "Inactivo", "Archivado"
        public string Estado { get; set; } = "Activo";

        // Columna "OBSERVACIONES/SIGUIENTE DILIGENCIA A REALIZAR"
        public string ObservacionesDiligencia { get; set; } = string.Empty;
    }
}