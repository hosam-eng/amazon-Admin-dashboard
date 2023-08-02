using AmazonAdmin.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
namespace AmazonAdmin.Application.Services
{
    public interface IOrderService
    {
        Task<List<OrderDTO>> getAllByUserId(string id);
        Task<OrderDTO> GetByIdAsync(int id);
        Task<OrderDTO> Create(OrderDTO orderDTO);
        Task<bool> Update(OrderDTO orderDTO);
        Task<bool> Delete(int id);
        Task<List<OrderDTO>> GetAllOrders();
    }
}
