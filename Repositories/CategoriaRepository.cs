using Lancheounet.Context;
using Lancheounet.Models;
using Lancheounet.Repositories.Interfaces;

namespace Lancheounet.Repositories
{
    public class CategoriaRepository : ICategoriaRepository
    { 

        private readonly AppDbContext _context;

        public CategoriaRepository(AppDbContext context)
        {
            _context = context;
        }

        public IEnumerable<Categoria> Categorias => _context.Categorias;
    }
}
