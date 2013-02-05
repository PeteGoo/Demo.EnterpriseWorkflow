using System;
using System.Activities;
using System.Net.Mail;

namespace EightBallLibrary.Activities {
    public class SendEmail : AsyncCodeActivity {
        [RequiredArgument]
        public InArgument<string> ToAddress { get; set; }

        [RequiredArgument]
        public InArgument<string> FromAddress { get; set; }

        [RequiredArgument]
        public InArgument<string> Subject { get; set; }

        [RequiredArgument]
        public InArgument<string> Body { get; set; }

        protected override IAsyncResult BeginExecute(AsyncCodeActivityContext context, AsyncCallback callback, object state) {
            MailMessage mail = new MailMessage();
            mail.To.Add(ToAddress.Get(context));

            mail.From = new MailAddress(FromAddress.Get(context));

            mail.Subject = Subject.Get(context);


            mail.Body = Body.Get(context);

            mail.IsBodyHtml = true;
            SmtpClient smtp = new SmtpClient();



            Action action = () => smtp.Send(mail);

            context.UserState = action;

            return action.BeginInvoke(callback, state);
        }

        protected override void EndExecute(AsyncCodeActivityContext context, IAsyncResult result) {
            Action action = context.UserState as Action;
            action.EndInvoke(result);
        }


    }

}