namespace MappingTests
{
    using AutoMapper;
    using Microsoft.Extensions.DependencyInjection;
    using Moq;
    using WebAPI.Extentions;

    /// <summary>
    /// Базовый класс для тестов на профили.
    /// </summary>
    /// <typeparam name="TTest"></typeparam>
    public abstract class BaseProfileTests<TTest>
        where TTest : BaseProfileTests<TTest>
    {
        private readonly IServiceCollection services;

        protected BaseProfileTests()
        {
            this.services = new ServiceCollection()
                .AddAutoMapper()
                .AddLogging();
        }

        protected TTest AddService<TInterface>()
            where TInterface : class
        {
            this.services.AddScoped<TInterface>(_ => Mock.Of<TInterface>(MockBehavior.Loose));
            return (TTest)this;  
        }

        protected TTest AddService<TInterface, TRealisation>()
            where TInterface : class
            where TRealisation: class, TInterface
        {
            this.services.AddScoped<TInterface, TRealisation>();
            return (TTest)this;
        }

        protected TTest AddService<TInterface, TRealisation>(TRealisation realisation)
            where TInterface : class
            where TRealisation : class, TInterface
        {
            this.services.AddScoped<TInterface, TRealisation>(_ => realisation);
            return (TTest)this;
        }

        protected IMapper Mapper
        {
            get => field ??= this.services.BuildServiceProvider().GetService<IMapper>()
                ?? throw new Exception($"Service \"{nameof(IMapper)}\" was not found");
        }
    }
}
