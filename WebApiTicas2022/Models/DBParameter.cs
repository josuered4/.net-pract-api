
using System.Data;

namespace WebApiTicas2022.Models
{
    public class DBParameter
    {
        //Especificacmos los parametros, mismos se declararan de la siguiente forma 
        public string Name { get; set; } // esta propiedad sera un parametro que utilizaremos en el context 2
        public ParameterDirection Direction { get; set; }//Es de tipo enumerable
        public object Value { get; set; }

        public DBParameter(string ParamName, ParameterDirection parameterDirection, object parameterValue)
        {
            Name = ParamName;
            Direction = parameterDirection;
            Value = parameterValue;
        }
        //Creamos un constructor para inicializar los atributos de la clase 
    }
}
