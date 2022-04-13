using Calculator;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace Project05
{
    public static class ContainerBuild
    {
        public static IServiceProvider Build()
        {
            var service = new ServiceCollection();

            service.AddSingleton<UserInterface>();
            service.AddSingleton<ReadWriteFile>();
            service.AddScoped<IProcessing, Processing>();
            service.AddScoped<IComputation, Computation>();
            service.AddSingleton<DataProcessing>();

            return service.BuildServiceProvider();
        }
    }
}
