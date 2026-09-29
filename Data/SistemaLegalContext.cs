using Microsoft.EntityFrameworkCore;
using CACET.Models;
using System.Text.Json;

namespace CACET.Data
{
    public class SistemaLegalContext : DbContext
    {
        public SistemaLegalContext(DbContextOptions<SistemaLegalContext> options) : base(options) { }

        public DbSet<CasoJudicial> CasosJudiciales { get; set; }
        public DbSet<Auditoria> Auditorias { get; set; }
        public DbSet<Socio> Socios { get; set; }
        public DbSet<Abogado> Abogados { get; set; }
        public DbSet<Movimiento> Movimientos { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<CasoJudicial>(entity =>
            {
                entity.ToTable("dbo.tbl_CasosJudiciales");
                entity.HasKey(e => e.Id);
                entity.Property(e => e.AbogadoAsignado).IsRequired().HasMaxLength(10);
                entity.Property(e => e.ApellidosNombres).IsRequired().HasMaxLength(250);
                entity.Property(e => e.NumeroPagare).IsRequired().HasMaxLength(50);
                entity.Property(e => e.MontoPagare).HasColumnType("decimal(18,2)");
                entity.Property(e => e.NumeroProceso).IsRequired().HasMaxLength(100);
                entity.Property(e => e.TipoProceso).HasMaxLength(50).HasDefaultValue("EJECUTIVO");
                entity.Property(e => e.MedidaCautelar).HasMaxLength(150).HasDefaultValue("NINGUNA");
                entity.Property(e => e.Resolucion).HasMaxLength(100).HasDefaultValue("EN TRAMITE");
                entity.Property(e => e.MandamientoEjecucion).HasColumnType("decimal(18,2)");
                entity.Property(e => e.UltimoMovimiento).HasMaxLength(500);
                entity.Property(e => e.Clasificacion).HasMaxLength(50).HasDefaultValue("Procesos activos");
                entity.Property(e => e.FechaRegistro).HasDefaultValueSql("GETDATE()");
                entity.Property(e => e.Estado).HasMaxLength(30).HasDefaultValue("Activo");
            });
        }

        private void GuardarAuditoria()
        {
            ChangeTracker.DetectChanges();
            var entradasAuditoria = new List<Auditoria>();

            foreach (var entry in ChangeTracker.Entries())
            {
                if (entry.Entity is Auditoria || entry.State == EntityState.Detached || entry.State == EntityState.Unchanged)
                    continue;

                var auditoria = new Auditoria
                {
                    Tabla = entry.Metadata.GetTableName() ?? entry.Metadata.Name,
                    FechaHora = DateTime.Now
                };

                var valoresAntiguos = new Dictionary<string, object?>();
                var valoresNuevos = new Dictionary<string, object?>();

                foreach (var property in entry.Properties)
                {
                    string nombrePropiedad = property.Metadata.Name;

                    if (property.Metadata.IsPrimaryKey())
                    {
                        auditoria.RegistroId = property.CurrentValue?.ToString() ?? "";
                    }

                    switch (entry.State)
                    {
                        case EntityState.Added:
                            auditoria.Accion = "CREAR";
                            valoresNuevos[nombrePropiedad] = property.CurrentValue;
                            break;
                        case EntityState.Deleted:
                            auditoria.Accion = "ELIMINAR";
                            valoresAntiguos[nombrePropiedad] = property.OriginalValue;
                            break;
                        case EntityState.Modified:
                            if (property.IsModified)
                            {
                                if (auditoria.Accion != "ELIMINAR")
                                    auditoria.Accion = "ACTUALIZAR";
                                
                                if (nombrePropiedad == "Estado" && property.CurrentValue?.ToString() == "Eliminado")
                                    auditoria.Accion = "ELIMINAR";

                                valoresAntiguos[nombrePropiedad] = property.OriginalValue;
                                valoresNuevos[nombrePropiedad] = property.CurrentValue;
                            }
                            break;
                    }
                }

                if (valoresAntiguos.Count > 0)
                    auditoria.ValoresAntiguos = JsonSerializer.Serialize(valoresAntiguos);
                if (valoresNuevos.Count > 0)
                    auditoria.ValoresNuevos = JsonSerializer.Serialize(valoresNuevos);

                entradasAuditoria.Add(auditoria);
            }

            foreach (var audit in entradasAuditoria)
            {
                Auditorias.Add(audit);
            }
        }

        public override int SaveChanges()
        {
            GuardarAuditoria();
            return base.SaveChanges();
        }

        public override Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = default)
        {
            GuardarAuditoria();
            return base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);
        }
    }
}