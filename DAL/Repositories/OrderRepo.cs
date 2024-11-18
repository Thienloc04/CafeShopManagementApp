using DAL.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repositories
{
    public class OrderRepo
    {
        private CafeManagementSystemContext _context;

        public void SaveOrder(Order order, List<OrderDetail> details)
        {
            _context = new();
            _context.Orders.Add(order);
            _context.SaveChanges();

            foreach (var item in details)
            {
                item.OrderId = order.OrderId;
                _context.OrderDetails.Add(item);
                _context.SaveChanges();
            }
        }

        public List<OrderDetail> GetAll()
        {
            _context = new();
            return _context.OrderDetails.Include("Order").Include("Product").ToList();
        }

        public decimal GetTotalIncome()
        {
            _context = new();
            return _context.Orders.Sum(x => x.TotalPrice);
        }

        public decimal GetDayIncome()
        {
            _context = new();
            DateTime today = DateTime.Now;
            return _context.Orders.Where(x => x.OrderDate == today).Sum(x => x.TotalPrice);
        }
    }
}
