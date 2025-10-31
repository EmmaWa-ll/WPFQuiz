namespace WPFQuiz.DataModels
{
    public class Quiz
    {

        public List<Question> Questions { get; set; }
        public string Title { get; set; }
        public Random Randomizer { get; set; }
        private List<Question> usedQuestions = new();

        public Quiz(string title = "")
        {
            Title = title;
            Questions = new List<Question>();
            Randomizer = new Random();
        }
        public Question GetRandomQuestion()
        {
            if (Questions.Count == 0)
            {
                return null;
            }
            if (usedQuestions.Count == Questions.Count)  // om alla frågor vart, slut
            {
                return null;
            }
            var aviable = Questions.Except(usedQuestions).ToList();  //hitta de frågor som ej använts 
            int index = Randomizer.Next(0, aviable.Count);
            var nextQuestion = aviable[index];

            usedQuestions.Add(nextQuestion);
            return nextQuestion;
        }

        public void AddQuestion(string statement, int correctAnswer, params string[] answers)
        {
            Question q = new Question(statement, correctAnswer, answers);
            Questions.Add(q);
        }

        public void RemoveQuestion(int index)
        {
            if (index < 0 || index >= Questions.Count)
            {
                return;
            }
;
            var q = Questions[index];
            Questions.RemoveAt(index);

            if (usedQuestions.Count > 0)
            {
                usedQuestions.RemoveAll(u => ReferenceEquals(u, q) || u == q);
            }

        }

    }
}
