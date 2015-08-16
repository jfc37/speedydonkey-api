namespace Data.CodeChunks
{
    public interface ICodeChunk<out T>
    {
        T Do();
    }
}