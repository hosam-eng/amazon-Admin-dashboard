using AmazonAdmin.Application.Contracts;
using AmazonAdmin.DTO;
using AmazonAdmin.Domain;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AmazonAdmin.Application.Services
{
    public class OrderItemServices : IOrderItemService
    {
        private readonly IOrderItemReposatory _repository;
        private readonly IMapper _Mapper;

        public OrderItemServices(IOrderItemReposatory repository,IMapper mapper)
        {
            _Mapper = mapper;
            _repository = repository;
        }

        public async Task<List<OrderItemShow>> getOrderItemsByOrderId(int id)
        {
            var res =await _repository.getOrderItemsByOrderId(id);
            
            return _Mapper.Map<List<OrderItemShow>>(res);
        }

        public async Task<bool> Create(OrderItemShow orderItemDto)
        {
            OrderItem orderItemModel = _Mapper.Map<OrderItem>(orderItemDto);
            var res = await _repository.CreateAsync(orderItemModel);
            if (res!=null)
            {
                await _repository.SaveChangesAsync();
                return true;
            }
            else
            {
                return false;
            }
        }

        public async Task<bool> Delete(int id)
        {
            var res = await _repository.DeleteAsync(id);
            if (res)
            {
                await _repository.SaveChangesAsync();
                return true;
            }else { return false; }
        }

        public async Task<bool> Update(int id, OrderItemShow orderItemDto)
        {
            OrderItem orderItem = _Mapper.Map<OrderItem>(orderItemDto);
            var res=await _repository.UpdateAsync(orderItem);
            if (res)
            {
                await _repository.SaveChangesAsync();
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
