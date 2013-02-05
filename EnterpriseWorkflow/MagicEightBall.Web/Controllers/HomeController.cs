using System.ServiceModel;
using System.Web.Mvc;
using EightBallLibrary.Hosting;

namespace MagicEightBall.Controllers {
    public class HomeController : Controller {
        public ActionResult Index() {
            return View();
        }

        [HttpPost]
        public ActionResult Index(Models.EightBallQuestionModel model) {
            if (ModelState.IsValid) {
                // Start the EightBall workflow
                // The eight balls are presumed to be in web apps in IIS in location
                // mapped to their name as defined in the HomeModel class
                IEightBallContract eightBall = ChannelFactory<IEightBallContract>.CreateChannel(
                    new BasicHttpBinding(),
                    new EndpointAddress(
                        string.Format(
                            "http://localhost/HardcoreWorkflow/{0}/EightBall.svc", model.EightBallType)));

                eightBall.Ask(model.Question, model.Email);
                ViewBag.Message = "Your answer will be sent to you via email";
                
            }
            return View();   
        }

    }
}
