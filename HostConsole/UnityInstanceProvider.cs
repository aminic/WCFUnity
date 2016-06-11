using Microsoft.Practices.Unity;
using MyLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.ServiceModel.Description;
using System.ServiceModel.Dispatcher;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace HostConsole
{
    public class UnityInstanceProvider : IInstanceProvider
    {
        public UnityContainer Container { set; get; }
        public Type ServiceType { set; get; }

        public UnityInstanceProvider()
               : this(null)
        {
        }

        public UnityInstanceProvider(Type type)
        {
            Console.WriteLine($"call {this.GetType().Name}.UnityInstanceProvider");
            ServiceType = type;
            Container = new UnityContainer();
        }

        #region IInstanceProvider Members

        public object GetInstance(InstanceContext instanceContext, Message message)
        {
            Console.WriteLine($"call {this.GetType().Name}.GetInstance(InstanceContext instanceContext, Message message)");


            int index = OperationContext.Current.IncomingMessageHeaders.FindHeader("userinfo", "check");
            if (index != -1)
            {
                var user = OperationContext.Current.IncomingMessageHeaders.GetHeader<UserInfo>(index);
                Console.WriteLine($"用户信息：{user.LoginName} {user.Password}");
            }


            var reader = OperationContext.Current.RequestContext.RequestMessage.GetReaderAtBodyContents();
            var xmlcontent = reader.ReadOuterXml();

            var doc = new XmlDocument();
            doc.LoadXml(xmlcontent);


            var methodName = doc.DocumentElement.Name;

            Console.WriteLine($"Call Method Name : {methodName}");

            throw new FaultException("error");

            return Container.Resolve(ServiceType);
        }

        public object GetInstance(InstanceContext instanceContext)
        {
            Console.WriteLine($"call {this.GetType().Name}.GetInstance(InstanceContext instanceContext)");
            return GetInstance(instanceContext, null);
        }
        public void ReleaseInstance(InstanceContext instanceContext, object instance)
        {
            Console.WriteLine($"call {this.GetType().Name}.ReleaseInstance(InstanceContext instanceContext, object instance)");
        }

        #endregion
    }
}

