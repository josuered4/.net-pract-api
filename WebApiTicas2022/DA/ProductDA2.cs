using WebApiTicas2022.Models;
using static WebApiTicas2022.Models.EnumsSql;

namespace WebApiTicas2022.DA
{
    public class ProductDA2
    {
        public Product GetProduct(int Id) //funcion para retornar un product en especifico
        {
            List<DBParameter> dBParameters = new List<DBParameter>();
            dBParameters.Add(new DBParameter("Id_param", System.Data.ParameterDirection.Input, Id));

            Product product = new TicasContext2().ExecuteSingle<Product>("[dbo].[GetProductById]", dBParameters, (int)DbConnectionString.DbDemo);
            return product;
            //Los procedimientos almacendados, tiene como funcionalidad de mandar las funciones en el GestorDB y no en el codigo.
        }
        
        public List<Product> GetProducts()
        {
            List<DBParameter> dBParameters = new List<DBParameter>();
            List<Product> products = new TicasContext2().ExecuteList<Product>("[dbo].[GetProducts]", dBParameters, (int)DbConnectionString.DbDemo);
            return products;
        }
        
        public Product Create(Product product)
        {
            List<DBParameter> dBParameters = new List<DBParameter>();
            dBParameters.Add(new DBParameter("Id_param",System.Data.ParameterDirection.Output, product.Id));
            dBParameters.Add(new DBParameter("Name_param", System.Data.ParameterDirection.Input, product.Name!));
            dBParameters.Add(new DBParameter("UniPrice_param", System.Data.ParameterDirection.Input, product.UniPrice));
            dBParameters.Add(new DBParameter("UnitsInStock_param", System.Data.ParameterDirection.Input, product.UnitsInStock));
            dBParameters.Add(new DBParameter("Discontinued_param", System.Data.ParameterDirection.Input, product.Discontinued));

            Product result = new TicasContext2()
                .ExecuteNonQuery(
                "[dbo].[CreateProduct]", 
                dBParameters,(int)DbConnectionString.DbDemo) > 0? product:null!;

            DBParameter OutputResultId = dBParameters.First(p => p.Name == "Id_param");
            result.Id = OutputResultId != null ? (int)OutputResultId.Value : 0;
            return result;
        }

        public bool DeleteProduct(int Id)
        {
            List<DBParameter> dBParameters = new List<DBParameter>();
            dBParameters.Add(new DBParameter("Id_param", System.Data.ParameterDirection.Input, Id));

            var result = new TicasContext2().ExecuteNonQuery("[dbo].[DeleteProduct]", dBParameters, (int)DbConnectionString.DbDemo);

            if (result > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public Product UpdateProduct(Product product)
        {
            List<DBParameter> dBParameters = new List<DBParameter>();
            dBParameters.Add(new DBParameter("Id_param", System.Data.ParameterDirection.Input, product.Id));
            dBParameters.Add(new DBParameter("Name_param", System.Data.ParameterDirection.Input, product.Name));
            dBParameters.Add(new DBParameter("UniPrice_param", System.Data.ParameterDirection.Input, product.UniPrice));
            dBParameters.Add(new DBParameter("UnitsInStock_param", System.Data.ParameterDirection.Input, product.UnitsInStock));
            dBParameters.Add(new DBParameter("Discontinued_param", System.Data.ParameterDirection.Input, product.Discontinued));

            var result = new TicasContext2().ExecuteNonQuery("[dbo].[UpdateProduct]", dBParameters, (int)DbConnectionString.DbDemo);

            if (result > 0)
            {
                return GetProduct(product.Id);
            }
            else
            {
                Product product1 = new Product();
                return product1;
            }
        }
    }
}
