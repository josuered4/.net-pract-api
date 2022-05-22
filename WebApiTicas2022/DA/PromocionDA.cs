using Microsoft.Data.SqlClient;
using WebApiTicas2022.DTO;
using WebApiTicas2022.Models;

namespace WebApiTicas2022.DA
{
    public class PromocionDA
    {
        private SqlConnection? Connection { get; set; }
        private void open()
        {
            try
            {
                string ConnectionString = "Data Source=(localdb)\\mssqllocaldb;Initial Catalog=TicasDemoDB";
                Connection = new SqlConnection(ConnectionString);
                Connection.Open();
            }catch (Exception ex)
            {
               Console.WriteLine("Error en la conexion de la base de datos \n "+ex.Message);
                close();
            }
        }

        private void close()
        {
            if (Connection != null)
            {
                Connection.Close();
            }
        }
        /*
        public Promoción createPromoción( Promoción promoción)
        {
            open();
            string comanSql = "INSERT INTO Promocions VALUES "
        }*/
    }
}
