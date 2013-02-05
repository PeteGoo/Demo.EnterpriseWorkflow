using System;
using System.Activities.Tracking;
using System.Diagnostics;

namespace EightBallLibrary.Tracking {
    /// <summary>
    /// Sample tracking participant that writes to the debug stream
    /// </summary>
    internal class EnterpriseWorkflowTrackingParticipant : TrackingParticipant {
        protected override void Track(TrackingRecord record, TimeSpan timeout) {
            Debug.WriteLine(string.Format("Workflow Id:{0} RecordType: {1} Record Details: {2}", record.InstanceId,
                                          record.GetType().Name, record.ToString()));
        }

    }
}