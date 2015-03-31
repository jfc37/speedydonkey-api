namespace Common
{
    public interface ICurrentUser
    {
        int Id { get; set; }
    }
    public class CurrentUser : ICurrentUser
    {
        public int Id
        {
            get { return _id.GetValueOrDefault(); }
            set { _id = _id ?? value; }
        }

        private int? _id;
    }
}
