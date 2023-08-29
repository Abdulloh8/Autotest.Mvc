using Autotest4.Models;
using Newtonsoft.Json;

namespace Autotest4.Services
{
    public class QuestionServices
    {
        
        
        public readonly List<QuestionModel>? QuestionsUz;
        public readonly List<QuestionModel>? QuestionsKi;
        public readonly List<QuestionModel>? QuestionsRu;

        public int TicketQuestionsCount => 10;
        public int TicketsCount => QuestionsUz.Count / TicketQuestionsCount;


        public QuestionServices()
        {
            var path = Path.Combine("JsonData", "uzlotin.json");
            var path2 = Path.Combine("JsonData", "uzkiril.json");
            var path3 = Path.Combine("JsonData", "rus.json");

            var json = System.IO.File.ReadAllText(path);
            var json2 = System.IO.File.ReadAllText(path2);
            var json3 = System.IO.File.ReadAllText(path3);
            
            QuestionsUz = JsonConvert.DeserializeObject<List<QuestionModel>>(json);
            QuestionsKi = JsonConvert.DeserializeObject<List<QuestionModel>>(json2);
            QuestionsRu = JsonConvert.DeserializeObject<List<QuestionModel>>(json3);
        }
    }
}
