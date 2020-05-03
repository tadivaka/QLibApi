using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;
using QLibrary.Common.Enums;

namespace QLibrary.Common.Extensions
{
    public static class Log4NetExtensions
    {
        public static void WriteLogInformation(this ILogger logger,ComponentLayer componentLayer,string className,string methodName, MethodLine methodLine, string message,params object[] args)
        {
            var layer = string.Empty;
            switch (componentLayer)
            {
                case ComponentLayer.API:
                    
                    break;
                case ComponentLayer.REPOSITORY:
                    logger.LogInformation("REPOSITORYLAYER", className, methodName, message, args);
                    break;
                default:
                    break;
            }

            var line = string.Empty;
            switch (methodLine)
            {
                case MethodLine.BEGIN:
                    line = "BEGIN";
                    break;
                case MethodLine.END:
                    line = "END";
                    break;
                default:
                    break;
            }

            logger.LogInformation(layer, line ,className, methodName,message, args);

        }

        public static void WriteLogError(this ILogger logger, ComponentLayer componentLayer, string className, string methodName, string message, params object[] args)
        {
            switch (componentLayer)
            {
                case ComponentLayer.API:
                    logger.LogError("APILAYER", className, methodName, message, args);
                    break;
                case ComponentLayer.REPOSITORY:
                    logger.LogError("REPOSITORYLAYER", className, methodName, message, args);
                    break;
                default:
                    break;
            }

        }

    }
}
