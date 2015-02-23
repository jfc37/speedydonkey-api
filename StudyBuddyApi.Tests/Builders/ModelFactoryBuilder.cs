using SpeedyDonkeyApi.Models;
using SpeedyDonkeyApi.Services;

namespace SpeedyDonkeyApi.Tests.Builders
{
    public class ModelFactoryBuilder
    {
        private IUrlConstructor _urlConstructor;

        public ModelFactory Build()
        {
            return new ModelFactory(_urlConstructor);
        }

        public ModelFactoryBuilder WithUrlConstructor(IUrlConstructor urlConstructor)
        {
            _urlConstructor = urlConstructor;
            return this;
        }
    }
}