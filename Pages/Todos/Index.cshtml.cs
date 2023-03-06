using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using ToDotNet.Data;
using ToDotNet.Models;


namespace ToDotNet.Pages.Todos
{
    public class IndexModel : PageModel
    {
        private readonly ToDotNetContext _context;

        public IndexModel(ToDotNetContext context)
        {
            _context = context;
        }

        public IList<Todo> Todo { get; set; } = default!;

        [BindProperty(SupportsGet = true)]
        public string CategorySearchString { get; set; }


        public async Task OnGetAsync(int? uid)
        {
            if (_context.Todo != null)
            {
                if (CategorySearchString != null)
                {
                    // Bug: SQL Injection vulnerability.
                    //
                    // Examples:
                    // ' OR 1 = 1 --
                    // ' UNION SELECT object_id , name  , name, GETDATE(), GETDATE(), name, 'True', 0 FROM sys.tables --
                    Todo = await _context.Todo.FromSqlRaw(
                      $"SELECT * FROM Todo WHERE UserId = {uid} AND Category LIKE '%{CategorySearchString}%'")
                      .ToListAsync();
                }
                else
                {
                    Todo = await _context
                        .Todo
                        .Where(m => m.UserId == uid)
                        .Include(m => m.User)
                        .ToListAsync();
                }
            }

            ViewData["UserId"] = uid;
        }
    }
}
