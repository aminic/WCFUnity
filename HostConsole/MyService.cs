using MyLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace HostConsole
{
    [ServiceBehavior(IncludeExceptionDetailInFaults = true)]
    public class MyService : IMyService
    {
        public int AddInt(int a, int b)
        {
            return a + b;
        }

        public Student GetStudent()
        {
            Student stu = new Student();
            stu.StudentName = "小明";
            stu.StudentAge = 22;
            return stu;
        }

        public int Divide(int x, int y)
        {
            if (0 == y)
            {
                throw new FaultException("被除数y不能为零!");
            }
            return x / y;

        }

        public CalResultResponse ComputingNumbers(CalcultRequest inMsg)
        {
            CalResultResponse rmsg = new CalResultResponse();
            switch (inMsg.Operation)
            {
                case "加":
                    rmsg.ComputedResult = inMsg.NumberA + inMsg.NumberB;
                    break;
                case "减":
                    rmsg.ComputedResult = inMsg.NumberA - inMsg.NumberB;
                    break;
                case "乘":
                    rmsg.ComputedResult = inMsg.NumberA * inMsg.NumberB;
                    break;
                case "除":
                    rmsg.ComputedResult = inMsg.NumberA / inMsg.NumberB;
                    break;
                default:
                    throw new ArgumentException("运算操作只允许加、减、乘、除。");

            }
            return rmsg;
        }

        public void TestGetHeaderMethod()
        {
            int index = OperationContext.Current.IncomingMessageHeaders.FindHeader("userinfo", "check");
            if (index != -1)
            {
                var user = OperationContext.Current.IncomingMessageHeaders.GetHeader<UserInfo>(index);
                Console.WriteLine($"用户信息：{user.LoginName} {user.Password}");
            }
        }

        public void SaveImportInfo(string key, UserInfo userinfo)
        {
            Console.WriteLine("call Service Method SaveImportInfo");
        }
    }

}
