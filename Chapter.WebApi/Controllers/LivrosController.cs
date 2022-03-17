using Chapter.WebApi.Models;
using Chapter.WebApi.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Chapter.WebApi.Controllers
{

    // Controller responsável pelos endpoints (URLs) referentes aos livros
    // Define que o tipo de resposta da API será no formato JSON
    [Produces("aplication/json")]

    // Define que a rota de uma requisição será no formato dominio/api/nomeController
    //exemplo:http://localhost:5000/api/livros 
    [Route("api/[controller]")]

    // atributo para habilitar comportamentos especificos de API, como status, retorno
    [ApiController]

    [Authorize]

    // [ControllerBase] - requisicoes HTTP
    public class LivrosController : ControllerBase
    {
        
        private readonly LivroRepository _livroRepository;
        
        public LivrosController(LivroRepository livroRepository)
        {
            _livroRepository = livroRepository;
        }
        // GET /api/livros
        [HttpGet]
        public IActionResult Listar()
        {
            try
            {
                // retorna no corpo da resposta, a lista de livros
                // retorna o status Ok - 200, sucesso
                return Ok(_livroRepository.Listar());
            }
            //caso erro, guarda a mensagem de erro na variável "e"
            catch (Exception e)
            {
                //exibe a mensagem da variável
                throw new Exception(e.Message);
            }

        }

        // GET /api/livros/1->numero do id livro
        [HttpGet("{id}")]
        public IActionResult BuscarPorId(int id)
        {
            try
            {
                Livro LivroProcurado = _livroRepository.BuscarPorId(id);

                if (LivroProcurado == null)
                {
                    return NotFound();
                }

                return Ok(LivroProcurado);

            }
            //caso erro, guarda a mensagem de erro na variável "e"
            catch (Exception e)
            {
                //exibe a mensagem da variável
                throw new Exception(e.Message);
            }

        }

        [HttpPost]
        public IActionResult Cadastrar(Livro livro)
        {
            try
            {
                _livroRepository.Cadastrar(livro);

                return StatusCode(201);
            }
            //caso erro, guarda a mensagem de erro na variável "e"
            catch (Exception e)
            {
                //exibe a mensagem da variável
                throw new Exception(e.Message);
            }

        }
    
        [HttpPut("{id}")]
        public IActionResult Atualizar(int id, Livro livro)
        {
            try
            {
                _livroRepository.Atualizar(id, livro);

                return StatusCode(204);
            }
            //caso erro, guarda a mensagem de erro na variável "e"
            catch (Exception e)
            {
                //exibe a mensagem da variável
                throw new Exception(e.Message);
            }
        }

        [HttpDelete("{id}")]
        public IActionResult Deletar(int id)
        {
            try
            {
                _livroRepository.Deletar(id);

                return StatusCode(204);
            }
            //caso erro, guarda a mensagem de erro na variável "e"
            catch (Exception e)
            {
                //exibe a mensagem da variável
                throw new Exception(e.Message);
            }
        }
    
    }


}
