using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using FrontendWebApi.Models;
using Microsoft.AspNetCore.Routing.Internal;
using System.Net.Mail;
using System.Net;
using System.Text;
using System.Text.Json;
using Newtonsoft.Json;
using JsonSerializer = System.Text.Json.JsonSerializer;

namespace FrontendWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MembersController : ControllerBase
    {
        private readonly TixplorerContext _context;

        public MembersController(TixplorerContext context)
        {
            _context = context;
        }

        // GET: api/Members/Login
        [HttpGet]
        [Route("Login")]
        public async Task<string> Login(string? account, string? password)
        {//會員登入，並回傳是否成功的資料，如果回傳資料：0=資料不符登入失敗、1=登入成功、99=資料庫異常
            //資料庫若無資料，則回傳資料錯誤訊息
            if (_context.Members == null)
                return "DataError";

            //查找是否有符合輸入的會員帳號密碼資料
            var members = _context.Members.FirstOrDefault(m => (m.Email.Equals(account)) && (m.Password.Equals(password)) || (m.Phone.Equals(account)) && (m.Password.Equals(password)));
            
            //資料庫比對後若無資料，則回傳登入失敗訊息
            if (members == null)
                return "LoginFail";

            //資料庫比對後若有資料，則回傳登入成功訊息
            return "LoginSuccess";
        }


        /// Member/Edit更改密碼用
        [HttpGet]
        [Route("sendPwdVEmail")]    
        public async Task<string> sendPwdVEmail(string validCode, string email)
        {
            try
            {
                    Member members = _context.Members.FirstOrDefault(m => m.Email == email);
                                        //Google 發信帳號
                    string GoogleID = "a6563612@gmail.com";
                    //應用程式密碼
                    //string TempPwd = "FErigc";
                    string TempPwd2 = "mwmhqwwnczfgpfql";
                    string ReceiveMail = email;
                    string SmtpServer = "smtp.gmail.com";
                    int SmtpPort = 587;
                    MailMessage MailContent = new MailMessage();
                    MailContent.From = new MailAddress(GoogleID);
                    MailContent.Subject = "Tixplorer旅遊售票網站密碼驗證碼";
                    MailContent.Body = $"<h1>以下是您的驗證碼：「{validCode}」，請於更改密碼頁面上輸入該驗證碼</h1>";
                    MailContent.IsBodyHtml = true;
                    MailContent.SubjectEncoding = Encoding.UTF8;
                    MailContent.To.Add(new MailAddress(ReceiveMail));
                    using (SmtpClient client = new SmtpClient(SmtpServer, SmtpPort))
                    {
                        client.EnableSsl = true;
                        client.Credentials = new NetworkCredential(GoogleID, TempPwd2);//寄信帳密 
                        client.Send(MailContent); //寄出信件
                    }
                    return "SendSuccess";
            }
            catch (Exception ex)
            {//失敗時回傳錯誤訊息
                return "SendFail";
            }
        }


        [HttpGet]
        [Route("sendValidEmail")]
        public async Task<string> sendValidEmail(string validCode, string email)
        {//註冊流程信箱驗證 → 使用Gmail的Server發送驗證信件
            try
            {
                Member members = _context.Members.FirstOrDefault(m => m.Email == email);

                if (members == null)
                {//如果信箱沒有註冊過，則繼續進行驗證碼發送流程
                    //Google 發信帳號
                    string GoogleID = "WeiTest1024@gmail.com";
                    //應用程式密碼
                    string TempPwd = "hvfzkzjtcpoglxoa";
                    //接收信箱
                    string ReceiveMail = email;

                    string SmtpServer = "smtp.gmail.com";
                    int SmtpPort = 587;
                    MailMessage MailContent = new MailMessage();
                    MailContent.From = new MailAddress(GoogleID);
                    //信件標題
                    MailContent.Subject = "Tixplorer旅遊售票網站驗證碼";
                    //信件內容
                    MailContent.Body = $"<h1>您好，您的驗證碼為：「{validCode}」，請於網頁上輸入該驗證碼</h1>";
                    MailContent.IsBodyHtml = true;
                    //使用內容編碼系統
                    MailContent.SubjectEncoding = Encoding.UTF8;
                    MailContent.To.Add(new MailAddress(ReceiveMail));
                    using (SmtpClient client = new SmtpClient(SmtpServer, SmtpPort))
                    {
                        client.EnableSsl = true;
                        client.Credentials = new NetworkCredential(GoogleID, TempPwd);//寄信帳密 
                        client.Send(MailContent); //寄出信件
                    }
                    return "SendSuccess";
                }
                else
                {
                    return "noEmail";    
                }
            }
            catch (Exception ex)
            {//失敗時回傳錯誤訊息
                return "SendFail";
            }
        }

        [HttpGet]
        [Route("sendValidEmailForgetPwd")]
        //產生信箱驗證碼
        public async Task<string> sendValidEmailForgetPwd(string validCode, string email)
        {//忘記密碼流程信箱驗證 → 使用Gmail的Server發送驗證信件
            try
            {
                Member members = _context.Members.FirstOrDefault(m => m.Email == email);

                if (members != null)
                {//如果信箱有註冊過，則繼續進行驗證碼發送流程
                    //Google 發信帳號
                    string GoogleID = "WeiTest1024@gmail.com";
                    //應用程式密碼
                    string TempPwd = "hvfzkzjtcpoglxoa";
                    //接收信箱
                    string ReceiveMail = email;

                    string SmtpServer = "smtp.gmail.com";
                    int SmtpPort = 587;
                    MailMessage MailContent = new MailMessage();
                    MailContent.From = new MailAddress(GoogleID);
                    //信件標題
                    MailContent.Subject = "Tixplorer旅遊售票網站驗證碼";
                    //信件內容
                    MailContent.Body = $"<h1>您好，您的驗證碼為：「{validCode}」，請於網頁上輸入該驗證碼</h1>";
                    MailContent.IsBodyHtml = true;
                    //使用內容編碼系統
                    MailContent.SubjectEncoding = Encoding.UTF8;
                    MailContent.To.Add(new MailAddress(ReceiveMail));
                    using (SmtpClient client = new SmtpClient(SmtpServer, SmtpPort))
                    {
                        client.EnableSsl = true;
                        client.Credentials = new NetworkCredential(GoogleID, TempPwd);//寄信帳密 
                        client.Send(MailContent); //寄出信件
                    }
                    return "SendSuccess";
                }
                else
                {
                    return "isNotRegister";
                }
            }
            catch (Exception ex)
            {//失敗時回傳錯誤訊息
                return "SendFail";
            }
        }

        [HttpGet]
        [Route("registerMember")]
        public async Task<string> registerMember(string datas)
        {//註冊會員 API
            if (datas != null)
            {
                try
                {
                    Member member = new Member();
                    Member MemberDatas = JsonSerializer.Deserialize<Member>(datas);
                    //設置需要自動填入的資料，如紅利點數、註冊日期KDKDKD

                    member.Name = MemberDatas.Name;
                    member.Email = MemberDatas.Email;
                    member.Password = MemberDatas.Password;
                    member.Phone = MemberDatas.Phone;
                    member.Address = MemberDatas.Address;
                    member.Birthday = Convert.ToDateTime(MemberDatas.Birthday);
                    member.Sex = Convert.ToInt32(MemberDatas.Sex);

                    member = CreateDefault(member);

                    //將資料加入容器中
                    _context.Members.Add(member);

                    //存資料庫資料
                    _context.SaveChanges();

                    //回傳成功訊息
                    return "RegisterSuccess";
                }
                catch (Exception ex)
                {
                    return "RegisterFail";
                }
            }
            return "DataError";
        }

        [HttpGet]
        [Route("resetPwd")]
        public async Task<string> resetPwd(string email, string pwd)
        {//重設密碼流程 API

            Member member = _context.Members.FirstOrDefault(m => m.Email == email);

            if (member != null)
            {//如果會員資料存在於資料庫中，才能進行重設密碼作業
                try
                {
                    //重設密碼
                    member.Password = pwd;

                    //存資料庫資料
                    _context.SaveChanges();
                    return "ResetSuccess";
                }
                catch (Exception ex)
                {
                    return "MissingEmail";
                }
            }
            return "DataError";
        }

        private Member CreateDefault(Member item)
        {//此方法用於建立新建會員時部分預設值，如「註冊日期」
            //設定註冊日期為當前年月日
            item.RegisterDate = DateTime.Now;

            //設定最後登入日期為null值
            item.LastLoginDate = null;

            //設置紅利點數預設值為0
            item.BonusPoint = 0;

            return item;
        }
    }
}
