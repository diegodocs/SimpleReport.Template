namespace SimpleReport.Template.Core.Config
{
    public interface IGlobalConfiguration
    {
        string ConnectionString { get; set; }
        string PathToExportExcelFiles{ get; set; }            
        int TimerIntervalInMiliSeconds { get; set; }
        int ThreadTimeOutInMiliseconds { get; set; }
    }
}