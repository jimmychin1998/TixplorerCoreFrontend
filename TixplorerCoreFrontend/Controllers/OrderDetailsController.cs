using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NuGet.Protocol;
using TixplorerCoreFrontend.Models;
using TixplorerCoreFrontend.Models.DTO;

namespace TixplorerCoreFrontend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderDetailsController : ControllerBase
    {
        private readonly TixplorerContext _context;

        public OrderDetailsController(TixplorerContext context)
        {
            _context = context;
        }

        // GET: api/OrderDetails
        [HttpGet]
        public async Task<ActionResult<IEnumerable<OrderDetail>>> GetOrderDetails()
        {
          if (_context.OrderDetails == null)
          {
              return NotFound();
          }
            return await _context.OrderDetails.ToListAsync();
        }

        // GET: api/OrderDetails/{orderid}
        [HttpGet]
        [Route("{orderid}")]
        public async Task<ActionResult<orderdetailDTO>> GetOrderDetail(string orderid)
        {
          if (_context.OrderDetails == null)
          {
              return NotFound();
          }
            var orderDetail = _context.OrderDetails.Where(p => p.OrderId == orderid);

            if (orderDetail == null)
            {
                return NotFound();
            }
            var orderproduct = _context.Products
           .Where(p => orderDetail.Select(o => o.PId).Contains(p.PId))
            .Select(p => new ordersproducts
            {
            PId = p.PId,
            Price = p.Price,
            Name = p.Name,
            DiscountPrice = p.DiscountPrice,
            Image = p.Image,
            TicketId = p.TicketId,
             })
            .ToList();
            var orderticket = _context.Tickets
            .Where(t => orderproduct.Select(o => o.TicketId).Contains(t.TicketId))
            .Select(t => new ordersticketDTO
             {
             TicketId = t.TicketId,
             Name = t.Name,
             Type = t.Type,
             Capacity = t.Capacity,
             Price = t.Price,
             DiscountPrice = t.DiscountPrice,
             Deadline = t.Deadline
             })
             .ToList();
            orderdetailDTO orderdetailDTO = new orderdetailDTO();
            orderdetailDTO.orderdetailproducts = orderproduct;
            orderdetailDTO.orderdetailticket = orderticket;
            return orderdetailDTO;
        }

        // PUT: api/OrderDetails/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutOrderDetail(int id, OrderDetail orderDetail)
        {
            if (id != orderDetail.OdId)
            {
                return BadRequest();
            }

            _context.Entry(orderDetail).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!OrderDetailExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/OrderDetails
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<OrderDetail>> PostOrderDetail(OrderDetail orderDetail)
        {
          if (_context.OrderDetails == null)
          {
              return Problem("Entity set 'TixplorerContext.OrderDetails'  is null.");
          }
            _context.OrderDetails.Add(orderDetail);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (OrderDetailExists(orderDetail.OdId))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetOrderDetail", new { id = orderDetail.OdId }, orderDetail);
        }

        // DELETE: api/OrderDetails/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteOrderDetail(string? id)
        {
            if (_context.OrderDetails == null)
            {
                return NotFound();
            }
            var OrderDetail = await _context.OrderDetails.Where(o => o.OrderId == id).ToListAsync();
            if (!OrderDetail.Any())
            {
                return NotFound();
            }

            _context.OrderDetails.RemoveRange(OrderDetail);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool OrderDetailExists(int id)
        {
            return (_context.OrderDetails?.Any(e => e.OdId == id)).GetValueOrDefault();
        }
    }
}
