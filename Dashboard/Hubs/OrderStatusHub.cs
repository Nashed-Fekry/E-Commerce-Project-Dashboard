using Microsoft.AspNetCore.SignalR;
using System.Diagnostics;
using Hub = Microsoft.AspNetCore.SignalR.Hub;
namespace Dashboard.Hubs
{
    public class OrderStatusHub:Hub
    {
        public async Task NotifyUpdateOrder(string orderId,string newOrderStatus)
        {            
            await Clients.OthersInGroup("order" + orderId).SendAsync("updateYourOrder", newOrderStatus);
        }
        public async Task SubscribeToOrderUpdates(string orderId)
        {
            Debug.WriteLine(orderId);
            await Groups.AddToGroupAsync(Context.ConnectionId, "order"+orderId);
        }

        public async Task UnsubscribeFromOrderUpdates(string orderId)
        {
            await Groups.RemoveFromGroupAsync(Context.ConnectionId, "order" + orderId);
        }

    }
}
