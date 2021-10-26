using DAO;
using ItoSoftwarePrueba.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Transactions;

namespace ItoSoftwarePrueba.Controllers
{
    public class OrderItemController : Controller
    {
        public static List<ProductViewModel> products;
        public static int _clienteId;

        private ProductRepository productRepository;
        private CustomerRepository customerRepository;
        private OrderItemRepository orderItemRepository;
        private OrderRepository orderRepository;
        private readonly IConfiguration _configuration;
        public OrderItemController(IConfiguration configuration)
        {
            _configuration = configuration;
            productRepository = new ProductRepository(_configuration.GetConnectionString("ConexionModel"));
            customerRepository = new CustomerRepository(_configuration.GetConnectionString("ConexionModel"));
            orderRepository = new OrderRepository(_configuration.GetConnectionString("ConexionModel"));
            orderItemRepository = new OrderItemRepository(_configuration.GetConnectionString("ConexionModel"));
        }

        public IActionResult Index()
        {
            products = new List<ProductViewModel>();

            var productos = productRepository.GetProducts();
            ViewBag.Productos = new SelectList(productos, "ProductId", "ProductName");

            var clientes = customerRepository.GetCustomers();
            ViewBag.Clientes = new SelectList(clientes, "CustomerId", "CustomerName");

            return View(products);
        }

        [HttpPost]
        public JsonResult GetProduct(int productId)
        {
            var producto = productRepository.GetProductByID(productId);
            return Json(producto);
        }

        [HttpPost]
        public JsonResult GetCliente(int clienteId)
        {
            var cliente = customerRepository.GetCustomerByID(clienteId);
            return Json(cliente);
        }

        [HttpPost]
        public IActionResult AgregarProducto([FromBody] ProductPostViewModel product)
        {
            var productos = productRepository.GetProducts();
            ViewBag.Productos = new SelectList(productos, "ProductId", "ProductName");

            var clientes = customerRepository.GetCustomers();
            ViewBag.Clientes = new SelectList(clientes, "CustomerId", "CustomerName");

            var producto = productRepository.GetProductByID(product.ProductId);
            
            products.Add(new ProductViewModel(){ 
                ProductId = producto.ProductId, 
                ProductName = producto.ProductName, 
                IsDiscontinued = producto.IsDiscontinued,
                SupplierId = producto.SupplierId,
                UnitPrice = producto.UnitPrice,
                Cantidad = product.Cantidad,
                Total = producto.UnitPrice * product.Cantidad
            });

            return View("Index", products);
        }

        public IActionResult Checkout(int clienteId)
        {
            _clienteId = clienteId;
            var cliente = customerRepository.GetCustomerByID(_clienteId);

            ViewBag.Cliente = cliente;

            return View(products);
        }

        public IActionResult GuardarOrden()
        {
            try
            {
                using (TransactionScope ts = new TransactionScope())
                {
                    var cliente = customerRepository.GetCustomerByID(_clienteId);
                    var total = products.Select(p => p.Total).Sum();
                    int totalOrders = orderRepository.GetTotalOrder();

                    var order = new AccessData.Order()
                    {
                        CustomerId = _clienteId,
                        Customer = cliente,
                        OrderDate = DateTime.Now,
                        TotalAmount = total,
                        OrderNumber = (totalOrders + 1).ToString().PadLeft(10, '0')
                    };

                    orderRepository.InsertOrder(order);
                    orderRepository.Save();

                    foreach(var pro in products)
                    {
                        var orderItem = new AccessData.OrderItem()
                        {
                            OrderId = order.OrderId,
                            ProductId = pro.ProductId,
                            Quantity = Convert.ToInt32(pro.Cantidad),
                            UnitPrice = pro.UnitPrice
                        };

                        orderItemRepository.InsertOrderItem(orderItem);
                        orderItemRepository.Save();
                    }

                    ts.Complete();
                }
            }
            catch (Exception ex)
            { 
                
            }

            return View("Index");
        }
    }
}
