using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatJaffApp.UITest
{
    public class WebTestingHostFactory<TProgram> : WebApplicationFactory<TProgram>
        where TProgram : class
    {
        protected override IHost CreateHost(IHostBuilder builder)
        {
            // Create the host that is actually used by the
            // TestServer (In Memory).
            var testHost = base.CreateHost(builder);
            // configure and start the actual host using Kestrel.
            builder.ConfigureWebHost(
              webHostBuilder => webHostBuilder.UseKestrel());
            var host = builder.Build();
            host.Start();
            // In order to cleanup and properly dispose HTTP server
            // resources we return a composite host object that is
            // actually just a way to intercept the StopAsync and Dispose
            // call and relay to our HTTP host.
            return new CompositeHost(testHost, host);
        }
    }
}
