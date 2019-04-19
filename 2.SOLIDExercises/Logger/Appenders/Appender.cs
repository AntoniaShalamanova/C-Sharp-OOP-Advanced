namespace Logger.Appenders
{
    using Contracts;
    using Enums;
    using Layouts.Contracts;

    public abstract class Appender : IAppender
    {
        protected readonly ILayout layout;

        public ReportLevel ReportLevel { get; set; }

        public int MessagesCount { get; protected set; }

        protected Appender(ILayout layout)
        {
            this.layout = layout;
        }

        public abstract void Append(string dateTime, 
            ReportLevel reportLevel, string message);
    }
}
