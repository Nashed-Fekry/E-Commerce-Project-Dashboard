using Application.Contracts;
using Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Contracts
{
    public interface IOrderItemReposatory:IReposatory<OrderItem,int>
    {
        Task<List<OrderItem>> getOrderItemsByOrderId(int id);

        Task<bool> deleteOrderItemsByOrderId(int id);   

    }
}
