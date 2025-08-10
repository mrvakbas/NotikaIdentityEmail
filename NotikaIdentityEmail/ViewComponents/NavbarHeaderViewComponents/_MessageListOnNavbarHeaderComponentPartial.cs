﻿using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using NotikaIdentityEmail.Context;
using NotikaIdentityEmail.Entities;
using NotikaIdentityEmail.Models.MessageViewModels;
using System.Threading.Tasks;

namespace NotikaIdentityEmail.ViewComponents.NavbarHeaderViewComponents
{
    public class _MessageListOnNavbarHeaderComponentPartial : ViewComponent
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly EmailContext _context;

        public _MessageListOnNavbarHeaderComponentPartial(UserManager<AppUser> userManager, EmailContext context)
        {
            _userManager = userManager;
            _context = context;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var userValue = await _userManager.FindByNameAsync(User.Identity.Name);
            var UserEmail = userValue.Email;
            var values = from message in _context.Messages
                         join user in _context.Users
                         on message.SenderEmail equals user.Email
                         where message.ReceiverEmail == UserEmail
                         select new MessageListWithUsersInfoViewModel
                         {
                             FullName = user.Name + " " + user.Surname,
                             ProfileImageUrl = user.ImageUrl,
                             SendDate = message.SendDate,
                             MessageDetail = message.MessageDetail
                         };
            return View(values.ToList());
        }
    }
}
