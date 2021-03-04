using ApplicationCore.Interfaces;
using ApplicationCore.Services;
using Autofac;
using Autofac.Integration.WebApi;
using Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Http;

namespace refactor_me.App_Start
{
	public class AutofacWebapiConfig
	{
    public static Autofac.IContainer Container;

    public static void Initialize(HttpConfiguration config)
    {
      Initialize(config, RegisterServices(new ContainerBuilder()));
    }


    public static void Initialize(HttpConfiguration config, Autofac.IContainer container)
    {
      config.DependencyResolver = new AutofacWebApiDependencyResolver(container);
    }

    private static Autofac.IContainer RegisterServices(ContainerBuilder builder)
    {
      //Register your Web API controllers.  
      builder.RegisterApiControllers(Assembly.GetExecutingAssembly());


      builder.RegisterType<ProductService>()
             .As<IProductService>()
             .InstancePerRequest();

			builder.RegisterType(typeof(DataAccessHelper))
						 .As(typeof(IDataAccessHelper))
						 .InstancePerRequest();

			//Set the dependency resolver to be Autofac.  
			Container = builder.Build();

      return Container;
    }
  }
}