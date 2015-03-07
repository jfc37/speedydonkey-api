namespace Models
{
    public interface IAccount
    {
        string Email { get; set; }
        string Password { get; set; }
        int Id { get; set; }
        IUser User { get; set; }
    }
    public class Account : IAccount, IEntity 
    {
        
        public string Email { get; set; }
        public string Password { get; set; }
        public int Id { get; set; }
        public IUser User { get; set; }
    }
}
