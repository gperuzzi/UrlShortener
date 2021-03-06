﻿using System;
using System.Web.Mvc;
using Ninject;
using System.Web.Routing;
using Ninject.Modules;
using UrlShortener.Domain.Service;
using System.Configuration;
using UrlShortener.Domain.SQLRepository;

namespace UrlShortener
{
    public class NinjectControllerFactory: DefaultControllerFactory
    {
        private IKernel kernel = new StandardKernel(new UrlShortenerModule());

        protected override IController GetControllerInstance(RequestContext context, Type controllerType)
        {
            if (controllerType == null)
                return null;
            return (IController)kernel.Get(controllerType);
        }


        private class UrlShortenerModule : NinjectModule
        {
            public override void Load()
            {
                Bind<ISQLRepository>()
                    .To<SQLRepository>()
                    .WithConstructorArgument("connectionString",
                                             ConfigurationManager.ConnectionStrings["AppDb"].ConnectionString
                    );

                Bind<IUrlShortenerService>().To<UrlShortenerService>();
            }
        }


    }
}