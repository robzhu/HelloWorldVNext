using System;
using System.Linq;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNet.Builder;
using Microsoft.AspNet.Hosting.Server;
using Microsoft.AspNet.Owin;
using Microsoft.Framework.ConfigurationModel;

namespace Nowin.vNext
{
    public class NowinServerFactory : IServerFactory
    {
        private Func<object, Task> _callback;
        private const string ServerUrlConfigKey = "server.urls";
        private const int DefaultPort = 5000;

        private Task HandleRequest( IDictionary<string, object> env )
        {
            return _callback( new OwinFeatureCollection( env ) );
        }

        public IServerInformation Initialize( IConfiguration configuration )
        {
            int port = DefaultPort;
            try
            {
                //expect a commandline argument of the format: "--server.urls http://localhost:5000"
                var portString = configuration[ ServerUrlConfigKey ].Split( ':' ).Last();
                port = int.Parse( portString );
            }
            catch { }

            Console.WriteLine( "Hosting on port: {0}", port );
            var builder = ServerBuilder.New()
                                       .SetAddress( IPAddress.Any )
                                       .SetPort( port )
                                       .SetOwinApp( OwinWebSocketAcceptAdapter.AdaptWebSockets( HandleRequest ) );

            return new NowinServerInformation( builder );
        }

        public IDisposable Start( IServerInformation serverInformation, Func<object, Task> application )
        {
            var information = (NowinServerInformation)serverInformation;
            _callback = application;
            INowinServer server = information.Builder.Build();
            server.Start();
            return server;
        }

        private class NowinServerInformation : IServerInformation
        {
            public NowinServerInformation( ServerBuilder builder )
            {
                Builder = builder;
            }

            public ServerBuilder Builder { get; private set; }
            public string Name { get { return "Nowin"; } }
        }
    }
}