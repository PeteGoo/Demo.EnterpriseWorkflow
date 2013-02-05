using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MagicEightBall.Models {
    public class EightBallQuestionModel {
        [Required]
        public string Question { get; set; }

        [Required]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [Required]
        public string EightBallType { get; set; }

        /// <summary>
        /// This is the list of possible Eight Balls, convention says that they will be located at web apps that
        /// map to their names (see the HomeController)
        /// </summary>
        public static readonly Dictionary<string, string> EightBallWorkflows = new Dictionary<string, string> {
            {"BasicMagicEightBall", "Basic Magic Eight Ball"},
            {"MildlyOffensiveMagicEightBall", "Mildly Offensive Eight Ball"},
            {"HumanEightBall", "Human Eight Ball"}
        };
        
    }
}