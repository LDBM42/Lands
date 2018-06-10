namespace Lands.Backend.MYSQLSettings
{
    using System.Data.Entity;

    //se hereda de DbConfiguration para editar parametros
    //de EntityFramework
    public class MySQLEFConfiguration : DbConfiguration
    {
        public MySQLEFConfiguration()
        {
            // aquí se configura el HistoryContext (que es el que se utiliza
            //para el migrations. Lo que hace es configurarlo para el proveedor
            //MySql.Data.MySqlClient de acuerdo a lo que  se puso en la clase
            //MySqlHistoryContext
            SetHistoryContext("MySql.Data.MySqlClient", 
                (conn, schema) => new MySqlHistoryContext(conn, schema));
        }
    }
}