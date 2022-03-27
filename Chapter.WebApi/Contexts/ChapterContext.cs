using Chapter.WebApi.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Chapter.WebApi.Contexts
{
    // dbcontext é a ponte entre o modelo de classe e o banco de dados
    public class ChapterContext : DbContext
    {
        public ChapterContext()
        {
        }
        public ChapterContext(DbContextOptions<ChapterContext>options) : base(options)
        {
        }
        // vamos utilizar esse método para configurar o banco de dados
        protected override void
        OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                // cada provedor tem sua sintaxe par especificação
                optionsBuilder.UseSqlServer("Data Source=MAICONOLIVEIRA;initial catalog=Chapter;Integrated Security=true");
            }                               //Data Source=MAICONOLIVEIRA;Initial Catalog=Chapter;Integrated Security=True
        } // fim da configuração do banco de dados

        // dbset representa as entidades que serão utilizadas nas operações de leitura, criação, atualização e deleção
        //aqui dizemos que a classe livro representa a tabela livro do banco de dados
        public DbSet<Livro> Livros { get; set; }

        public DbSet<Usuario> Usuarios { get; set; }

    }

}


