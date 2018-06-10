namespace Lands.Domain
{
    using System.Data.Entity;

    public class DataContext : DbContext
    {
        // al constructor le pasamos el parametro del superconstructor
        // a base le pasamos el nombre del String de conexion que
        //especificamos en el Web.config (DefaultConnection)
        public DataContext() : base("DefaultConnection")
        {

        }
    }
}
