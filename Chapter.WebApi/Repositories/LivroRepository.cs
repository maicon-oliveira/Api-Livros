using Chapter.WebApi.Contexts;
using Chapter.WebApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Chapter.WebApi.Repositories
{
    public class LivroRepository
    {
        // possui acesso aos dados
        private readonly ChapterContext _context;
        // somente um data context na memória da aplicação na requisição, evitar o usar o new
        // para o repositório existir, precisa do contexto, a aplicacao cria
        // configurar no startup
        public LivroRepository(ChapterContext context)
        {
            //conexão do banco de dados fica armazenado na variavel _context
            _context = context;
        }
        // retorna a lista de livros
        public List<Livro> Listar()
        {
            // SELECT Id, Titulo, QuantidadePaginas, Disponivel FROM Livros;
            return _context.Livros.ToList();
        }

        //metodo busca livros por id
        public Livro BuscarPorId(int id)
        {
            return _context.Livros.Find(id);
        }

        //metodo cadastrar novo livro
        public void Cadastrar(Livro livro)
        {
            _context.Livros.Add(livro);

            _context.SaveChanges();
        }

        //metodo atualizar dados
        public void Atualizar(int id, Livro livro)
        {
            Livro livroBuscado = _context.Livros.Find(id);

            if (livroBuscado != null)
            {
                livroBuscado.Titulo = livro.Titulo;
                livroBuscado.QuantidadePaginas = livro.QuantidadePaginas;
                livroBuscado.Disponivel = livro.Disponivel;
            }

            _context.Livros.Update(livroBuscado);

            _context.SaveChanges();
        }

        //metodo deletar dados
        public void Deletar(int id)
        {
            Livro livroBuscado = _context.Livros.Find(id);

            _context.Livros.Remove(livroBuscado);

            _context.SaveChanges();
        }
    }
}
