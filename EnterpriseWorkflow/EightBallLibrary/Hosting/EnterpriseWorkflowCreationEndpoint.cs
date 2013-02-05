using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.ServiceModel.Activities;
using System.ServiceModel.Channels;

namespace EightBallLibrary.Hosting {
    /// <summary>
    /// A service endpoint that clients can call to create new instances of our workflow
    /// </summary>
    internal class EnterpriseWorkflowCreationEndpoint : WorkflowHostingEndpoint  {
        public EnterpriseWorkflowCreationEndpoint(Binding binding, EndpointAddress endpointAddress)
            : base(typeof(IEightBallContract), binding, endpointAddress) {}

        protected override WorkflowCreationContext OnGetCreationContext(object[] inputs, OperationContext operationContext, Guid instanceId, WorkflowHostingResponseContext responseContext) {

            if(!operationContext.IncomingMessageHeaders.Action.EndsWith("Ask")) {
                throw new InvalidOperationException();
            }

            EnterpriseWorkflowCreationContext workflowCreationContext = new EnterpriseWorkflowCreationContext();

            string question = inputs[0] as string;
            string email = inputs[1] as string;

            workflowCreationContext.WorkflowArguments.Add("Question", question);
            workflowCreationContext.Email = email;

            responseContext.SendResponse(instanceId, null);

            return workflowCreationContext;
        }
    }
}