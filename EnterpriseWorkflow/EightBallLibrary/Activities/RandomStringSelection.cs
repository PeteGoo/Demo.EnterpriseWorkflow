using System;
using System.Activities;
using System.Collections.Generic;
using System.Linq;

namespace EightBallLibrary.Activities {
    public class SelectRandomString : CodeActivity<string> {

        [RequiredArgument]
        public InArgument<IEnumerable<string>> Answers { get; set; }

        protected override string Execute(CodeActivityContext context) {
            IEnumerable<string> answers = Answers.Get(context).ToArray();
            if(answers.Count() == 0) {
                return string.Empty;
            }
            Random random = new Random(DateTime.Now.Millisecond);
            return answers.ElementAt(random.Next(0, answers.Count() - 1));
        }
    }
}