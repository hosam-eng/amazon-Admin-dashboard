using AmazonAdmin.Application.Services;
using AmazonAdmin.Domain;
using AmazonAdmin.DTO;
using Microsoft.AspNetCore.Mvc;

namespace AmazonAdminDashboardMVC.Controllers
{
    public class OrderController : Controller
    {
        private readonly IOrderService _orderService;
        private readonly IUserService _userService;
        private readonly IOrderItemService _orderItemService;
        public OrderController(IOrderService orderService,IUserService userService, IOrderItemService orderItemService)
        {
            _orderService = orderService;
            _userService = userService;
            _orderItemService = orderItemService;
        }

        public async Task<IActionResult> Index()
        {
            List<OrderDTO> orders = await _orderService.GetAllOrders();
            if(orders != null)
            {
				foreach (var item in orders)
				{
					item.UserName = await _userService.UserName(item.UserId);
				}
			}
            return View(orders);
        }

        public async Task<IActionResult> GetOrderItems(int id)
        {
            var items=await _orderItemService.getOrderItemsByOrderId(id);
            return View(items);
        }

        public async Task<IActionResult> UpadatStatus(int id)
        {
            OrderDTO order = await _orderService.GetByIdAsync(id);
            if (order != null)
            {
                    order.UserName = await _userService.UserName(order.UserId);
            }
            return View(order);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> UpadatStatus(int id,Status status)
        {
            OrderDTO orderdto = new OrderDTO();
            if (ModelState.IsValid)
            {
                orderdto=await _orderService.GetByIdAsync(id);
                orderdto.status = status;
                var res = await _orderService.Update(orderdto);
                if (res)
                {
                    return RedirectToAction("Index");
                }
                else
                {
                    ModelState.AddModelError("", "Failed To Update Status!");
                }
            }
            if (orderdto != null)
            {
                orderdto.UserName = await _userService.UserName(orderdto.UserId);
            }
            return View(orderdto);
        }
    }
}
