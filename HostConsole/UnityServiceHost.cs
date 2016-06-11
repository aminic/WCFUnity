using Microsoft.Practices.Unity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.ServiceModel.Description;
using System.ServiceModel.Dispatcher;
using System.Text;
using System.Threading.Tasks;

namespace HostConsole
{
    public class UnityServiceHost : ServiceHost
    {
        public UnityContainer Container { set; get; }

        public UnityServiceHost()
            : base()
        {
            Container = new UnityContainer();
        }

        public UnityServiceHost(UnityContainer container, Type serviceType, params Uri[] baseAddresses)
            : base(serviceType, baseAddresses)
        {
            Container = container;
        }

        protected override void OnOpening()
        {
            if (this.Description.Behaviors.Find<UnityServiceBehavior>() == null)
                this.Description.Behaviors.Add(new UnityServiceBehavior(Container));

            base.OnOpening();
        }
    }
}
