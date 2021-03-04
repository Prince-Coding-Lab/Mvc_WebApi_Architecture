﻿using ApplicationCore.Dto;
using AutoMapper;
using refactor_me.App_Start;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Routing;

namespace refactor_this
{
	public class WebApiApplication : System.Web.HttpApplication
	{
		protected void Application_Start()
		{
			Bootstrapper.Run();
			Mapper.Initialize(c => c.AddProfile<AutoMapperProfile>());
			GlobalConfiguration.Configure(WebApiConfig.Register);
		}
	}
}