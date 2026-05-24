using Domain;

namespace MappingTests
{
    /// <summary>
    /// Модульные тесты для <see cref="WebAPI.Mapping.Profiles.TitleProfile"/>.
    /// </summary>
    [TestFixture]
    public sealed class TitleProfileTests : BaseProfileTests<TitleProfileTests>
    {
        [Test]
        public void Configure_Success()
        {
            this.Mapper.ConfigurationProvider.AssertConfigurationIsValid();
        }

        [Test]
        public void MapFromEntityToModel()
        {
            // arrange
            var entity = new Title("Тестовый заголовок");

            // act
            var model = this.Mapper.Map<string>(entity);

            // assert
            Assert.That(model, Is.Not.Null);
            Assert.That(model, Is.EqualTo("Тестовый заголовок"));
        }

        [Test]
        public void MapFromModelToEntity()
        {
            // arrange
            var model = "Тестовый заголовок";

            // act
            var entity = this.Mapper.Map<Title>(model);

            // assert
            Assert.That(entity, Is.Not.Null);
            Assert.That(entity.Value, Is.EqualTo("Тестовый заголовок"));
        }
    }

    
}
