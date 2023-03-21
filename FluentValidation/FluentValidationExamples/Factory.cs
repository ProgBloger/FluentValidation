namespace FluentValidationExamples
{
    public class Factory : IFactory
    {
        private readonly IServiceProvider serviceProvider;

        public Factory(IServiceProvider serviceProvider)
        {
            this.serviceProvider = serviceProvider;
        }

        public T Create<T>()
        {
            return serviceProvider.GetService<T>();
        }
    }
}
