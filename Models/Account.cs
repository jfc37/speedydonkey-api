namespace Models
{
    public class Account : IAccount, IEntity 
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
