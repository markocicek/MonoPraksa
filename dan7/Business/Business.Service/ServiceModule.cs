﻿using Autofac;
using Business.Service.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Service
{
    public class ServiceModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<CustomerService>().As<ICustomerService>();
            builder.RegisterType<EmployeeService>().As<IEmployeeService>();
        }
    }
}
