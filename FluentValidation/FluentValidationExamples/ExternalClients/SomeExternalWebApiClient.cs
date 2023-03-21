namespace FluentValidationExamples.ExternalClients
{
    public class SomeExternalWebApiClient
    {
        public async Task<bool> IdExists(int id)
        {
            await Task.Delay(TimeSpan.FromSeconds(1));

            return true;
        }

        public async Task<bool> ClientCanBeString(int id)
        {
            await Task.Delay(TimeSpan.FromSeconds(1));

            return false;
        }
    }
}
