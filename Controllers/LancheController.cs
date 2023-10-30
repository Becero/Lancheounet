using Lancheounet.Models;
using Lancheounet.Repositories.Interfaces;
using Lancheounet.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace Lancheounet.Controllers
{
    public class LancheController : Controller
    {
        private readonly ILancheRepository  _lancheRepository;

        public LancheController(ILancheRepository lancheRepository)
        {
            _lancheRepository = lancheRepository;
        }

        public IActionResult List(string categoria)
        {
            //var lanches = _lancheRepository.Lanches;          
            //return View(lanches);
            //var lanchesListViewModel = new LancheListViewModel();
            //lanchesListViewModel.Lanches = _lancheRepository.Lanches;
            //lanchesListViewModel.CategoriaAtual = "Categoria Atual";

            IEnumerable<Lanche> lanches;
            string categoriaAtual = string.Empty;
            if (string.IsNullOrEmpty(categoria))
            {
                lanches = _lancheRepository.Lanches.OrderBy(l => l.LancheId);
                categoriaAtual = "Todos os lanches";
            }
            else
            {
                if (string.Equals("Normal", categoria, StringComparison.OrdinalIgnoreCase))//ignora caixa alta ou baixa
                {
                    lanches = _lancheRepository.Lanches.Where(l => l.Categoria.CategoriaNome.Equals("Normal"))
                        .OrderBy(l => l.Nome);
                }
                else
                {

                    lanches = _lancheRepository.Lanches.Where(l => l.Categoria.CategoriaNome.Equals("Natural"))
                        .OrderBy(l => l.Nome);
                }
                categoriaAtual = categoria;
            }
            var lanchesListViewModel = new LancheListViewModel 
            { 
                Lanches = lanches,
                CategoriaAtual=categoriaAtual,
            };
            return View(lanchesListViewModel);
        }
    }
}
