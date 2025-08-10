﻿using MailKit.Net.Smtp;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MimeKit;
using NotikaIdentityEmail.Entities;
using NotikaIdentityEmail.Models.IdentityModel;
using System.Threading.Tasks;

namespace NotikaIdentityEmail.Controllers
{
    public class RegisterController : Controller
    {
        private readonly UserManager<AppUser> _userManager;

        public RegisterController(UserManager<AppUser> userManager)
        {
            _userManager = userManager;
        }

        [HttpGet]
        public IActionResult CreateUser()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreateUser(RegisterUserViewModel model)
        {
            Random rnd = new Random();
            int code = rnd.Next(100000, 1000000);
            AppUser appUser = new AppUser()
            {
                Name = model.Name,
                Email = model.Email,
                Surname = model.Surname,
                UserName = model.Username,
                ActivationCode = code,
            };

            var result = await _userManager.CreateAsync(appUser, model.Password);
            if (result.Succeeded)
            {
                //Buraya mail kodları gelecek (tkbh gtaa dzry luxa)

                MimeMessage mimeMessage = new MimeMessage();

                MailboxAddress mailboxAddressFrom = new MailboxAddress("Admin", "merve.akba54@gmail.com");
                mimeMessage.From.Add(mailboxAddressFrom);

                MailboxAddress mailboxAddressTo = new MailboxAddress("User", model.Email);
                mimeMessage.To.Add(mailboxAddressTo);

                var bodyBuilder = new BodyBuilder();
                bodyBuilder.TextBody = "Hesabınızı dogrulamak için gerekli olan aktivasyon kodu: " + code;
                mimeMessage.Body = bodyBuilder.ToMessageBody();

                mimeMessage.Subject = "Notika Identity Aktivasyon Kodu";

                SmtpClient client = new SmtpClient();
                client.Connect("smtp.gmail.com", 587, false);
                client.Authenticate("merve.akba54@gmail.com", "tkbhgtaadzryluxa");
                client.Send(mimeMessage);
                client.Disconnect(true);

                TempData["EmailMove"] = model.Email;

                return RedirectToAction("UserActivation", "Activation");
            }
            else
            {
                foreach (var item in result.Errors)
                {
                    ModelState.AddModelError("", item.Description);
                }
            }
            return View();
        }
    }
}
