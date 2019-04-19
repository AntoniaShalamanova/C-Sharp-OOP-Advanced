namespace Logger.Appenders
{
    using Layouts.Contracts;
    using Enums;
    using System;

    public class ConsoleAppender : Appender
    {
        public ConsoleAppender(ILayout layout)
            :base(layout)
        {
        }

        public override void Append(string dateTime, ReportLevel reportLevel,
            string message)
        {
            if (this.ReportLevel <= reportLevel)
            {
                this.MessagesCount++;

                Console.WriteLine(
                this.layout.Format, dateTime, reportLevel, message);
            }
        }

        public override string ToString()
        {
            return $"Appender type: {this.GetType().Name}, " +
                $"Layout type: {this.layout.GetType().Name}, " +
                $"Report level: {this.ReportLevel}, " +
                $"Messages appended: {this.MessagesCount}";
        }
    }
}
