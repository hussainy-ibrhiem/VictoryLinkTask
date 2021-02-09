using Topshelf;

namespace HandlingService
{
    internal static class ConfigureService
    {
        internal static void Configure()
        {
            HostFactory.Run(configure =>
            {
                configure.Service<HandelingWindowsService>(service =>
                {
                    service.ConstructUsing(s => new HandelingWindowsService());
                    service.WhenStarted(s => s.Start());
                    service.WhenStopped(s => s.Stop());
                });
                //Setup Account that window service use to run.  
                configure.RunAsLocalSystem();
                configure.SetServiceName("HandelingRequestes");
                configure.SetDisplayName("Handeling Requestes");
                configure.SetDescription("Handeling Requestes .Net windows service with Topshelf");
            });
        }
    }
}
