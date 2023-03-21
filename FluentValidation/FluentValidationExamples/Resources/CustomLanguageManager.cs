namespace FluentValidationExamples.Resources
{
    // You could also implement the ILanguageManager interface directly
    // if you want to load the messages from a completely different location
    // other than the FluentValidation default (for example, if you wanted
    // to store FluentValidation’s default messages in a database).
    public class CustomLanguageManager : FluentValidation.Resources.LanguageManager
    {
        // Note that if you replace messages in the en culture, you should consider also
        // replacing the messages for en-US and en-GB too, as these will take precedence
        // for users from these locales.
        public CustomLanguageManager()
        {
            // The default message for the NotNull validator is '{PropertyName}' must not be empty.
            AddTranslation("en", "NotNullValidator", "'{PropertyName}' is required.");
            AddTranslation("en-US", "NotNullValidator", "'{PropertyName}' is required.");
            AddTranslation("en-GB", "NotNullValidator", "'{PropertyName}' is required.");
        }
    }
}
