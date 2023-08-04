using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.NetworkInformation;
using System.Security.Cryptography;
using System.Threading.Tasks;
using System.Xml.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic;
using Newtonsoft.Json;
using TixlorerCore.Models;
using TixplorerCoreFrontend.Models;
using TixplorerCoreFrontend.Models.DTO;

namespace TixplorerCoreFrontend.Controllers
{
    public class MembersController : Controller
    {
        private readonly TixplorerContext _context;

        public MembersController(TixplorerContext context)
        {
            _context = context;
        }

        // GET: Members
        public async Task<IActionResult> Index()
        {//會員中心
              return _context.Members != null ? 
                          View(await _context.Members.ToListAsync()) :
                          Problem("Entity set 'TixplorerContext.Members'  is null.");
        }
        // POST: Members/Login
        [HttpPost]
        public async Task<IActionResult> Login()
        {//會員登入
            return _context.Members != null ?
                        View(await _context.Members.ToListAsync()) :
                        Problem("Entity set 'TixplorerContext.Members'  is null.");
        }
        [HttpPost]
        public async Task<IActionResult> Register()
        {//會員註冊
            return _context.Members != null ?
                        View(await _context.Members.ToListAsync()) :
                        Problem("Entity set 'TixplorerContext.Members'  is null.");
        }

        [HttpGet]
        public async Task<IActionResult> Logout()
        {//會員登出
            return _context.Members != null ?
                        View(await _context.Members.ToListAsync()) :
                        Problem("Entity set 'TixplorerContext.Members'  is null.");
        }

        // GET: Members/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null || _context.Members == null)
            {
                return NotFound();
            }

            var member = await _context.Members
                .FirstOrDefaultAsync(m => m.MId == id);
            if (member == null)
            {
                return NotFound();
            }

            return View(member);
        }

        // GET: Members/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Members/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("MId,Email,Phone,Password,Name,Sex,IdNumber,Birthday,Address,Credit,Favorite,BonusPoint,RegisterDate,LastLoginDate")] Member member)
        {
            if (ModelState.IsValid)
            {
                _context.Add(member);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(member);
        }

        [HttpGet]
        public async Task<IActionResult> myFavorite()
        {
            //登入帳號用Email

            string sessionData = HttpContext.Session.GetString(CDictionary.TIXPLORER_USER_DATA); //從Session取資料
            //string account = GlobalVar.loginSession; //從GlobalVar取資料
            dynamic loginData = JsonConvert.DeserializeObject(sessionData);
            //dynamic loginData = JsonConvert.DeserializeObject(GlobalVar.loginSession);

            string accountValue = loginData.Account;
            var mData = _context.Members.FirstOrDefault(d => d.Email == accountValue);
            if (mData != null)//若有找到資料
            {
                var memberDTO = new memberEditVM
                {
                    MId = mData.MId,
                    Email = mData.Email,
                    Phone = mData.Phone,
                    Password = mData.Password,
                    Name = mData.Name,
                    Sex = mData.Sex,
                    Birthday = mData.Birthday,
                    Address = mData.Address,
                    BonusPoint = mData.BonusPoint,
                    Favorite = mData.Favorite,
                };

                //處理myFavortie的格式
                //將memberDTO.Favorite的內容作切割並存在arrFavorite;
                string[] arrFavorite = memberDTO.Favorite.Split(';', StringSplitOptions.RemoveEmptyEntries);


                //將我的最愛存在VIewData
                ViewData["arrFavorite"]=arrFavorite;

                //計算陣列長度
                ViewData["arrFavoriteCount"] = arrFavorite.Length;

                return View(memberDTO);
            }
            else
            {
                return NotFound();
            }
        }
        [HttpGet]
        public async Task<string> addToFavorite(string id)
        {
            try
            {
                string sessionData = HttpContext.Session.GetString(CDictionary.TIXPLORER_USER_DATA);
                dynamic loginData = JsonConvert.DeserializeObject(sessionData);

                Console.WriteLine(loginData);

                string accountValue = loginData.Account;
                var mData = _context.Members.FirstOrDefault(d => d.Email == accountValue);
                if (mData != null)
                {
                    mData.Favorite+= $"{id};";
                    _context.SaveChanges();
                    return "success";
                }
                return "nodata";
            }
            catch
            {
                return "false";
            }
        }

        [HttpGet]
        public async Task<string> removeFavorite(string id)
        {
            try
            {
                string sessionData = HttpContext.Session.GetString(CDictionary.TIXPLORER_USER_DATA);
                dynamic loginData = JsonConvert.DeserializeObject(sessionData);

                string accountValue = loginData.Account;
                var mData = _context.Members.FirstOrDefault(d => d.Email == accountValue);
                string strFavorite = mData.Favorite;
                if (mData != null)
                {
                    mData.Favorite=strFavorite.Replace($"{id};", "");
                    _context.SaveChanges();
                    return "success";
                }
                return "nodata";
            }
            catch
            {
                return "false";
            }
        }


        [HttpGet]
        public async Task<IActionResult> Edit() 
        {
            //登入帳號用Email

            string sessionData = HttpContext.Session.GetString(CDictionary.TIXPLORER_USER_DATA); //從Session取資料
            //string account = GlobalVar.loginSession; //從GlobalVar取資料
            dynamic loginData = JsonConvert.DeserializeObject(sessionData);
            //dynamic loginData = JsonConvert.DeserializeObject(GlobalVar.loginSession);

            string accountValue = loginData.Account;
            var mData = _context.Members.FirstOrDefault(d => d.Email == accountValue);

            if (mData != null)//若有找到資料
            {
                var memberDTO = new memberEditVM
                {
                    MId = mData.MId,
                    Email = mData.Email,
                    Phone = mData.Phone,
                    Password = mData.Password,
                    Name = mData.Name,
                    Sex = mData.Sex,
                    Birthday = mData.Birthday,
                    Address = mData.Address,
                    BonusPoint =mData.BonusPoint,
           
                };
                return View(memberDTO);
            }
            else
            {   
                return NotFound();
            }

        }

        [HttpPost]
        /*[ValidateAntiForgeryToken] */   ////////////Form版
        public async Task<IActionResult> Edit(memberEditVM memberDTO)
        {
            string mId = memberDTO.MId;
            string mName= memberDTO.Name;
            string mAddress= memberDTO.Address;
            int mSex=memberDTO.Sex;

            var datas = _context.Members.FirstOrDefault(d=> d.MId == mId);

            Debug.WriteLine(mSex);

            if (datas != null)
            {
                datas.Name = mName;
                datas.Address = mAddress;
                datas.Sex = mSex;
                _context.SaveChanges();
                //return RedirectToAction("Index", "Home");
                return RedirectToAction("Edit", "Members");
            }
            return RedirectToAction("Edit", "Members");
        }
    


        //[HttpPost]
        //public async Task<IActionResult> Edit([FromBody] memberEditVM memberDTO)  ////////////AJAX版
        //{
        //    string mId = memberDTO.MId;
        //    string mName = memberDTO.Name;
        //    string mAddress = memberDTO.Address;

        //    var datas = _context.Members.FirstOrDefault(d => d.MId == mId);

        //    if (datas != null)
        //    {
        //        datas.Name = mName;
        //        datas.Address = mAddress;
        //        try
        //        {
        //            await _context.SaveChangesAsync();
        //            return Json(new { success = true, message = "儲存成功" });
        //        }
        //        catch
        //        {
        //            // 處理異常，你也可以選擇將 ex 的訊息寫入日誌
        //            return Json(new { success = false, message = "未成功儲存" });
        //        }
        //    }
        //    return Json(new { success = true, message = "不存在此會員" });
        //}

        [HttpGet]
        public async Task<IActionResult> editPassword(string Mid)
        {
            //Mid = "M23060006";
            //查詢該會員的全部資料
            Member datas = _context.Members.FirstOrDefault(d => d.MId == Mid);
            return View(datas);
        }

        //導向畫面版
        [HttpPost]
        public async Task<IActionResult> editPassword(string Email, string newPwd)
        {

            //Mid = "M20010001";   //demo用
            //newPsd = "ThePasswordIsChanged";  //demo用

            var datas = _context.Members.FirstOrDefault(d => d.Email == Email);

            if (datas != null)
            {
                datas.Password = newPwd;

                _context.SaveChanges();

                ViewBag.Message = "修改成功";

                return RedirectToAction("Edit", "Members"); // 關閉本視窗
            }

            return View("Error");//若沒查詢到資料則返回Error
        }


        [HttpGet]
        public async Task<string> MemberEdit(string? packdata)  ////////////AJAX版
        {
            memberEditVM vm = JsonConvert.DeserializeObject<memberEditVM>(packdata);

            var datas = _context.Members.FirstOrDefault(d => d.MId == vm.MId);

            if (datas != null)
            {
                datas.Name = vm.Name;
                datas.Address = vm.Address;
                try
                {
                    await _context.SaveChangesAsync();
                    return "儲存成功";
                }
                catch
                {
                    // 處理異常，你也可以選擇將 ex 的訊息寫入日誌
                    return "未成功儲存";
                }
            }
            return "不存在此會員";
        }

        public async Task<IActionResult> changedSuccess()
        {
            return View();
        }

        // GET: Members/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null || _context.Members == null)
            {
                return NotFound();
            }

            var member = await _context.Members
                .FirstOrDefaultAsync(m => m.MId == id);
            if (member == null)
            {
                return NotFound();
            }

            return View(member);
        }

        // POST: Members/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            if (_context.Members == null)
            {
                return Problem("Entity set 'TixplorerContext.Members'  is null.");
            }
            var member = await _context.Members.FindAsync(id);
            if (member != null)
            {
                _context.Members.Remove(member);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool MemberExists(string id)
        {
          return (_context.Members?.Any(e => e.MId == id)).GetValueOrDefault();
        }
    }
}
