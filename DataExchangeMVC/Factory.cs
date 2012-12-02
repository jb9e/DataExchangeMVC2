using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.Specialized;
using System.Configuration;
using DataExchangeMVC.Services.Interfaces;

namespace DataExchangeMVC
{
    public class Factory
    {
        private Factory() 
        { 
        }

        private static Factory _factory = new Factory();

        public static Factory GetInstance() 
        {
            return _factory;
        }

        public IService GetService(String serviceName)
        {
            Type type;
            Object obj = null;
            try
            {
                type = Type.GetType(GetImplName(serviceName));
                obj = Activator.CreateInstance(type);
            }
            catch (Exception ex)
            {
                throw;
            }
            return (IService)obj;
        }

        private string GetImplName(string servicename)
        {
            return ConfigurationManager.AppSettings[servicename];
        }
    }
}