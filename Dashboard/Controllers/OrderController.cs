using Application.Services;
using AutoMapper;
using Dashboard.Hubs;
using Domain;
using DTO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;

namespace Dashboard.Controllers
{

    [Authorize]
    public class OrderController : Controller
    {
        private readonly IOrderService _orderService;
        private readonly IUserService _userService;
        private readonly IOrderItemService _orderItemService;
        private readonly IHubContext<OrderStatusHub> _hubContext;
        public OrderController(IHubContext<OrderStatusHub> hubContext,IOrderService orderService,IUserService userService, IOrderItemService orderItemService)
        {
            _orderService = orderService;
            _userService = userService;
            _orderItemService = orderItemService;
            _hubContext= hubContext;
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
                    //await _hubContext.Clients.All.SendAsync("updateYourOrder", orderdto.ArrivalDate);
                    await _hubContext.Clients.Group("order" + id).SendAsync("updateYourOrder", orderdto.status);
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
