using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApiTicas2022.Models;
using Microsoft.EntityFrameworkCore;


namespace WebApiTicas2022.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly TicasContext Context; // Creamos una variable de tipo contexto

        public ProductsController(TicasContext context)
        {
            Context = context; //inyectamos la dependencia context
        }

        [HttpPost]
        public async Task<ActionResult<Product>> Post(Product product)
        {
            Context.Products.Add(product);//se agrega un producto dentro del listado de productos

            await Context.SaveChangesAsync(); //es para tabajar en otro hilo 
                                              //se guardan los cambios
            return CreatedAtAction("GetProduct", new { id = product.Id }, product);//es como un redirect en Django
                                                                                   //Creacion de un producto, y retornamos el valor id
                                                                                   //param 1- Nombre de la accion, hace referencia a la accion o funcion que es la siguiente
                                                                                   //param 2- el dato a retornar en este caso el id
                                                                                   //param 3-que elemento a retornar osea el product
                                                                                   // Retorna un http 201, creacion de un objeto y el id
        }

        [HttpGet("{id}")] //indica que es una peticion get
        public async Task<ActionResult<Product>> GetProduct(int id) //Tambien se va a retornar un product
                                                                    //como parametro utilizaremos una id 
        {
            //Product product = await Context.Products.FindAsync(id); es lo mismo que lo siguiente pero solo se inidica que se crara una variale para almacenar el objeto creado
            var product = await Context.Products.FindAsync(id);//se crea una variable que almacenara el objeto indicado por el id 
            //Se verifica que si se aya encontrado el objeto almacenado.
            if (product == null)
            {
                return NotFound();
            }
            return product;
            //es una funcion que retorna un objeto por su id
        }
        /*
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Product>>> GetProducts() //Enumerable como una lista pero mas facil, se retornara una lista de productos 
        {
            var Products = await Context.Products.ToListAsync(); //creamos una variable con la lista de products, pero se debe de importar 
            return Products;

            //return await Context.Products.ToListAsync(); tambien puede funcionar de la siguiente manea EntityFrameworkCore
        }*/

        [HttpGet] //NUEVO GET
        public async Task<ActionResult<IEnumerable<ProductDTO>>> GetProducts() //Enumerable como una lista pero mas facil, se retornara una lista de productos 
        {
            return await Context.Products
                .Select(product =>(ProductDTO)product)
                .ToListAsync(); //creamos una variable con la lista de products, pero se debe de importar 

            //return await Context.Products.ToListAsync(); tambien puede funcionar de la siguiente manea EntityFrameworkCore
        }

        [HttpPut] // la mejor opcion para la actualizacion de instancias o registros
        public async Task<IActionResult> PutProduct(int id, ProductDTO productDTO)// Rercordar que son parametros y una clase es una plantilla para un tipo de dato
        {
            if (id != productDTO.Id) //validamos la existencia de un registro con ese id
            {
                return BadRequest();
            }

            var Product = await Context.Products.FindAsync(id);//hacemos otra busqueda, pero ahora para asignar el valor en una variable
            if(Product == null)
            {
                return NotFound();
            }
            Product.Name = productDTO.Name;// dATOS a modifica
            Product.UniPrice = productDTO.UnitPrice;
            
            try//lanzamos el try por si es puede haber un problema con la conexion de la base de datos
            {
                await Context.SaveChangesAsync();//seguardan los cambios 
            }
            catch (Exception e)//si ocurre el problema se guarda la excepcion
            {
                System.Console.WriteLine(e.Message);
            }
            return NoContent();// para no retornar algun contenido 
        }

        [HttpDelete] // para la eliminacion
        public async Task<ActionResult<ProductDTO>> DeleteProduct(int id)
        {
            var product = await Context.Products.FindAsync(id);//Se busca el producto 
            if (product == null)
            {
                return NotFound();
            }//se valida si allá algo 

            Context.Products.Remove(product);// se remueve el producto 
            await Context.SaveChangesAsync();// se guardan los cambios 
            return NoContent();//no se retorna ningun contendido 
        }

    }
}
