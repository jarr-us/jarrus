using Baseball.Models;
using System.Windows.Forms;

namespace Jarrus.Display
{
    public class BaseballFormDisplay : FormDisplay<Team>
    {
        public BaseballFormDisplay(Form form, FormControls controls) : base(form, controls){}

        public override Team[] FetchOptions()
        {
            var dao = new TeamDAO();
            return dao.FetchAllGenes().ToArray();
        }
    }
}
