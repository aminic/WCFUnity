using Microsoft.Practices.Unity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HostConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                var baseAddress = "http://localhost:999/";
                var ws = new RealServerHost(baseAddress);
                ws.Start();
                Console.WriteLine("已启动");
            }
            catch (Exception ex)
            {
                var message = "服务端异常[" + ex.GetType().Name + "]:" + ex.Message;
                Console.WriteLine(message);
            }





            Console.ReadKey();
        }
    }
}
