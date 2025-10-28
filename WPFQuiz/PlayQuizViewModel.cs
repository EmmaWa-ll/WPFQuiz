using System.ComponentModel;
using System.Runtime.CompilerServices;
using WPFQuiz.DataModels;

namespace WPFQuiz
{
    public class PlayQuizViewModel : INotifyPropertyChanged
    {
        public Quiz CurrentQuiz { get; set; }
        public Question CurrentQuestion { get; set; }
        public int CorrectAnswers { get; set; }
        public int TotalAnswered { get; set; }
        private List<Question> order = new();
        private int startQuestion = 0;
        public bool IsQuizFinished { get; set; }


        public string ScoreText
        {
            get
            {
                int percent = 0;
                if (TotalAnswered > 0)
                {
                    percent = (int)((double)CorrectAnswers / TotalAnswered * 100);
                }
                return $"Answeres: {CorrectAnswers} / {TotalAnswered} ({percent}%)";
            }
        }

        public PlayQuizViewModel(Quiz quiz)
        {
            CurrentQuiz = quiz;

            var random = new Random();
            order = quiz.Questions.OrderBy(q => random.Next()).ToList();
            startQuestion = 0;
            CurrentQuestion = order.Any() ? order[startQuestion] : null;

            CorrectAnswers = 0;
            TotalAnswered = 0;
            OnPropertyChanged(nameof(CurrentQuestion));  //Nameof är funktion som ger namnet på egennskap som en sträng
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        public void OnPropertyChanged([CallerMemberName] string name = "")
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(name));
            }
        }

        public bool AnswerQuestion(int selectedAnswer)
        {
            TotalAnswered++;
            bool isCorrect = CurrentQuestion.IsCorrect(selectedAnswer);
            if (isCorrect)
            {
                CorrectAnswers++;

            }
            CurrentQuestion = CurrentQuiz.GetRandomQuestion();
            if (CurrentQuestion == null)
            {
                IsQuizFinished = true;
            }
            OnPropertyChanged(nameof(CurrentQuestion));
            OnPropertyChanged(nameof(ScoreText));
            return isCorrect;
        }


    }
}
