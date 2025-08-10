using Microsoft.AspNetCore.Mvc;
using NotikaIdentityEmail.Context;
using NotikaIdentityEmail.Entities;

namespace NotikaIdentityEmail.ViewComponents.MessageViewComponents
{
    public class _MessageCategoryListSidebarComponentPartial : ViewComponent
    {
        private readonly EmailContext _context;

        public _MessageCategoryListSidebarComponentPartial(EmailContext context)
        {
            _context = context;
        }

        public IViewComponentResult Invoke(int id)
        {
            var values = _context.Categories.ToList();
            return View(values);
        }
    }
}
