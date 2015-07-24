using Models.OnlinePayments;

namespace OnlinePayments
{
    public interface IResponseCreator<T>
    {
        T Create();
    }
}