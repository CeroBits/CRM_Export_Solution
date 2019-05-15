using System.Management.Automation;

namespace CRM_Export_Solution
{
    [Cmdlet(VerbsCommon.Get, "Export_Solution_BH")]
    public class CRM_Export_Solution : Cmdlet
    {
        [Parameter]
        public string SolutionCreatePath { get; set; }
        [Parameter]
        public string SolutionToExport { get; set; }
        [Parameter]
        public bool ManagedSolution { get; set; }
        [Parameter]
        public string CrmUrl { get; set; }
        [Parameter]
        public string Organization { get; set; }


        protected override void ProcessRecord()
        {
            XrmSDK.Main(SolutionCreatePath, SolutionToExport, ManagedSolution, CrmUrl, Organization);
        }

    }
}