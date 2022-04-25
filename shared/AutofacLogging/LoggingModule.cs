using Autofac.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Autofac;
using DotnetSoftwarePlatform.Common.Interfaces;

namespace DotnetSoftwarePlatform.AutofacLogging
{
    public sealed class LoggingModule : Autofac.Module
    {
        private static void InjectLoggerProperties(ActivatedEventArgs<object> e)
        {
            var instanceType = e.Instance.GetType();

            // Get all the injectable properties to set.
            // If you wanted to ensure the properties were only UNSET properties,
            // here's where you'd do it.
            var properties = instanceType
              .GetProperties(BindingFlags.Public | BindingFlags.Instance)
              .Where(p => p.PropertyType == typeof(ILogger) && p.CanWrite &&
                p.GetIndexParameters().Length == 0);

            // Set the properties located.
            foreach (var propToSet in properties)
            {
                propToSet.SetValue(e.Instance, e.Context.Resolve<ILoggerFactory>().GetLogger(instanceType.FullName), null);
            }
        }

        private static void OnComponentPreparing(object sender, PreparingEventArgs e)
        {
            e.Parameters = e.Parameters.Union(
              new[]{
                new ResolvedParameter(
                    (p, i) => p.ParameterType == typeof(ILogger),
                    (p, i) => i.Resolve<ILoggerFactory>().GetLogger(p.Member.DeclaringType.FullName)
                ),
              });
        }
    }
}
