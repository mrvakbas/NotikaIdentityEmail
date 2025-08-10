using Microsoft.AspNetCore.Mvc;
using NotikaIdentityEmail.Context;

namespace NotikaIdentityEmail.ViewComponents.NavbarHeaderViewComponents
{
    public class _NotificationListOnNavbarHeaderComponentPartial : ViewComponent
    {
        private readonly EmailContext _context;

        public _NotificationListOnNavbarHeaderComponentPartial(EmailContext context)
        {
            _context = context;
        }

        public IViewComponentResult Invoke()
        {
            var values = _context.Notifications.ToList();
            return View(values);
        }
    }
}
