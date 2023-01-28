using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using ToDotNet.Data;
using ToDotNet.Models;

namespace ToDotNet.Pages.Todos
{
    public class IndexModel : PageModel
    {
        private readonly ToDotNet.Data.ToDotNetContext _context;

        public IndexModel(ToDotNet.Data.ToDotNetContext context)
        {
            _context = context;
        }

        public IList<Todo> Todo { get;set; } = default!;

        public async Task OnGetAsync()
        {
            if (_context.Todo != null)
            {
                Todo = await _context.Todo
                .Include(t => t.User).ToListAsync();
            }
        }
    }
}
