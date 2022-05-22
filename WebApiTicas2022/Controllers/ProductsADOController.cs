using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApiTicas2022.DA;
using WebApiTicas2022.Models;
namespace WebApiTicas2022.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsADOController : ControllerBase
    {
        private TicasContext2? Context; // Creamos una variable de tipo contexto

        /*public ProductsADOController(TicasContext2 context)
        {
            Context = context; //inyectamos la dependencia context
        }*/
        /*private readonly TicasContext _context;

        public ProductsADOController(TicasContext ticasContext)
        {
            _context = ticasContext;
        }*/


        [HttpGet]
        /*
        public ActionResult<List<Product>> GetProducts()
        {
            Context = new TicasContext2();
            Context.Get(); //Consualtamos la funcion get desde el context de tICAScontext2
            ProductDA productDA = new ProductDA();//instanciamos la entidad que creamos
            return productDA.GetProducts();//llamamos a get
        }*/
        public ActionResult<List<Product>> GetProducts()
        {
            ProductDA2 productDA2 = new ProductDA2();
            return productDA2.GetProducts();
        }

        /*
        [HttpDelete]
        public ActionResult<string> DeleteProductADO(int Id)
        {
            ProductDA productDA = new ProductDA();
            if (productDA.DeleteProduct(Id) == true)
            {
                return "Elemento eliminado";
            }
            return "error en la eliminacion";
            
        }*/
        [HttpDelete]
        public ActionResult<string> DeleteProductADO(int Id)
        {
            ProductDA2 productDA2 = new ProductDA2();
            if (productDA2.DeleteProduct(Id))
            {
                return "Producto Eliminado :)";
            }
            return "Error al eliminar el producto :v";
        }

        /*
        [HttpPost]
        public ActionResult<Product> AddProduct(Product product)
        {
            ProductDA productDA = new ProductDA();
            return productDA.AddProducts(product);
        }*/

        [HttpPost]
        public ActionResult<Product> AddProduct(Product product)
        {
            ProductDA2 productDA2 = new ProductDA2();
            return productDA2.Create(product);
        }


        /*
        [HttpGet("{Id}")]
        public ActionResult<Product> GetProductADO(int Id)
        {
            ProductDA productDA =new ProductDA();
            return productDA.SearchProduct(Id);
        }*/

        [HttpGet("{Id}")]
        public ActionResult<Product> GetProductADO(int Id)
        {
            ProductDA2 productDA = new ProductDA2();
            return productDA.GetProduct(Id);
        }

        /*
        [HttpPut]
        public ActionResult<Product> UpdateProductADO(Product product)
        {
            ProductDA productDA = new ProductDA();
            return productDA.UpdateProduct(product);
        }*/
        [HttpPut]
        public ActionResult<Product> UpdateProductADO(Product product)
        {
            ProductDA2 productDA2 = new ProductDA2();
            return productDA2.UpdateProduct(product);
        }
    }
}
