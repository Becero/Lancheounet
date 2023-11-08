using Lancheounet.Models;
using Lancheounet.Repositories.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Lancheounet.Controllers
{
    public class PedidoController : Controller
    {
        private readonly IPedidoRepository _pedidoRepository;
        private readonly CarrinhoCompra _carrinhoCompra;

        public PedidoController(IPedidoRepository pedidoRepository, CarrinhoCompra carrinhoCompra)
        {
            _pedidoRepository = pedidoRepository;
            _carrinhoCompra = carrinhoCompra;
        }
        [Authorize]
        [HttpGet]
        public IActionResult Checkout() 
        {
            return View();
        }
        [Authorize]
        [HttpPost]
        public IActionResult Checkout(Pedido pedido)
        {
            int totalItensPedido = 0;
            decimal precoTotalPedido = 0.0m;

            //itens do carrinho
            List<CarrinhoCompraItem> items = _carrinhoCompra.GetCarrinhoCompraItens();
            _carrinhoCompra.CarrinhoCompraItems = items;

            // verifica se existem itens no carrinho
            if(_carrinhoCompra.CarrinhoCompraItems.Count == 0)
            {
                ModelState.AddModelError("", "Seu carrinho esta vazio,que tal incluir um lanche?");                
            }

            //CALCULA O TOTAL
            foreach(var item in items)
            {
                totalItensPedido += item.Quantidade;
                precoTotalPedido += (item.Lanche.Preco * item.Quantidade);
            }

            //atribui os valores obtidos
            pedido.TotalItensPedido = totalItensPedido;
            pedido.PedidoTotal = precoTotalPedido;

            //valida
            if(ModelState.IsValid)
            {
                //cria pedido e details
                _pedidoRepository.CriarPedido(pedido);
                //mensagem
                ViewBag.CheckoutCompletoMnesagem = "Obrogado pelo seu Pedido";
                ViewBag.TotalPedido = _carrinhoCompra.GetCarrinhoCompraTotal();

                //Limpa carrinho
                _carrinhoCompra.LimparCarrinho();

                //dados cliente
                return View("~/Views/Pedido/CheckoutCompleto.cshtml",pedido);
            }
            return View(pedido);
        }

    }
}
