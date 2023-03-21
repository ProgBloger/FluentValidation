namespace FluentValidationExamples.ExternalClients
{
    public class SomeExternalDatabase
    {
        public async Task<string> GetNotNullMessage()
        {
            await Task.Delay(TimeSpan.FromSeconds(1));

            return "{PropertyName} is required.";
        }
    }
}
