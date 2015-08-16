namespace OnlinePayments
{
    public interface IResponseCreator<T>
    {
        T Create();
    }
}