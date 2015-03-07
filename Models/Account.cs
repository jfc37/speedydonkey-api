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
        
        public virtual string Email { get; set; }
        public virtual string Password { get; set; }
        public virtual int Id { get; set; }
        public virtual IUser User { get; set; }
    }
}
