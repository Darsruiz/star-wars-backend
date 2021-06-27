using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace star_wars.CustomExceptions
{
    public class CustomException : Exception
    {
        private static NLog.Logger logger = NLog.LogManager.GetCurrentClassLogger();
        public override string Message => "Cannot be null";
        public CustomException() { }
        public CustomException(string message, Exception innerException) : base(message, innerException)
        {
            string ExceptionFilePath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            string[] ExceptionText = { $"{ message } { innerException }" };
            File.AppendAllLines(Path.Combine(ExceptionFilePath, "star_wars_exceptions_null.txt"), ExceptionText);
            logger.Warn(ExceptionText[0]);
        }
    }
}
