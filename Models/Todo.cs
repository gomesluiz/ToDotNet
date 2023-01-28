namespace ToDotNet.Models
{
    public class Todo
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public DateTime Created { get; set; } = DateTime.Now;
        public DateTime Due { get; set; }
        public string Category { get; set; } = string.Empty;
        public bool IsCompleted { get; set; } = false;

        public int UserId { get; set; }
        public User User { get; set; }
    }
}
