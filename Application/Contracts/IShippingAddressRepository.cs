using Application.Contracts;
using Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Contracts
{
    public interface IShippingAddressRepository
         : IReposatory<shippingAddress, int>
    {
    }
}
