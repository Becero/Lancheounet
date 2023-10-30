using Lancheounet.Models;
using Lancheounet.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace Lancheounet.Components
{
    public class CarrinhoCompraResumo : ViewComponent
    {
        private readonly CarrinhoCompra _carrinhoCompra;

        public CarrinhoCompraResumo(CarrinhoCompra carrinhoCompra)
        {
            _carrinhoCompra = carrinhoCompra;
        }

        public IViewComponentResult Invoke()
        {
            var itens = _carrinhoCompra.GetCarrinhoCompraItens();
            //var itens = new List<CarrinhoCompraItem>() { 
            //    new CarrinhoCompraItem(),
            //    new CarrinhoCompraItem()
            //};Teste para validar o carrinho

            _carrinhoCompra.CarrinhoCompraItems = itens;
            var carrinhoCompraVM = new CarrinhoCompraViewModel
            {
                CarrinhoCompra = _carrinhoCompra,
                CarrinhoCompraTotal = _carrinhoCompra.GetCarrinhoCompraTotal()
            };
            return View(carrinhoCompraVM);
        }
    }
}
