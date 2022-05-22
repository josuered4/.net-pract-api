using Microsoft.Data.SqlClient;
using System.Data;
using static WebApiTicas2022.Models.EnumsSql;
using System.Reflection;

namespace WebApiTicas2022.Models
{
    public class TicasContext2 //Crearemos un segundo contexto
    {
        // Se creara para la implementacion de 
        /*
         * 
         */
        private string ConnectionString;//para almacenar la conexion
        private SqlConnection Connection { get; set; } //Propiedad para la creacion de la conexion
        private SqlCommand Command { get; set; } //Propiedad para los comandos

        public List<DBParameter> OutParameters { get; private set; }
        
        /*
         * Acontinuacion se creara una lista de paramentros, para esto hay dos formas para su creacion
         * 
         */

        public TicasContext2()
        {
            ConnectionString = "Data Source=(localdb)\\mssqllocaldb;Initial Catalog=TicasDemoDB";
        }

        /*public void Get()
        {
            SqlConnection Connection = new SqlConnection(ConnectionString);
            Connection.Open();

            SqlCommand Command = new SqlCommand();//creamos una instancia de consulta 
            Command.Connection = Connection;//le agregamos la conexion
            Command.CommandText = "SELECT Id, Name, UniPrice, UnitsInStock, Discontinued FROM Products";//Ingresamos el comando 

            SqlDataReader Reader = Command.ExecuteReader();
            
            List<object> ListObjects = new List<object>();

            while (Reader.Read())
            {
                ListObjects.Add(Reader);
            }
            Connection.Dispose();
        }*/

        //Crearemos funciones globales para crear la coneccion y acciones de escritura.

        private void Open(int connectionDb) //Funcion para abrir una conexion   
        {
            try
            {
                switch (connectionDb)
                {
                    case (int)DbConnectionString.DbDemo:
                        ConnectionString = "Data Source=(localdb)\\mssqllocaldb;Initial Catalog=TicasDemoDB";
                        break;
                    /*case (int)DbConnectionString.DbEfoodies:
                        ConnectionString = ConnectionString = "Data Source=(localdb)\\mssqllocaldb;Initial Catalog=TicasDemoDB";
                        break;*/
                }
                Connection = new SqlConnection(ConnectionString);
                Connection.Open();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Close();
            }
        }

        private void Close()//se sierra la conexion 
        {
            if (Connection != null)
                Connection.Close();
        }

        private object ExecuteProcedure(string procedureName, ExecuteType executeType, List<DBParameter> parameters) //no se debe llama BbParameter sino DbParameter 
        {
            object returnObject = null!;
            if (Connection != null && Connection.State == System.Data.ConnectionState.Open)//si la conexion esta abierta
            {
                Command = new SqlCommand(procedureName, Connection); //procedure es el comantex o el procedimiento almacenado


                //Procedimiento almacenado 
                Command.CommandType = System.Data.CommandType.StoredProcedure; //asiganmos al comandtype el tipo de valor que sera el procedimiento almacencado

                if (parameters != null) //si los parametros son diferentes a nulo
                {
                    Command.Parameters.Clear();//Limpiamos los parametros 
                    foreach(DBParameter param in parameters) //recorremos en cada parameto 
                    {
                        SqlParameter sqlParameter = new SqlParameter(); //se instancia un sqlparameter para cada parametro
                        sqlParameter.ParameterName = param.Name == "return" //asignamos el nombre de cada parametro, el valor del parametro pasado precediendo de @
                            ? param.Name
                            : String.Concat("@", param.Name);
                        sqlParameter.Value = param.Value; //asigamos el valor 
                        sqlParameter.Direction = param.Direction; //asignamos la direccion
                        Command.Parameters.Add(sqlParameter); //añadimos todos los valores para la creacion de un parametro completo 

                        //por cada paramtro que enviemos se al

                    }

                }
                switch (executeType) //de tipo de excucion se vera que funcion utilizar no es lo mismo utilizar exucuteReader que el nonquery
                {
                    case ExecuteType.ExecuteReader:
                        returnObject = Command.ExecuteReader();
                        break;
                    case ExecuteType.ExecuteNonQuery:
                        returnObject = Command.ExecuteNonQuery();
                        break;
                    case ExecuteType.ExecuteScalar:
                        returnObject = Command.ExecuteScalar();
                        break;
                    default: break;
                }
            }
            return returnObject; //retornamos el objeto 
        }

        public T ExecuteSingle<T>(String procudureName, List<DBParameter> parameters, int connectionDB) where T : new()
        {
            Open(connectionDB);
            T returnObject = default(T)!;
            if(Connection.State == System.Data.ConnectionState.Open)
            {
                IDataReader Reader = (IDataReader)ExecuteProcedure(procudureName, ExecuteType.ExecuteReader, parameters);
                if (Reader.Read())
                {
                    returnObject = new T();
                    for(int i=0; i < Reader.FieldCount; i++)
                    {
                        PropertyInfo propertyInfo = typeof(T).GetProperty(Reader.GetName(i))!;//obtenemos el nombre de las propiedades
                        if(propertyInfo != null && Reader.GetValue(i) != DBNull.Value)
                        {
                            propertyInfo.SetValue(returnObject, Reader.GetValue(i), null);//
                        }
                    }
                }
                Reader.Close();
                ReturnOutParameters(parameters);
                Close();
            }
            return returnObject;
        }

        private void ReturnOutParameters(List<DBParameter> parameters)
        {
            if(Command.Parameters.Count > 0)
            {
                for(int i=0; i < Command.Parameters.Count; i++)
                {
                    DBParameter parameter = (from p in parameters
                                             where p.Name == (Command.Parameters[i].ParameterName).Replace("@", "")
                                             select p).FirstOrDefault()!;
                    parameter.Value = Command.Parameters[i].Value;
                }
            }
        }

        public List<T> ExecuteList<T>(String procedureName, List<DBParameter> parameters, int connectionDB) where T : new()
        {
            Open(connectionDB);
            List<T> list = new List<T>();
            if (Connection.State == System.Data.ConnectionState.Open)
            {
                IDataReader Reader = (IDataReader)ExecuteProcedure(procedureName, ExecuteType.ExecuteReader, parameters);
                while (Reader.Read())
                {
                    T tempObject = new T();
                    for (int i = 0; i < Reader.FieldCount; i++)
                    {
                        PropertyInfo propertyInfo = typeof(T).GetProperty(Reader.GetName(i))!;
                        if (propertyInfo != null && Reader.GetValue(i) != DBNull.Value)
                        {
                            propertyInfo.SetValue(tempObject, Reader.GetValue(i));
                        }
                    }
                    list.Add(tempObject);
                }
                Reader.Close();
                UpdateOutParameters();
                Close();
            }
            return list;
        }

        private void UpdateOutParameters()
        {
            if(Command.Parameters.Count > 0)
            {

                OutParameters = new List<DBParameter>();
                OutParameters.Clear();

                for(int i =0; i<Command.Parameters.Count; i++)
                {
                    OutParameters.Add(
                        new DBParameter(
                            Command.Parameters[i].ParameterName,
                            ParameterDirection.Output,
                            Command.Parameters[i].Value
                            )
                        );
                }
            }
        }

        public int ExecuteNonQuery(string procedureName, List<DBParameter> parameters, int connectionDb)
        {
            Open(connectionDb);
            int value = (int)ExecuteProcedure(procedureName, ExecuteType.ExecuteNonQuery, parameters);
            ReturnOutParameters(parameters);
            Close();
            
            return value;
        }
    }
}
