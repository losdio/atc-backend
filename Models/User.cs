namespace atc_backend.Models
{
    public class User
    {
        public int UserId { get; set; } = 0;
        public string UserName { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public DateTime CreatedOn { get; set; } = DateTime.Now;
    }
}
