using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CACET.Migrations
{
    /// <inheritdoc />
    public partial class SincronizarModelos : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CasosJudiciales",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AbogadoAsignado = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    NumeroSocio = table.Column<int>(type: "int", nullable: false),
                    ApellidosNombres = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false),
                    NumeroPagare = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    MontoPagare = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    NumeroProceso = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    TipoProceso = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false, defaultValue: "EJECUTIVO"),
                    FechaCalificacion = table.Column<DateTime>(type: "datetime2", nullable: true),
                    MedidaCautelar = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false, defaultValue: "NINGUNA"),
                    Resolucion = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false, defaultValue: "EN TRAMITE"),
                    MandamientoEjecucion = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    UltimoMovimiento = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    Clasificacion = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false, defaultValue: "Procesos activos"),
                    FechaRegistro = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETDATE()"),
                    Estado = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false, defaultValue: "Activo"),
                    ObservacionesDiligencia = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CasosJudiciales", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CasosJudiciales");
        }
    }
}
