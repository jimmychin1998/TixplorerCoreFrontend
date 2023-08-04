namespace TixlorerCore.Models
{
    public static class GlobalVar
    {
        //存放全域變數或共用資訊使用

        //資料庫Entities(Context)更新使用指令：(下列註解指令複製並於Nuget主控台執行
        //Scaffold-DbContext "Data Source=.;Initial Catalog=Tixplorer;Integrated Security=True;TrustServerCertificate=True;" Microsoft.EntityFrameworkCore.SqlServer -OutputDir Models -force

        //如果需要使用ADO.NET，下方提供連線用字串
        //本機連線使用：不使用請註解掉程式碼行
        internal static string ConnectionStrBuilding = "Data Source=.;Initial Catalog=Tixplorer;Integrated Security=True";
        //遠端連線使用：不使用請註解掉程式碼行
        //public static string ConnectionStrBuilding = "Data Source=26.151.244.84;Initial Catalog=Tixplorer;User ID=VisualStudio;Password=03100808";

        //SPA專案使用本機路徑(請根據自己電腦的localhost修改)
        internal static string SPAurl = "http://localhost:5173/";
        //SPA專案使用雲端路徑(使用時請註解掉本機路徑)
        //internal static string SPAurl = "http://localhost:5173/";

        internal static string MVCurl = "https://localhost:7289/";

        //紀錄當前SPA傳入的登入資訊Session使用
        internal static string loginSession = string.Empty;

    }
}
