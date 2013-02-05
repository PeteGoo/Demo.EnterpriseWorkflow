using System.Linq;
using System.Net.Mail;
using System.Runtime.Serialization;
using System.ServiceModel.Activities;
using System.Text;

namespace EightBallLibrary.Hosting {
    /// <summary>
    /// This class provides the ability to store data (email address)alongside our workflow without putting it in arguments 
    /// and also allows us to send an email once the workflow is complete
    /// </summary>
    [DataContract]
    public class EnterpriseWorkflowCreationContext : WorkflowCreationContext {
        [DataMember]
        public string Email { get; set; }

        protected override System.IAsyncResult OnBeginWorkflowCompleted(System.Activities.ActivityInstanceState completionState, System.Collections.Generic.IDictionary<string, object> workflowOutputs, System.Exception terminationException, System.TimeSpan timeout, System.AsyncCallback callback, object state) {

            if(terminationException == null) {
                MailMessage mail = new MailMessage();
                mail.To.Add(Email);

                mail.From = new MailAddress("foo@bar.com");

                mail.Subject = "The workflow has completed";

                StringBuilder stringBuilder = new StringBuilder();
                
                stringBuilder.AppendLine("The result of the workflow was: ");
                
                workflowOutputs.ToList().ForEach(kvp => 
                    stringBuilder.AppendLine(string.Format("{0}: {1}", kvp.Key, kvp.Value))
                );
                
                mail.Body = stringBuilder.ToString();

                mail.IsBodyHtml = false;

                SmtpClient smtp = new SmtpClient();
                smtp.Send(mail);
                
            }

            return base.OnBeginWorkflowCompleted(completionState, workflowOutputs, terminationException, timeout, callback, state);
        }
        
    }
}