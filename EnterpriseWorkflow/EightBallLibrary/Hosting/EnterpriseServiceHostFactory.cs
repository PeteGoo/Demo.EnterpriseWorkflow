using System;
using System.Linq;
using System.ServiceModel;
using System.ServiceModel.Activities;
using System.ServiceModel.Activities.Activation;
using System.ServiceModel.Activities.Description;
using System.ServiceModel.Description;
using EightBallLibrary.Tracking;

namespace EightBallLibrary.Hosting {
    /// <summary>
    /// Creates an instance of a WorkflowServiceHost, preconfigured for our enterprise workflow scenario
    /// </summary>
    public class EnterpriseServiceHostFactory : WorkflowServiceHostFactory {

        public override ServiceHostBase CreateServiceHost(string constructorString, Uri[] baseAddresses) {
            ServiceHostBase workflowServiceHost = base.CreateServiceHost(constructorString, baseAddresses);
            AddBehaviorsAndEndpoints(workflowServiceHost as WorkflowServiceHost);
            return workflowServiceHost;
        }

        protected override WorkflowServiceHost CreateWorkflowServiceHost(WorkflowService service, Uri[] baseAddresses) {
            WorkflowServiceHost workflowServiceHost = base.CreateWorkflowServiceHost(service, baseAddresses);
            AddBehaviorsAndEndpoints(workflowServiceHost);
            return workflowServiceHost;
        }

        protected override WorkflowServiceHost CreateWorkflowServiceHost(System.Activities.Activity activity, Uri[] baseAddresses) {
            WorkflowServiceHost workflowServiceHost = base.CreateWorkflowServiceHost(activity, baseAddresses);
            AddBehaviorsAndEndpoints(workflowServiceHost);
            return workflowServiceHost;
        }

        /// <summary>
        /// Adds the standard behaviors and endpoints to our workflow service host.
        /// </summary>
        /// <param name="workflowServiceHost">The workflow service host.</param>
        private void AddBehaviorsAndEndpoints(WorkflowServiceHost workflowServiceHost) {
            // Check whether we have already initialised the service host
            if (workflowServiceHost.Description.Endpoints.Where(endpoint => endpoint is EnterpriseWorkflowCreationEndpoint).Any()) {
                return;
            }

            // Add endpoints for any services that have been defined in the workflow
            workflowServiceHost.AddDefaultEndpoints();

            ServiceEndpoint firstEndpoint = (from endpoint in workflowServiceHost.Description.Endpoints
                                 where endpoint.IsSystemEndpoint == false
                                 select endpoint).FirstOrDefault();

            BasicHttpBinding binding = new BasicHttpBinding();
            EndpointAddress endpointAddress = new EndpointAddress(workflowServiceHost.BaseAddresses[0]);

            // Add the creation endpoint
            EnterpriseWorkflowCreationEndpoint creationEndpoint = new EnterpriseWorkflowCreationEndpoint(firstEndpoint != null ? firstEndpoint.Binding : binding, firstEndpoint != null ? firstEndpoint.Address : endpointAddress);

            workflowServiceHost.AddServiceEndpoint(creationEndpoint);


            // Add the SQL workflow instance store
            //SqlWorkflowInstanceStore store = new SqlWorkflowInstanceStore("myConnectionString");
            //workflowServiceHost.DurableInstancingOptions.InstanceStore = store;

            // Add the idle behavior
            workflowServiceHost.Description.Behaviors.RemoveAll<WorkflowIdleBehavior>();
            WorkflowIdleBehavior idleBehavior = new WorkflowIdleBehavior {
                TimeToPersist = TimeSpan.FromMilliseconds(10000),
                TimeToUnload = TimeSpan.FromMilliseconds(10000)
            };
            workflowServiceHost.Description.Behaviors.Add(idleBehavior);

            // Add the unhandled exception behavior
            WorkflowUnhandledExceptionBehavior unhandledExceptionBehavior = new WorkflowUnhandledExceptionBehavior {
                Action = WorkflowUnhandledExceptionAction.AbandonAndSuspend
            };
            workflowServiceHost.Description.Behaviors.Add(unhandledExceptionBehavior);

            // Add tracking behavior
            EnterpriseWorkflowTrackingBehavior trackingBehavior = new EnterpriseWorkflowTrackingBehavior();
            workflowServiceHost.Description.Behaviors.Add(trackingBehavior);

            // Add a custom extension
            workflowServiceHost.WorkflowExtensions.Add(() => new WorkflowHostingEnvironmentExtension());

        }


    }
}