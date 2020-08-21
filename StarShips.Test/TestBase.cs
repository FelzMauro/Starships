using Domain.Interfaces;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Services;
using SimpleInjector;
using Unity;

namespace StarShips.Test
{
    [TestClass]
    public class TestBase
    {
        protected readonly Container _container;
        public TestBase()
        {
            _container = new Container();

            _container.Options.DefaultScopedLifestyle = new SimpleInjector.Lifestyles.AsyncScopedLifestyle();

            _container.Register<IApiCaller, ApiCaller>();
            _container.Register<ICalculator, Calculator>();

            _container.Verify();
        }
    }
}
