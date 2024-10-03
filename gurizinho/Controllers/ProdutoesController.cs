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
        public async Task<ActionResult<IEnumerable<Produto>>> GetProdutos() {
            var list = await unitOfWork.ProdutoRepository.GetAllAsync();

            if (list is null) { 
                return NotFound("produtos não encontrados");
            }

            return  Ok(list);
        }

        [HttpGet("pagination")]
        public async Task<ActionResult<IEnumerable<Produto>>> GetAllPages([FromQuery] PageProdutoParameters prm) {

            var produtos = await unitOfWork.ProdutoRepository.GetProdutosPageAsync(prm);


            var metadata = new
            {
                produtos.Count,
                produtos.PageSize,
                produtos.TotalItemCount,
                produtos.PageCount,
                produtos.HasNextPage,
                produtos.HasPreviousPage
            };


            Response.Headers.Append("X-Pagination", JsonConvert.SerializeObject(metadata));

            
            return Ok(produtos);

        }

        [HttpGet("filter/preco")]
        public async Task<ActionResult<IEnumerable<Produto>>> GetByPrice([FromQuery] FiltoDeProdutoPorPreco prm) 
        {
            if (prm is null) 
            {
                return BadRequest("Não pode ser null");
            }
            var produtos = await unitOfWork.ProdutoRepository.GetProdutosFiltroPrecoAsync(prm);


            var metadata = new
            {
                produtos.Count,
                produtos.PageSize,
                produtos.TotalItemCount,
                produtos.PageCount,
                produtos.HasNextPage,
                produtos.HasPreviousPage
            };

            Response.Headers.Append("X-Pagination", JsonConvert.SerializeObject(metadata));


            return Ok(produtos);

        }


        [HttpGet("{id:int}", Name = "ObterProduto")]
        public async Task<ActionResult<Produto>> GetProduto(int id) {
            var produto = await unitOfWork.ProdutoRepository.GetAsync(prod => prod.ProdutoId == id);

            if (produto is null)
            {
                return NotFound("produto não encontrado");
            }

            return produto;
        }

        [HttpPost]
        public async Task<ActionResult> CreateProduto([FromBody] ProdutoCreateDTO newProduto) 
        {
            if (newProduto is null)
                return BadRequest();

            var produto = objectMapper.Map<Produto>(newProduto);
            produto.DataCadastro = DateTime.Now;
            
            produto = unitOfWork.ProdutoRepository.Create(produto);
            await unitOfWork.commitAsync();

            return new CreatedAtRouteResult("ObterProduto", new { id = produto.ProdutoId}, produto);
        }


        [HttpPut("{id:int}")]
        public async Task<ActionResult> UpdateProduto(int id, Produto newProduto)
        {

            if(id != newProduto.ProdutoId)
                return BadRequest();

            unitOfWork.ProdutoRepository.Update(newProduto);
            await unitOfWork.commitAsync();


            return Ok(newProduto);
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult<Produto>> DeleteProduto(int id)
        {
            var produto = await unitOfWork.ProdutoRepository.GetAsync(prod => prod.ProdutoId == id);

            if (produto is null)
            {
                return NotFound("produto não encontrado");
            }

            produto = unitOfWork.ProdutoRepository.Delete(produto);
            await unitOfWork.commitAsync();

            return Ok(produto);
        }


    }
}
