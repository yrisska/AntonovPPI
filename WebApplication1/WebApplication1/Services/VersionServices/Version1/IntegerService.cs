namespace WebApplication1.Services.VersionServices.Version1
{
    public class IntegerService : IIntegerService
    {
        public async Task<int> GetRandomInteger() => await Task.FromResult(new Random().Next());
    }
}
