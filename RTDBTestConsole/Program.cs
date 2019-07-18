using System;
using Framework.Autofac;
using Microsoft.Extensions.Configuration;
using Utils.Attributes;
using Utils.Helper;

namespace RTDBTestConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            GlobalAttributes.RepositoryName = "RTDBInterface";
            GlobalAttributes.Configuration = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();
            AutofacModule.InitTest();
            Console.WriteLine(RTDBHelper.GetDataByTagAndTime("HLU1_HLU1_FIC53101", DateTime.Now));
        }
    }
}