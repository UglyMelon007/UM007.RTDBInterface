using System;
using System.Threading.Tasks;
using AspectCore.DynamicProxy;
using log4net;
using Utils.Attributes;

namespace Framework.AspectCore
{
    public class LogInterceptor : AbstractInterceptor
    {
        private readonly ILog _log = LogManager.GetLogger(GlobalAttributes.RepositoryName, typeof(LogInterceptor));

        public override async Task Invoke(AspectContext context, AspectDelegate next)
        {
            try
            {
                _log.Info("Before service call");
                await next(context);
            }
            catch (Exception ex)
            {
                _log.Error($"Service threw an exception!{ex}");
                throw;
            }
            finally
            {
                _log.Info("After service call");
            }
        }
    }
}