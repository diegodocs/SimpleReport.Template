namespace SimpleReport.Template.Core.Config
{

    public class GlobalConfiguration : IGlobalConfiguration
    {
        public string ConnectionString { get; set; }
        public string PathToExportExcelFiles { get; set; }
        public int ThreadTimeOutInMiliseconds { get; set; }
        public int TimerIntervalInMiliSeconds { get; set; }
    }
}