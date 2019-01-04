using Office;
using System.Windows.Forms;

namespace Jarrus.Display
{
    public class OfficeFormDisplay : FormDisplay<Survey>
    {
        public OfficeFormDisplay(Form form, FormControls controls) : base(form, controls) { }

        public override Survey[] FetchOptions()
        {
            var dao = new SurveyDAO();
            return dao.FetchOptions();
        }
    }
}
