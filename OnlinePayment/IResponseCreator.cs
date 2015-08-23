namespace OnlinePayments
{
    public interface IResponseCreator<out T>
    {
        T Create();
    }
}