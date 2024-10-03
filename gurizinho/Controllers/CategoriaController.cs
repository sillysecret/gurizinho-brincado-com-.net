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
        public async Task<ActionResult<IEnumerable<Categoria>>> GetAll() 
        {
            //fetch com produtos
            var list = await unitOfWork.CategoriaRepository.GetAllAsync();

            if (list is null)
                return NotFound();

            return Ok(list);
        }


        [HttpGet("paginate")]
        public async Task<ActionResult<IEnumerable<Categoria>>> GetPage([FromQuery] PageCategoriaParameters prm)
        {
            
            var list = await unitOfWork.CategoriaRepository.GetCategoriaPageAsync(prm);

            if (list is null)
                return NotFound();

            var metadata = new
            {
                list.Count,
                list.PageSize,
                list.TotalItemCount,
                list.PageCount,
                list.HasNextPage,
                list.HasPreviousPage
            };

            Response.Headers.Append("X-Pagination", JsonConvert.SerializeObject(metadata));

            return Ok(list);
        }

        [HttpGet("filter/nome")]
        public async Task<ActionResult<IEnumerable<Categoria>>> GetByName([FromQuery] FiltroCategoriaPorNome prm)
        {
            if (prm is null)
            {
                return BadRequest("Não pode ser null");
            }

            var categorias = await unitOfWork.CategoriaRepository.GetCategoriaPorNomeAsync(prm);


            var metadata = new
            {
                categorias.Count,
                categorias.PageSize,
                categorias.TotalItemCount,
                categorias.PageCount,
                categorias.HasNextPage,
                categorias.HasPreviousPage
            };

            Response.Headers.Append("X-Pagination", JsonConvert.SerializeObject(metadata));


            return Ok(categorias);

        }

        //validação de parametros do endpoint 
        [HttpGet("{id:int:min(1)}", Name = "ObterCategoria")]
        public async Task<ActionResult<Categoria>> GetCategoria(int id)
        {
            //AsNoTracking faz com que ele não armazene no chash do contexto assim ela fica mais rapida mas so deve ser usada quando n vai interagir com aquele dado(escrever)
            var categoria = await unitOfWork.CategoriaRepository.GetAsync(categoria => categoria.CategoriaId == id);

            if (categoria is null)
            {
                return NotFound("produto não encontrado");
            }

            return categoria;
        }

        [HttpPost]
        public async Task<ActionResult> CreateCategorias(Categoria newCategoria)
        {
            if (newCategoria is null)
                return BadRequest();
            
            var created = unitOfWork.CategoriaRepository.Create(newCategoria);
            await unitOfWork.commitAsync();

            return new CreatedAtRouteResult("ObterProduto", new { id = created.CategoriaId }, created);
        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult> UpdateCategoria(int id, Categoria newCategoria)
        {

            if (id != newCategoria.CategoriaId)
                return BadRequest();

            var updated = unitOfWork.CategoriaRepository.Update(newCategoria);
            await unitOfWork.commitAsync();

            return Ok(updated);
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult<Categoria>> DeleteCategoria(int id)
        {
            var categoria = await unitOfWork.CategoriaRepository.GetAsync(categoria => categoria.CategoriaId == id);

            if (categoria is null)
            {
                return NotFound("produto não encontrado");
            }

            var deleted = unitOfWork.CategoriaRepository.Delete(categoria);
            await unitOfWork.commitAsync();


            return Ok(deleted);
        }


    }
}
