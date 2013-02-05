using System.Activities.Tracking;
using System.Collections.ObjectModel;
using System.ServiceModel;
using System.ServiceModel.Activities;
using System.ServiceModel.Channels;
using System.ServiceModel.Description;

namespace EightBallLibrary.Tracking {
    internal class EnterpriseWorkflowTrackingBehavior : IServiceBehavior {
        /// <summary>
        /// Provides the ability to change run-time property values or insert custom extension objects such as error handlers, message or parameter interceptors, security extensions, and other custom extension objects.
        /// </summary>
        /// <param name="serviceDescription">The service description.</param>
        /// <param name="serviceHostBase">The host that is currently being built.</param>
        public virtual void ApplyDispatchBehavior(ServiceDescription serviceDescription, ServiceHostBase serviceHostBase) {
            WorkflowServiceHost workflowServiceHost = serviceHostBase as WorkflowServiceHost;
            if (null != workflowServiceHost) {
                workflowServiceHost.WorkflowExtensions.Add(()
                                                           => new EnterpriseWorkflowTrackingParticipant {
                                                               TrackingProfile = new TrackingProfile() {
                                                                   Name = "EnterpriseWorkflowTrackingProfile",
                                                                   Queries = {
                                                                       new WorkflowInstanceQuery {
                                                                           States = {
                                                                               WorkflowInstanceStates.Completed,
                                                                               WorkflowInstanceStates.Idle,
                                                                               WorkflowInstanceStates.Resumed,
                                                                               WorkflowInstanceStates.Unsuspended,
                                                                               WorkflowInstanceStates.UnhandledException,
                                                                               WorkflowInstanceStates.Started,
                                                                               WorkflowInstanceStates.Canceled,
                                                                               WorkflowInstanceStates.Aborted,
                                                                               WorkflowInstanceStates.Terminated,
                                                                               WorkflowInstanceStates.Suspended
                                                                           }
                                                                       },
                                                                       new ActivityStateQuery() {
                                                                           ActivityName = "*",
                                                                           States = {"*"}, 
                                                                       },
                                                                       new FaultPropagationQuery() {
                                                                           FaultHandlerActivityName = "*", 
                                                                           FaultSourceActivityName = "*" 
                                                                       }
                                                                   }
                                                               }
                                                           });
            }
        }

        /// <summary>
        /// Provides the ability to pass custom data to binding elements to support the contract implementation.
        /// </summary>
        /// <param name="serviceDescription">The service description of the service.</param>
        /// <param name="serviceHostBase">The host of the service.</param>
        /// <param name="endpoints">The service endpoints.</param>
        /// <param name="bindingParameters">Custom objects to which binding elements have access.</param>
        public void AddBindingParameters(ServiceDescription serviceDescription, ServiceHostBase serviceHostBase, Collection<ServiceEndpoint> endpoints, BindingParameterCollection bindingParameters) { }

        /// <summary>
        /// Provides the ability to inspect the service host and the service description to confirm that the service can run successfully.
        /// </summary>
        /// <param name="serviceDescription">The service description.</param>
        /// <param name="serviceHostBase">The service host that is currently being constructed.</param>
        public virtual void Validate(ServiceDescription serviceDescription, ServiceHostBase serviceHostBase) { }
    }
}