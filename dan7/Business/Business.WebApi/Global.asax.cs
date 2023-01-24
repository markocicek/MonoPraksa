using Autofac;
using Autofac.Integration.WebApi;
using Business.Repository;
using Business.Repository.Common;
using Business.Service;
using Business.Service.Common;
using Business.WebApi.App_Start;
using Business.WebApi.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Http;
using System.Web.Routing;

namespace Business.WebApi
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            GlobalConfiguration.Configure(WebApiConfig.Register);
            AutofacConfig.StartAutofac();
        }
    }
}
