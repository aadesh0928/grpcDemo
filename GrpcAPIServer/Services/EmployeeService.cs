using Grpc.Core;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Google.Protobuf.Collections;
namespace GrpcAPIServer.Services
{
    public class EmployeeService: Employee.EmployeeBase
    {
        private readonly ILogger<EmployeeService> _logger;

        private List<EmployeeResponse> _employees;
        public EmployeeService(ILogger<EmployeeService> logger)
        {
            _logger = logger;
            _employees = new List<EmployeeResponse>();
            this.LoadEmployees();
        }

        private void LoadEmployees()
        {
            _employees.AddRange(new List<EmployeeResponse>
            {
                new EmployeeResponse
                {
                    Address = "Gurugram",
                    DateOfBirth = "28/09/1985",
                    Id = 1,
                    Name = "aadesh",
                     Phone = "9654355626"
                },
               new EmployeeResponse
                {
                    Address = "Noida",
                    DateOfBirth = "12/06/2000",
                    Id = 1,
                    Name = "RatiShankar",
                     Phone = "9654355626"
                },
                                                new EmployeeResponse
                {
                    Address = "Sector 90, Gurugram",
                    DateOfBirth = "23/09/1997",
                    Id = 1,
                    Name = "Mukesh",
                     Phone = "9654355626"
                },
                                                                new EmployeeResponse
                {
                    Address = "Rohini, New Delhi",
                    DateOfBirth = "28/09/1985",
                    Id = 1,
                    Name = "Ankur",
                     Phone = "9654355626"
                },
                                                                                new EmployeeResponse
                {
                    Address = "Rithala",
                    DateOfBirth = "28/09/1985",
                    Id = 1,
                    Name = "Param",
                     Phone = "9654355626"
                }

            });
        }
        public override async Task GetEmployeeDetails(EmployeeRequest request, IServerStreamWriter<EmployeeResponse> responseStream, ServerCallContext context)
        {
            List<EmployeeResponse> filtereEmployees = null;
            switch (request.SearchBy)
            {
                case EmployeeRequest.Types.SearchBy.Id:
                    {
                        filtereEmployees = _employees
                            .Where(employee => 
                            employee.Id == int.Parse(request.EmployeeId)
                            ).ToList();
                        break;
                    }
                case EmployeeRequest.Types.SearchBy.Name:
                    {
                        filtereEmployees = _employees
                            .Where(empl =>
                            empl.Name.Contains(request.EmplouyeeName)).ToList();
                        break;
                    }
            }

            foreach(var empployee in filtereEmployees)
            {
                await responseStream.WriteAsync(empployee);
            }

        }


    }
}
