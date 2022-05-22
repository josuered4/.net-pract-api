using Microsoft.Data.SqlClient;
using WebApiTicas2022.Models;
namespace WebApiTicas2022.DA
{
    public class ProductDA
    {
        public List<Product> GetProducts()
        {
            //Creamos una Instancia de Connection, Para usarla como conextion
            SqlConnection Connection = new SqlConnection("Data Source=(localdb)\\mssqllocaldb;Initial Catalog=TicasDemoDB");// conexion con una base de datos mediante ado
            Connection.Open();//abrimos la conexion

            SqlCommand Command = new SqlCommand();//creamos una instancia de consulta 
            Command.Connection = Connection;//le agregamos la conexion
            Command.CommandText = "SELECT Id, Name, UniPrice, UnitsInStock, Discontinued FROM Products";//Ingresamos el comando 

            SqlDataReader Reader = Command.ExecuteReader();//Ejecuamos el Comando y lo almacenamos dentro de una variable
            List<Product> Products = new List<Product>();//Creamos una lista para poder almacenar los productos, sera una lista de tipo Product

            while (Reader.Read())//Sacamos cada dato para asignarlos en la lista de arriba
            {
                Products.Add(new Product
                {
                    Id = Convert.ToInt32(Reader[0]),//sacamos cada datos y lo asginamos en la lista 
                    Name = (string)Reader[1],
                    UniPrice = Convert.ToDecimal(Reader[2]),
                    UnitsInStock = Convert.ToDecimal(Reader[3]),
                    Discontinued = Convert.ToBoolean(Reader[4]),
                });
            }
            Connection.Dispose();//cerramos la conexion
            return Products; // retornamos la lista con los datos 
        }

        public Product AddProducts(Product product)//Retornamos un Produt
        {
            SqlConnection Connection = new SqlConnection("Data Source=(localdb)\\mssqllocaldb;Initial Catalog=TicasDemoDB");// conexion con una base de datos mediante ado
            Connection.Open();//abrimos la conexion

            string insertProduct = "INSERT INTO Products (Name, UniPrice, UnitsInStock, Discontinued)" +
                "output INSERTED.Id " +
                "VALUES (@Name, @UniPrice, @UnitsInStock, @Discontinued)";

            SqlCommand Command = new SqlCommand();//creamos una instancia de consulta 
            Command.Connection = Connection;//le agregamos la conexion
            Command.CommandText = insertProduct;//Ingresamos el comando 

            Command.Parameters.Add(new SqlParameter("@Name", product.Name));//sacamos los parametros
            Command.Parameters.Add(new SqlParameter("@UniPrice", product.UniPrice));
            Command.Parameters.Add(new SqlParameter("@UnitsInStock", product.UnitsInStock));
            Command.Parameters.Add(new SqlParameter("@Discontinued", product.Discontinued));

            var valueReturnProduct = Command.ExecuteNonQuery();
            Connection.Dispose();
            if (valueReturnProduct > 0)
            {
                return product; // si se agrego retornamos el product
            }
            else
            {
                throw new Exception();//sino no se agrego retornamos una excepcion
            }

        }

        public bool DeleteProduct(int Id)
        {
            SqlConnection Connection = new SqlConnection("Data Source=(localdb)\\mssqllocaldb;Initial Catalog=TicasDemoDB");// conexion con una base de datos mediante ado
            Connection.Open();//abrimos la conexion

            string DeleProduct = "DELETE FROM Products WHERE Id = @Id";

            SqlCommand Command = new SqlCommand();//creamos una instancia de consulta 
            Command.Connection = Connection;//le agregamos la conexion
            Command.CommandText = DeleProduct;//Ingresamos el comando 
            Command.Parameters.AddWithValue("@Id", Id);

            var TablesAfected = Command.ExecuteNonQuery();
            if (TablesAfected > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        
        public Product SearchProduct(int Id)
        {
            SqlConnection connection = new SqlConnection("Data Source=(localdb)\\mssqllocaldb;Initial Catalog=TicasDemoDB");
            connection.Open();

            string searchProduct = "SELECT Id, Name, UniPrice, UnitsInStock, Discontinued FROM Products WHERE Id = @Id";

            SqlCommand command = new SqlCommand();
            command.Connection = connection;
            command.CommandText = searchProduct;

            command.Parameters.Add(new SqlParameter("@Id", Id));
            Product product = new Product(); //se debe precrear una variable donde se alamacenara el resultado 

            SqlDataReader reader = command.ExecuteReader();

            if (reader.Read())
            {
                product = new Product()//reasignamos el valor la bariable antes creada 
                {
                    Id = (int)reader["Id"],
                    Name = (string)reader["Name"],
                    UniPrice = (decimal)reader["UniPrice"],
                    UnitsInStock = (decimal)reader["UnitsInStock"],
                    Discontinued = (bool)reader["Discontinued"]
                };
            };
            if(product == null)
            {
                throw new ArgumentOutOfRangeException();
            }
            connection.Dispose();
            return product;
        }

        public Product UpdateProduct(Product product)
        {
            SqlConnection connection = new SqlConnection("Data Source=(localdb)\\mssqllocaldb;Initial Catalog=TicasDemoDB");
            connection.Open();

            string UpdateProduct = "UPDATE Products SET" +
                " Name = @Name, " +
                "UniPrice  = @UniPrice, " +
                "UnitsInStock  = @UnitsInStock, " +
                "Discontinued  = @Discontinued " +
                "WHERE Id = @Id ";
            SqlCommand command = new SqlCommand();
            command.Connection= connection;
            command.CommandText = UpdateProduct;

            command.Parameters.Add(new SqlParameter("@Name", product.Name));
            command.Parameters.Add(new SqlParameter("@UniPrice", product.UniPrice));
            command.Parameters.Add(new SqlParameter("@UnitsInStock", product.UnitsInStock));
            command.Parameters.Add(new SqlParameter("@Discontinued", product.Discontinued));
            command.Parameters.Add(new SqlParameter("@Id", product.Id));

            var arrowAffect = command.ExecuteNonQuery();
            connection.Dispose();

            if(arrowAffect > 0)
            {
                return SearchProduct(product.Id);
            }
            else
            {
                throw new ArgumentOutOfRangeException();
            }
        }

    }
}
