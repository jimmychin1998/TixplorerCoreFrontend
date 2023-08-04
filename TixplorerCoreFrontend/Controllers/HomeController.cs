using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using TixplorerCoreFrontend.Models;
using System.Text.Json;
using TixplorerCoreFrontend.ViewModels;
using TixlorerCore.Models;
using Microsoft.AspNetCore.Http;
using TixplorerFore.Controllers;

namespace TixplorerCoreFrontend.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public async Task<IActionResult> Index(string? logindatas, string? CartDatas)
        {
            if (logindatas != null & logindatas != "")
                setSession(logindatas);

            if (CartDatas != null & CartDatas != "")
                return (new CartController()).AddToCart(CartDatas);
            else
                return View();
        }
        public IActionResult Logout()
        {//登出事件
            //移除Server登入資訊Session
            HttpContext.Session.Remove("TIXPLORER_USER_DATA");
            //清空紀錄登入資訊用變數
            GlobalVar.loginSession = string.Empty;

            //重新返回首頁
            return RedirectToAction("Index");
        }

        [HttpPost]
        public void setSession(string logindatas)
        {//接收SPA登入畫面傳來的登入資料並於後端Server存成Session
            if (logindatas != null)
            {//如果有傳入登入訊息，則於Server端紀錄Session
                HttpContext.Session.SetString(CDictionary.TIXPLORER_USER_DATA, logindatas);
                //紀錄登入資訊用變數 (捨棄)
                //GlobalVar.loginSession = logindatas.ToString();
                //GlobalVar.loginSession = HttpContext.Session.GetString(CDictionary.TIXPLORER_USER_DATA);

                //驗證Seesion是否存進資料使用
                System.Diagnostics.Debug.WriteLine(HttpContext.Session.GetString(CDictionary.TIXPLORER_USER_DATA));

                //用JSON格式解構傳入的登入資料
                //JsonSerializer.Deserialize<List<string>>(logindatas);
            }
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}