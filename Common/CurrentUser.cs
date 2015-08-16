namespace Common
{
    public interface ICurrentUser
    {
        int Id { get; set; }

        bool IsLoggedIn { get; }
    }
    public class CurrentUser : ICurrentUser
    {
        public int Id
        {
            get { return _id.GetValueOrDefault(); }
            set { _id = _id ?? value; }
        }

        public bool IsLoggedIn { get { return _id.HasValue; } }

        private int? _id;
    }
}
