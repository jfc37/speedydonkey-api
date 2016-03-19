namespace Contracts.Users
{
    public class AuthZeroUserModel
    {
        public AuthZeroUserModel()
        {
            
        }

        public AuthZeroUserModel(string userId)
        {
            UserId = userId;
        }

        public string UserId { get; set; }
    }
}
