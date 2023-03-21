namespace FluentValidationExamples
{
    public interface IFactory
    {
        T Create<T>();
    }
}