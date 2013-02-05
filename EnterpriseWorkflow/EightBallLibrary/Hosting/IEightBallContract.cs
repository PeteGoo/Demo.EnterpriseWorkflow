using System;
using System.Collections.Generic;
using System.ServiceModel;

namespace EightBallLibrary.Hosting {
    /// <summary>
    /// The contract for the <see cref="EnterpriseWorkflowCreationEndpoint"/> that clients will use
    /// </summary>
    [ServiceContract(Name = "IEightBallContract")]
    public interface IEightBallContract {
        [OperationContract]
        Guid Ask(string question, string email);
    }
}