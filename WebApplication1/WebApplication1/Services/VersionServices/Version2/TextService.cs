namespace WebApplication1.Services.VersionServices.Version2
{
    public class TextService : ITextService
    {
        public async Task<string> GetLoremText() => await Task.FromResult("Lorem ipsum");
    }
}
