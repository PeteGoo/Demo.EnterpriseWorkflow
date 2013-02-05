using System;
using System.Activities.Hosting;
using System.Collections.Generic;
using System.Web.Hosting;

namespace EightBallLibrary.Hosting {
    /// <summary>
    /// Provides access to information about the Hosting Environment
    /// </summary>
    public class WorkflowHostingEnvironmentExtension : IWorkflowInstanceExtension {
        private WorkflowInstanceProxy host;

        /// <summary>
        /// When implemented, returns any additional extensions the implementing class requires.
        /// </summary>
        /// <returns>
        /// A collection of additional workflow extensions.
        /// </returns>
        public IEnumerable<object> GetAdditionalExtensions() {
            return null;
        }

        /// <summary>
        /// Sets the specified target <see cref="T:System.Activities.Hosting.WorkflowInstanceProxy"/>.
        /// </summary>
        /// <param name="instance">The target workflow instance to set.</param>
        public void SetInstance(WorkflowInstanceProxy instance) {
            this.host = instance;
        }

        /// <summary>
        /// Gets the current workflow's URI.
        /// </summary>
        /// <returns></returns>
        public Uri GetCurrentWorkflowUri() {
            return HostingEnvironment.ApplicationVirtualPath == null
                       ? null
                       : new Uri(HostingEnvironment.ApplicationVirtualPath, UriKind.Relative);
        }
    }
}