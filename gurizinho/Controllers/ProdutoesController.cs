using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using gurizinho.Context;
using gurizinho.models;
using gurizinho.Repository;
using Microsoft.EntityFrameworkCore.Query.Internal;
using gurizinho.Repository.UFW;
using AutoMapper;
using gurizinho.DTOs;
using gurizinho.Pagination;
using Newtonsoft.Json;

namespace gurizinho.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class ProdutoesController : ControllerBase
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper objectMapper;

        public ProdutoesController(IUnitOfWork unitOfWork, IMapper objectMapper)
        {
            this.unitOfWork = unitOfWork;
            this.objectMapper = objectMapper;
        }

        [HttpGet]
        public ActionResult<IEnumerable<Produto>> GetProdutos() {
            var list = unitOfWork.ProdutoRepository.GetAll();

            if (list is null) { 
                return NotFound("produtos não encontrados");
            }

            return  Ok(list);
        }

        [HttpGet("pagination")]
        public ActionResult<IEnumerable<Produto>> GetAllPages([FromQuery] PageProdutoParameters prm) {

            var produtos = unitOfWork.ProdutoRepository.GetProdutosPage(prm);


            var metadata = new
            {
                produtos.TotalCount,
                produtos.PageSize,
                produtos.CurrentPage,
                produtos.TotalPages,
                produtos.HasNext,
                produtos.HasPrevious
            };

            Response.Headers.Append("X-Pagination", JsonConvert.SerializeObject(metadata));

            
            return Ok(produtos);

        }

        [HttpGet("filter/preco")]
        public ActionResult<IEnumerable<Produto>> GetByPrice([FromQuery] FiltoDeProdutoPorPreco prm) 
        {
            if (prm is null) 
            {
                return BadRequest("Não pode ser null");
            }
            var produtos = unitOfWork.ProdutoRepository.GetProdutosFiltroPreco(prm);


            var metadata = new
            {
                produtos.TotalCount,
                produtos.PageSize,
                produtos.CurrentPage,
                produtos.TotalPages,
                produtos.HasNext,
                produtos.HasPrevious
            };

            Response.Headers.Append("X-Pagination", JsonConvert.SerializeObject(metadata));


            return Ok(produtos);

        }


        [HttpGet("{id:int}", Name = "ObterProduto")]
        public ActionResult<Produto> GetProduto(int id) {
            var produto = unitOfWork.ProdutoRepository.Get(prod => prod.ProdutoId == id);

            if (produto is null)
            {
                return NotFound("produto não encontrado");
            }

            return produto;
        }

        [HttpPost]
        public ActionResult CreateProduto([FromBody] ProdutoCreateDTO newProduto) 
        {
            if (newProduto is null)
                return BadRequest();

            var produto = objectMapper.Map<Produto>(newProduto);
            produto.DataCadastro = DateTime.Now;
            
            produto = unitOfWork.ProdutoRepository.Create(produto);
            unitOfWork.commit();

            return new CreatedAtRouteResult("ObterProduto", new { id = produto.ProdutoId}, produto);
        }


        [HttpPut("{id:int}")]
        public ActionResult UpdateProduto(int id, Produto newProduto)
        {

            if(id != newProduto.ProdutoId)
                return BadRequest();

            unitOfWork.ProdutoRepository.Update(newProduto);
            unitOfWork.commit();


            return Ok(newProduto);
        }

        [HttpDelete("{id:int}")]
        public ActionResult<Produto> DeleteProduto(int id)
        {
            var produto = unitOfWork.ProdutoRepository.Get(prod => prod.ProdutoId == id);

            if (produto is null)
            {
                return NotFound("produto não encontrado");
            }

            produto = unitOfWork.ProdutoRepository.Delete(produto);
            unitOfWork.commit();

            return Ok(produto);
        }


    }
}
