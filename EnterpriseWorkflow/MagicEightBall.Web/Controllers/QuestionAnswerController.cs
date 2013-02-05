using System.Web.Mvc;
using EightBallLibrary.Activities;
using MagicEightBall.Models;
using System.ServiceModel;

namespace MagicEightBall.Controllers
{
    /// <summary>
    /// This controller will allow the human eight ball to answer the question
    /// </summary>
    public class QuestionAnswerController : Controller
    {
        /// <summary>
        /// Display the answe entry form
        /// </summary>
        /// <param name="workflowName"></param>
        /// <param name="questionId"></param>
        /// <returns></returns>
        public ActionResult Answer(string workflowName, string questionId)
        {
            ViewBag.WorkflowName = workflowName;
            ViewBag.QuestionId = questionId;
            string question = HttpContext.Request.QueryString["Question"];
            ViewBag.Question = question;

            return View();
        }

        /// <summary>
        /// Process the answer, calling the correct workflow
        /// </summary>
        /// <param name="workflowName"></param>
        /// <param name="questionId"></param>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Answer(string workflowName, string questionId, QuestionAnswerModel model) {
            ViewBag.WorkflowName = workflowName;
            ViewBag.QuestionId = questionId;
            string question = HttpContext.Request.QueryString["Question"];
            ViewBag.Question = question;

            if(ModelState.IsValid) {
                // Call the Creation Endpoint on the workflow to create an instance of the appropriate 
                // EightBall workflow
                IQuestionAnswerContract answerService =
                    ChannelFactory<IQuestionAnswerContract>.CreateChannel(
                    new BasicHttpBinding(),
                    new EndpointAddress(
                        string.Format(
                            "http://localhost/HardcoreWorkflow/{0}/EightBall.svc", workflowName)));

                answerService.AnswerTheQuestion(questionId, model.Answer);
                ViewBag.Message = "Your answer has been accepted";
            }
            return View();
        }

    }
}
