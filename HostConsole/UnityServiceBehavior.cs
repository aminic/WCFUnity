using Microsoft.Practices.Unity;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.ServiceModel.Description;
using System.ServiceModel.Dispatcher;
using System.Text;
using System.Threading.Tasks;

namespace HostConsole
{
    public class UnityServiceBehavior : IServiceBehavior
    {
        public UnityInstanceProvider InstanceProvider
        { get; set; }

        private ServiceHost serviceHost = null;

        public UnityServiceBehavior()
        {
            Console.WriteLine($"call {this.GetType().Name}.UnityServiceBehavior()");
            InstanceProvider = new UnityInstanceProvider();
        }
        public UnityServiceBehavior(UnityContainer unity)
        {
            Console.WriteLine($"call {this.GetType().Name}.UnityServiceBehavior(UnityContainer unity)");
            InstanceProvider = new UnityInstanceProvider();
            InstanceProvider.Container = unity;
        }
        public void ApplyDispatchBehavior(ServiceDescription serviceDescription, ServiceHostBase serviceHostBase)
        {
            Console.WriteLine($"call {this.GetType().Name}.ApplyDispatchBehavior(ServiceDescription serviceDescription, ServiceHostBase serviceHostBase)");
            foreach (ChannelDispatcherBase cdb in serviceHostBase.ChannelDispatchers)
            {
                ChannelDispatcher cd = cdb as ChannelDispatcher;
                if (cd != null)
                {
                    foreach (EndpointDispatcher ed in cd.Endpoints)
                    {
                        InstanceProvider.ServiceType = serviceDescription.ServiceType;
                        ed.DispatchRuntime.InstanceProvider = InstanceProvider;

                    }
                }
            }
        }


        public void Validate(ServiceDescription serviceDescription, ServiceHostBase serviceHostBase)
        {
            Console.WriteLine($"call {this.GetType().Name}.Validate(ServiceDescription serviceDescription, ServiceHostBase serviceHostBase)");
        }


        public void AddBindingParameters(
            ServiceDescription serviceDescription,
            ServiceHostBase serviceHostBase,
            Collection<ServiceEndpoint> endpoints,
            BindingParameterCollection bindingParameters)
        {
            Console.WriteLine($"call {this.GetType().Name}.AddBindingParameters");
        }

    }
}
