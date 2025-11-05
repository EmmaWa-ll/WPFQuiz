using System.IO;
using System.Text.Json;
using WPFQuiz.DataModels;

namespace WPFQuiz.Files
{
    public static class QuizFile
    {
        public static string Folder => Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "WPFQuiz");

        public static void EnsureSeeded()
        {
            Directory.CreateDirectory(Folder);
            if (Directory.EnumerateFiles(Folder, "*.json").Any())
            {
                return;
            }
            string baseDir = AppContext.BaseDirectory;
            string quizzesDir = Path.Combine(baseDir, "quizzes");
            if (!Directory.Exists(quizzesDir))
            {
                return;
            }
            foreach (var src in Directory.EnumerateFiles(quizzesDir, "*.json"))
            {
                string dest = Path.Combine(Folder, Path.GetFileName(src));
                if (!File.Exists(dest))
                {
                    File.Copy(src, dest, overwrite: false);
                }
            }
        }
        public static List<string> LoadTitles()
        {
            if (!Directory.Exists(Folder))
            {
                return new List<string>();
            }

            return Directory.GetFiles(Folder, "*.json")
                .Select(Path.GetFileNameWithoutExtension)
                .OrderBy(x => x)
                .ToList();
        }

        public static async Task<Quiz?> LoadFile(string title)
        {
            Directory.CreateDirectory(Folder);
            var path = Path.Combine(Folder, title + ".json");
            if (!File.Exists(path))
            {
                return null;
            }
            var json = await File.ReadAllTextAsync(path);
            return JsonSerializer.Deserialize<Quiz>(json);
        }

        public static async Task SaveFile(Quiz quiz)
        {
            Directory.CreateDirectory(Folder);
            string safeTitle = string.Join("_", quiz.Title.Split(Path.GetInvalidFileNameChars()));
            var path = Path.Combine(Folder, safeTitle + ".json");
            var json = JsonSerializer.Serialize(quiz, new JsonSerializerOptions { WriteIndented = true });
            await File.WriteAllTextAsync(path, json);
        }

    }
}
