using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Threading;

namespace FluentFramework.Core
{
    public class Logger
    {
        public Logger()
        {
            Trace.Listeners.Add(new TextWriterTraceListener(Console.Out));
            messages = new List<string>();
        }

        private List<string> messages;

        public void LogMessage(string message)
        {
            messages.Add(message);
            Trace.WriteLine(message);
        }

        public void LogMessageWithTimestamp(string message)
        {
            var msg = $"{DateTime.Now.ToString("MM/dd hh:mm:ss")} - {message}";
            messages.Add(msg);
            Trace.WriteLine(msg);
        }

        public void Publish(string filePath)
        {
            using (var sw = new StreamWriter(filePath))
            {
                foreach (var line in messages)
                {
                    sw.WriteLine(line);
                }
            }
        }
    }
}
