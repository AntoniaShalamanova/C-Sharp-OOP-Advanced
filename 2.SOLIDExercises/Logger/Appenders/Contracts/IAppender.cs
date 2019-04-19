namespace Logger.Appenders.Contracts
{
    using Enums;

    public interface IAppender
    {
        ReportLevel ReportLevel { get; set; }

        void Append(string dateTime, ReportLevel reportLevel, string message);

        int MessagesCount { get; }
    }
}
