using Chapter.WebApi.Contexts;
using Chapter.WebApi.Interfaces;
using Chapter.WebApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Chapter.WebApi.Repositories
{
    public class UsuarioRepository :IUsuarioRepository
    {
        private readonly ChapterContext _context;

        public UsuarioRepository(ChapterContext context)
        {
            _context = context;
        }

        public List<Usuario> Listar()
        {
            return _context.Usuarios.ToList();
        }


        public void Cadastrar(Usuario u)
        {
            _context.Usuarios.Add(u);
            _context.SaveChanges();
        }

        public Usuario BuscarPorId(int id)
        {
            return _context.Usuarios.Find(id);
        }

        public void Atualizar(int id, Usuario u)
        {
            Usuario UsuarioEncontrado = _context.Usuarios.Find(id);

            if (UsuarioEncontrado != null )
            {
                UsuarioEncontrado.Email = u.Email;
                UsuarioEncontrado.Senha = u.Senha;
                UsuarioEncontrado.Tipo = u.Tipo;
            }

            _context.Usuarios.Update(UsuarioEncontrado);
            _context.SaveChanges();
        }

        public void Deletar(int id)
        {
            Usuario UsuarioEncontrado = _context.Usuarios.Find(id);

            _context.Usuarios.Remove(UsuarioEncontrado);
            _context.SaveChanges();
        }

        
        public Usuario Login(string email, string senha)
        {
            return _context.Usuarios.FirstOrDefault(u => u.Email == email && u.Senha == senha);
        }

    }      
}
