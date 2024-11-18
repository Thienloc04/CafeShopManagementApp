using DAL.Models;
using DAL.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Services
{
    public class OrderService
    {
        private OrderRepo _repo = new();

        public void SaveOrder(int userId, Dictionary<int, CartItem> cart, decimal totalPrice)
        {
            List<OrderDetail> orderDetails = new List<OrderDetail>();

            Order order = new Order();
            order.UserId = userId;
            order.OrderDate = DateTime.Now;
            order.TotalPrice = totalPrice;

            foreach (var item in cart.Values)
            {
                OrderDetail detail = new OrderDetail();
                detail.ProductId = item.ProductId;
                detail.Quantity = item.Quantity;
                detail.UnitPrice = item.UnitPrice;
                orderDetails.Add(detail);
            }

            _repo.SaveOrder(order, orderDetails);
        }

        public List<OrderDetail> GetOrderDetails()
        {
            return _repo.GetAll();
        }

        public List<OrderDetail> GetOrdersByUserId(int userId)
        {
            return _repo.GetAll().Where(x => x.Order.UserId == userId).ToList(); 
        } 
    }
}
