using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using ToDotNet.Data;
using ToDotNet.Models;

namespace ToDotNet.Pages.Todos
{
    public class DetailsModel : PageModel
    {
        private readonly ToDotNetContext _context;

        public DetailsModel(ToDotNetContext context)
        {
            _context = context;
        }

      public Todo Todo { get; set; } = default!; 

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.Todo == null)
            {
                return NotFound();
            }

            var todo = await _context.Todo.FirstOrDefaultAsync(m => m.Id == id);
            if (todo == null)
            {
                return NotFound();
            }
            else 
            {
                Todo = todo;
            }
            return Page();
        }
    }
}
