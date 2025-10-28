namespace WPFQuiz.DataModels
{
    public class Question
    {
        public string Statement { get; set; }
        public string[] Answers { get; set; }
        public int CorrectAnswer { get; set; }

        public Question(string statement, int correctAnswer, string[] answers)
        {
            Statement = statement;
            Answers = answers;
            CorrectAnswer = correctAnswer;

        }
        public bool IsCorrect(int selectedAnswer)
        {
            return selectedAnswer == CorrectAnswer;
        }

    }
}
