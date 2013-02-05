using System.ServiceModel;

namespace EightBallLibrary.Activities {
    /// <summary>
    /// A service contract that allows the user to answer a question
    /// </summary>
    [ServiceContract(Namespace = "http://teched2011.com/MagicEightBall", Name = "IQuestionAnswerContract", ConfigurationName = "IQuestionAnswerContract")]
    public interface IQuestionAnswerContract {
        /// <summary>
        /// Sets the answer
        /// </summary>
        /// <param name="questionId">The question id.</param>
        /// <param name="answer">The answer.</param>
        [OperationContract(Action = "http://teched2011.com/MagicEightBall/IQuestionAnswerContract/AnswerTheQuestion", ReplyAction = "http://teched2011.com/MagicEightBall/IQuestionAnswerContract/AnswerTheQuestionResponse")]
        void AnswerTheQuestion(string questionId, string answer);
    }
}