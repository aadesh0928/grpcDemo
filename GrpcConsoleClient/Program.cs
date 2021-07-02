using System;
using System.Threading.Tasks;
using Grpc.Core;
using Grpc.Net.Client;
namespace GrpcConsoleClient
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var channel = GrpcChannel.ForAddress("https://localhost:5001");
            var client = new Employee.EmployeeClient(channel);
            var request = new EmployeeRequest
            { EmplouyeeName = "a",
                EmployeeId = "1",
                SearchBy = EmployeeRequest.Types.SearchBy.Name 
            };
            using (var res = client.GetEmployeeDetails(request))
            {
                while(await res.ResponseStream.MoveNext())
                {
                    EmployeeResponse employee = res.ResponseStream.Current;
                    Console.WriteLine($"Employee name is {employee.Name}, age : {employee.Address}");
                }
            }
            Console.ReadLine();
        }
    }
}
