using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TixplorerCoreFrontend.Models;
using TixplorerCoreFrontend.Models.DTO;

namespace TixplorerCoreFrontend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CouponListsController : ControllerBase
    {
        private readonly TixplorerContext _context;

        public CouponListsController(TixplorerContext context)
        {
            _context = context;
        }

        // GET: api/CouponLists
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CouponList>>> GetCouponLists()
        {
          if (_context.CouponLists == null)
          {
              return NotFound();
          }
            return await _context.CouponLists.ToListAsync();
        }

        // GET: api/CouponLists/5
        [HttpGet("{id}")]
        public async Task<ActionResult<CouponDTO>> GetCouponList(string id)
        {
          if (_context.CouponLists == null)
          {
              return NotFound();
          }
            var couponList = await _context.CouponLists.Where(m=>m.MId ==id).ToListAsync();

            var coupon = _context.Coupons.Where(c=>couponList.Select(o=>o.CouponId).Contains(c.CouponId)).Select(c=>new Coupon
            {
                CouponId=c.CouponId,
                Name=c.Name,
                PId=c.PId,
                Discount=c.Discount,
                DiscountType=c.DiscountType,
                ExpirationDate=c.ExpirationDate,
                UsableDay=c.UsableDay,
            }).ToList();
            CouponDTO couponDTO = new CouponDTO();
            couponDTO.CouponsListDTO = couponList;
            couponDTO.CouponsDTO = coupon;
            return couponDTO;
        }

        // PUT: api/CouponLists/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCouponList(int id, CouponList couponList)
        {
            if (id != couponList.CouponlistId)
            {
                return BadRequest();
            }

            _context.Entry(couponList).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CouponListExists(id))
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

        // POST: api/CouponLists
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<CouponList>> PostCouponList(CouponList couponList)
        {
          if (_context.CouponLists == null)
          {
              return Problem("Entity set 'TixplorerContext.CouponLists'  is null.");
          }
            _context.CouponLists.Add(couponList);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (CouponListExists(couponList.CouponlistId))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetCouponList", new { id = couponList.CouponlistId }, couponList);
        }

        // DELETE: api/CouponLists/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCouponList(int id)
        {
            if (_context.CouponLists == null)
            {
                return NotFound();
            }
            var couponList = await _context.CouponLists.FindAsync(id);
            if (couponList == null)
            {
                return NotFound();
            }

            _context.CouponLists.Remove(couponList);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool CouponListExists(int id)
        {
            return (_context.CouponLists?.Any(e => e.CouponlistId == id)).GetValueOrDefault();
        }
    }
}
