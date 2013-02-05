using System.Activities;
using EightBallLibrary.Hosting;

namespace EightBallLibrary.Activities {
    /// <summary>
    /// Gets the URI of the current workflow
    /// </summary>
    public class GetWorkflowUri : CodeActivity<string> {
        protected override string Execute(CodeActivityContext context) {
            return context.GetExtension<WorkflowHostingEnvironmentExtension>().GetCurrentWorkflowUri().ToString();
        }
    }
}