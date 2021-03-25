using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToolViewYoutubePro
{
    class Program
    {
        static void Main(string[] args)
        {
            view();
        }

        static void view()
        {
            int dem = 1;
            //int numberLoop = Int32.Parse(ConfigurationManager.AppSettings["numberLoop"]);
            int numberLoop = 7;
            try
            {
                while (true)
                {
                    ToolViewYoutubePro toolViewYoutube = new ToolViewYoutubePro();
                    ToolLog.writeLog("tao moi trinh duyet");
                    for (int i = 1; i <= numberLoop; i++)
                    {
                        toolViewYoutube.gotoGoogle();
                        toolViewYoutube.gotoYoutube();
                        toolViewYoutube.openNewTab();
                        toolViewYoutube.closeNewTab(0);
                        ToolLog.writeLog("----------------------------------------------------------xem lan so: " + dem);
                        dem++;
                    }
                    toolViewYoutube.closeBrowser();
                    ToolLog.writeLog("dong trinh duyet");
                }
            }
            catch (Exception ex)
            {
                ToolLog.showError(ex);
            }
        }
    }
}
