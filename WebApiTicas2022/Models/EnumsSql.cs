using System.ComponentModel;
using System.Reflection;//Nos da funcines para saber de que tipo es un determinado objeto

namespace WebApiTicas2022.Models
{
    public class EnumsSql
    {
        public enum ExecuteType
        {
            ExecuteReader,
            ExecuteNonQuery,
            ExecuteScalar,

        }
            //LOs enumerables nos ayudan para clasificar siertos objetos o respuestas.
            // ExecuteReader, ExecuterNonQuery, ExecuteScalar son los enumerables mas conocidos.

        public enum SqlAction
        {
            [Description("Insertar") ]
            Insert = 1,
            [Description("Actualizar")]
            Update = 2,
            [Description("Eliminar")]
            Delete = 3,
            [Description("Seleccionar")]
            Select = 4,
        }

        public enum DbConnectionString
        {
            //Este lo podemos utilizar para las conexiones a las bases de datos 
            DbDemo = 1,
            DbEfoodies =2,
        }
        public static string GetDescription(Enum value)//Para la descripcion de los enumerables 
        {
            return value
                .GetType()//obtenemos el tipo
                .GetMember(value.ToString()) //sacamos el miembro pero convetido en string
                .FirstOrDefault() //Se tamra el primero 
                ?.GetCustomAttribute<DescriptionAttribute>()//obtener los atriburos 
                ?.Description!;//obtendemeos la descripcion
        }

    }
}
