
namespace Lands.Backend.MYSQLSettings
{
    using System.Data.Common;
    using System.Data.Entity;
    using System.Data.Entity.Migrations.History;

    // esto es para manejar el historico de datos dentro de mysql
    public class MySqlHistoryContext : HistoryContext
    {
        public MySqlHistoryContext(DbConnection conn, string defaultSchema)
            : base(conn, defaultSchema)
        {

        }

        //Este metodo se ejecuta cada vez que el modelo de la base de datos
        //se está creando
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<HistoryRow>().Property(h => h.MigrationId).HasMaxLength(50).IsRequired();
            modelBuilder.Entity<HistoryRow>().Property(h => h.ContextKey).HasMaxLength(100).IsRequired();
        }
    }
}