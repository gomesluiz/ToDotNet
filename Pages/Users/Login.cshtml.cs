using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ToDotNet.Data;
using ToDotNet.Models;

namespace ToDotNet.Pages.Users
{
    public class LoginModel : PageModel
    {
        private readonly ToDotNet.Data.ToDotNetContext _context;

        public LoginModel(ToDotNet.Data.ToDotNetContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public new User User { get; set; } = default!;
     
        public async Task<IActionResult> OnPostAsync()
        {

            var user = await _context.User.FirstOrDefaultAsync(
                m => m.Email == User.Email &&
                m.Password == User.Password
            );

            if (user == null)
            {
                return RedirectToPage("/Index");
            }

            var cookieOptions = new CookieOptions
            {
                Expires = DateTime.UtcNow.AddHours(2),
            };

            Response.Cookies.Append("UserName", User.Email, cookieOptions);


            return RedirectToPage("/Todos/Index", new { uid = user.Id });
        }
    }
}
