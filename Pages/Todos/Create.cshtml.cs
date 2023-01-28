using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ToDotNet.Models;

namespace ToDotNet.Pages.Todos
{
    public class CreateModel : PageModel
    {
        private readonly Data.ToDotNetContext _context;

        public CreateModel(Data.ToDotNetContext context)
        {
            _context = context;
        }

        public IActionResult OnGet(int? uid)
        {
            ViewData["UserId"] = uid;
            return Page();
        }

        [BindProperty]
        public Todo Todo { get; set; } = default!;
        
        public async Task<IActionResult> OnPostAsync()
        {
            _context.Todo.Add(Todo); await _context.SaveChangesAsync();

            return RedirectToPage("./Index", new { uid = Todo.UserId});
        }
    }
}