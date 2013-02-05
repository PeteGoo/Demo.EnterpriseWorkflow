using System.ComponentModel.DataAnnotations;

namespace MagicEightBall.Models {
    public class QuestionAnswerModel {
        [Required]
        public string Answer { get; set; } 
    }
}