using Baseball.Models;
using GeneticAlgorithms.BasicTypes;
using System.Windows.Forms;

namespace Jarrus.Display
{
    public class TaskRunnerDisplay : FormDisplay
    {
        public TaskRunnerDisplay(Form form, FormControls controls) : base(form, controls){}

        public override Gene[] FetchOptions()
        {
            var dao = new TeamDAO();
            return dao.FetchAllGenes().ToArray();
        }
    }
}
