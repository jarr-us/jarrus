using System.Windows.Forms;

namespace Jarrus.Models
{
    public class TextUpdater
    {
        delegate void SetTextCallback(Form f, Control ctrl, string text);
        delegate void SetProgressCallback(Form f, ProgressBar ctrl, double progress);
        /// <summary>
        /// Set text property of various controls
        /// </summary>
        /// <param name="form">The calling form</param>
        /// <param name="ctrl"></param>
        /// <param name="text"></param>
        public static void SetText(Form form, Control ctrl, string text)
        {
            // InvokeRequired required compares the thread ID of the 
            // calling thread to the thread ID of the creating thread. 
            // If these threads are different, it returns true. 
            if (ctrl.InvokeRequired)
            {
                SetTextCallback d = new SetTextCallback(SetText);
                form.Invoke(d, new object[] { form, ctrl, text });
            }
            else
            {
                ctrl.Text = text;
            }
        }

        public static void SetProgressBar(Form form, ProgressBar ctrl, double progress)
        {
            // InvokeRequired required compares the thread ID of the 
            // calling thread to the thread ID of the creating thread. 
            // If these threads are different, it returns true. 
            if (ctrl.InvokeRequired)
            {
                SetProgressCallback d = new SetProgressCallback(SetProgressBar);
                form.Invoke(d, new object[] { form, ctrl, progress });
            }
            else
            {
                ctrl.Value = (int)progress;
            }
        }
    }
}
