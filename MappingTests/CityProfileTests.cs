using WebAPI.Mapping.Profiles;

namespace MappingTests
{
    /// <summary>
    /// Модульные тесты для <see cref="WebAPI.Mapping.Profiles.CityProfile"/>.
    /// </summary>
    [TestFixture]
    public sealed class CityProfileTests : BaseProfileTests<CityProfileTests>
    {
        [Test]
        public void ConfigureSucces()
        {
            this.Mapper.ConfigurationProvider.AssertConfigurationIsValid();
        }
    }
}
