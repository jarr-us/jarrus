using System;
using System.Deployment.Application;
using System.Threading;
using System.Windows.Forms;

namespace Jarrus.Models
{
    public class UpdateChecker
    {
        public static bool Check()
        {
            UpdateCheckInfo info = null;

            if (ApplicationDeployment.IsNetworkDeployed)
            {
                ApplicationDeployment ad = ApplicationDeployment.CurrentDeployment;

                try
                {
                    info = ad.CheckForDetailedUpdate();
                }
                catch (DeploymentDownloadException)
                {
                    // No network connection
                    return false;
                }
                catch (InvalidDeploymentException)
                {
                    return false;
                }
                catch (InvalidOperationException)
                {
                    return false;
                }

                if (info.UpdateAvailable)
                {
                    try
                    {
                        ad.Update();
                        Application.Restart();
                        Environment.Exit(0);
                    }
                    catch (DeploymentDownloadException)
                    {
                        // No network connection
                    }

                    return false;
                }
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
