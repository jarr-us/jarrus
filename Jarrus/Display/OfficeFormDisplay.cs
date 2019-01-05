using GeneticAlgorithms.BasicTypes;
using Office;
using System;
using System.Windows.Forms;

namespace Jarrus.Display
{
    public class OfficeFormDisplay : FormDisplay
    {
        public OfficeFormDisplay(Form form, FormControls controls) : base(form, controls) { }

        public override Gene[] FetchOptions()
        {
            var dao = new SurveyDAO();
            var options = dao.FetchOptions();

            return options;
        }
    }
}
