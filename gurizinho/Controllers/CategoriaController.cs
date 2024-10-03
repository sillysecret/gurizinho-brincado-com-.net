using gurizinho.Context;
using gurizinho.models;
using gurizinho.Pagination;
using gurizinho.Repository;
using gurizinho.Repository.UFW;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace gurizinho.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class CategoriaController : ControllerBase
    {
        private readonly IUnitOfWork unitOfWork;

        public CategoriaController(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork; 
        }

        [HttpGet]
        public ActionResult<IEnumerable<Categoria>> GetAll() 
        {
            //fetch com produtos
            var list = unitOfWork.CategoriaRepository.GetAll();

            if (list is null)
                return NotFound();

            return Ok(list);
        }


        [HttpGet("paginate")]
        public ActionResult<IEnumerable<Categoria>> GetPage([FromQuery] PageCategoriaParameters prm)
        {
            
            var list = unitOfWork.CategoriaRepository.GetCategoriaPage(prm);

            if (list is null)
                return NotFound();

            return Ok(list);
        }

        [HttpGet("filter/nome")]
        public ActionResult<IEnumerable<Categoria>> GetByName([FromQuery] FiltroCategoriaPorNome prm)
        {
            if (prm is null)
            {
                return BadRequest("Não pode ser null");
            }

            var categorias = unitOfWork.CategoriaRepository.GetCategoriaPorNome(prm);


            var metadata = new
            {
                categorias.TotalCount,
                categorias.PageSize,
                categorias.CurrentPage,
                categorias.TotalPages,
                categorias.HasNext,
                categorias.HasPrevious
            };

            Response.Headers.Append("X-Pagination", JsonConvert.SerializeObject(metadata));


            return Ok(categorias);

        }

        //validação de parametros do endpoint 
        [HttpGet("{id:int:min(1)}", Name = "ObterCategoria")]
        public ActionResult<Categoria> GetCategoria(int id)
        {
            //AsNoTracking faz com que ele não armazene no chash do contexto assim ela fica mais rapida mas so deve ser usada quando n vai interagir com aquele dado(escrever)
            var categoria = unitOfWork.CategoriaRepository.Get(categoria => categoria.CategoriaId == id);

            if (categoria is null)
            {
                return NotFound("produto não encontrado");
            }

            return categoria;
        }

        [HttpPost]
        public ActionResult CreateCategorias(Categoria newCategoria)
        {
            if (newCategoria is null)
                return BadRequest();
            
            var created = unitOfWork.CategoriaRepository.Create(newCategoria);
            unitOfWork.commit();

            return new CreatedAtRouteResult("ObterProduto", new { id = created.CategoriaId }, created);
        }

        [HttpPut("{id:int}")]
        public ActionResult UpdateCategoria(int id, Categoria newCategoria)
        {

            if (id != newCategoria.CategoriaId)
                return BadRequest();

            var updated = unitOfWork.CategoriaRepository.Update(newCategoria);
            unitOfWork.commit();

            return Ok(updated);
        }

        [HttpDelete("{id:int}")]
        public ActionResult<Categoria> DeleteCategoria(int id)
        {
            var categoria = unitOfWork.CategoriaRepository.Get(categoria => categoria.CategoriaId == id);

            if (categoria is null)
            {
                return NotFound("produto não encontrado");
            }

            var deleted = unitOfWork.CategoriaRepository.Delete(categoria);
            unitOfWork.commit();


            return Ok(deleted);
        }


    }
}
