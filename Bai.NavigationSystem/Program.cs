using System;
using System.Reflection;
using Autofac;
using Bai.NavigationSystem.BureauApi;
using RestSharp;

namespace Bai.NavigationSystem
{
    public class Program
    {
        static void Main(string[] args)
        {
            ContainerBuilder containerBuilder = CreateContainerBuilder();

            using (IContainer container = containerBuilder.Build())
            {
                var controlCenter = container.Resolve<IControlCenter>();
                controlCenter.Execute();
                Console.WriteLine(controlCenter.ProbeReport());
                Console.ReadLine();
            }
        }

        private static ContainerBuilder CreateContainerBuilder()
        {
            Assembly programAssembly = Assembly.GetExecutingAssembly();

            var builder = new ContainerBuilder();
            builder.RegisterAssemblyTypes(programAssembly)
                   .AsImplementedInterfaces();

            // We need to override the registration above, as we need to specify an email address
            builder.RegisterType<MissionControlProxy>()
                   .AsImplementedInterfaces()
                   .WithParameter("anEmail", "shahid_naveed@msn.com");

            builder.RegisterType<RestClient>()
                   .AsImplementedInterfaces();

            builder.RegisterType<RestRequest>()
                   .AsImplementedInterfaces();

            return builder;
        }
    }
}