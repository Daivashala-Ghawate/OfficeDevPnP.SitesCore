using Microsoft.SharePoint.Client;
using OfficeDevPnP.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace OfficeDevPnP.Core.Test.ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            string templateWebUrl = "https://kovag.sharepoint.com/sites/Samenwerkingsgroep_Template";
            string targetWebUrl = "https://kovag.sharepoint.com/sites/ProvisioningTarget_EN_6";
            string userName = "rens.oosterbos@kovag.onmicrosoft.com";
            string pwdS = "Spikes16";

            SecureString pwd = new SecureString();
            foreach (char c in pwdS.ToCharArray()) pwd.AppendChar(c);

            using (var ctx = new ClientContext(templateWebUrl))
            {
                // ctx.Credentials = new NetworkCredentials(userName, pwd);
                ctx.Credentials = new SharePointOnlineCredentials(userName, pwd);
                ctx.RequestTimeout = Timeout.Infinite;

                // Just to output the site details
                Web web = ctx.Web;
                ctx.Load(web, w => w.Title);
                ctx.ExecuteQueryRetry();

                var nodes = NavigationExtensions.LoadSearchNavigation(ctx.Web);
            }

            using (var ctx = new ClientContext(targetWebUrl))
            {
                // ctx.Credentials = new NetworkCredentials(userName, pwd);
                ctx.Credentials = new SharePointOnlineCredentials(userName, pwd);
                ctx.RequestTimeout = Timeout.Infinite;

                // Just to output the site details
                Web web = ctx.Web;
                ctx.Load(web, w => w.Title);
                ctx.ExecuteQueryRetry();

                var nodes = NavigationExtensions.LoadSearchNavigation(ctx.Web);
            }
        }
    }
}
