namespace OnlinePayments
{
    public class ResponseCreator<T> : IResponseCreator<T>
        where T : new()
    {
        public T Create()
        {
            return new T();
        }
    }
}