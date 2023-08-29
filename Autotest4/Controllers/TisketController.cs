using Autotest4.Models;
using Autotest4.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http;

namespace Autotest4.Controllers
{
    public class TisketController : Controller
    {
        private UsersService _usersService;
        private QuestionServices _questionServices;

        public TisketController(UsersService usersService, QuestionServices questionServices)
        {
            _usersService = usersService;
            _questionServices = questionServices;
        }

        public IActionResult Index()
        {
            var user = _usersService.GetCurrentUser(HttpContext);

            if (user == null)
                return RedirectToAction("SingIn", "User");

            Ticket ticket = new Ticket();
            ticket = _usersService.GetTicket(HttpContext);

            List<int> sanoq = new List<int>()
            {
                ticket.Ticket1,
                ticket.Ticket2,
                ticket.Ticket3,
                ticket.Ticket4,
                ticket.Ticket5,
                ticket.Ticket6,
                ticket.Ticket7,
                ticket.Ticket8,
                ticket.Ticket9,
                ticket.Ticket10,
                ticket.Ticket11,
                ticket.Ticket12,
                ticket.Ticket13,
                ticket.Ticket14,
                ticket.Ticket15,
                ticket.Ticket16,
                ticket.Ticket17,
                ticket.Ticket18,
                ticket.Ticket19,
                ticket.Ticket20,
                ticket.Ticket21,
                ticket.Ticket22,
                ticket.Ticket23,
                ticket.Ticket24,
                ticket.Ticket25,
                ticket.Ticket26,
                ticket.Ticket27,
                ticket.Ticket28,
                ticket.Ticket29,
                ticket.Ticket30,
                ticket.Ticket31,
                ticket.Ticket32,
                ticket.Ticket33,
                ticket.Ticket34,
                ticket.Ticket35,
                ticket.Ticket36,
                ticket.Ticket37,
                ticket.Ticket38,
                ticket.Ticket39,
                ticket.Ticket40,
                ticket.Ticket41,
                ticket.Ticket42,
                ticket.Ticket43,
                ticket.Ticket44,
                ticket.Ticket45,
                ticket.Ticket46,
                ticket.Ticket47,
                ticket.Ticket48,
                ticket.Ticket49,
                ticket.Ticket50,
                ticket.Ticket51,
                ticket.Ticket52,
                ticket.Ticket53,
                ticket.Ticket54,
                ticket.Ticket55,
                ticket.Ticket56,
                ticket.Ticket57,
                ticket.Ticket58,
                ticket.Ticket59,
                ticket.Ticket60,
                ticket.Ticket61,
                ticket.Ticket62,
                ticket.Ticket63,
                ticket.Ticket64,
                ticket.Ticket65,
                ticket.Ticket66,
                ticket.Ticket67,
                ticket.Ticket68,
                ticket.Ticket69,
                ticket.Ticket70
            };

            ViewBag.s = sanoq;
            

            return View();
        }
        public IActionResult StartTisket(int tisid,int queid)
        {

            HttpContext.Response.Cookies.Append("correctcount", "0");
            HttpContext.Response.Cookies.Append("tisketnumber", $"{tisid}");
            HttpContext.Response.Cookies.Append("questionnumber", $"{queid}");


            return RedirectToAction("Question");
        }
        public IActionResult Question()
        {
            string tisids = HttpContext.Request.Cookies["tisketnumber"]!;
            int tisid = int.Parse(tisids);

            string queids = HttpContext.Request.Cookies["questionnumber"]!;
            int queid = int.Parse(queids);

            int a = _usersService.GetAnswers(HttpContext, queid);
            
            HttpContext.Response.Cookies.Append("asdf", $"{a}");
            if (a == 10)
            {
                ViewBag.q = queid;
                ViewBag.t = tisid * 10 +1;
                var til = "uz";

                if (HttpContext.Request.Cookies.ContainsKey("til"))
                {
                    til = HttpContext.Request.Cookies["til"];
                }

                var question = _questionServices.QuestionsUz?.FirstOrDefault(x => x.Id == queid);

                if (til == "ki")
                {
                    question = _questionServices.QuestionsKi?.FirstOrDefault(x => x.Id == queid);
                }
                else if (til == "ru")
                {
                    question = _questionServices.QuestionsRu?.FirstOrDefault(x => x.Id == queid);
                }

                if (question == null)
                {
                    return View("NotFound");
                }
                    

                ViewBag.Question = question;


                int t = tisid * 10 + 1;
                var intlist = new List<int>();
                for (int i = 0; i <= 9; i++)
                {
                    int a1 = _usersService.GetAnswers(HttpContext, t+i);
                    intlist.Add(a1);
                }
                List<QuestionModel> models = new List<QuestionModel>();
                
                for (int i = 0; i <= 9; i++)
                {
                    var question2 = _questionServices.QuestionsRu?.FirstOrDefault(x => x.Id == t + i);
                    models.Add(question2);
                }
                
                List<bool?>? bools = new List<bool?>();

                for (int i = 0; i <= 9; i++)
                {
                    if (intlist[i] == 10)
                    {
                        bools.Add(null);
                    }
                    else
                    {
                        bools.Add(models[i].choices[intlist[i]].Answer);
                    }
                    
                }
                ViewBag.f = bools;

                return View();
            }
            

            

            return RedirectToAction("QuestionRez");
        }
        public IActionResult QuestionRez()
        {
            string ass = HttpContext.Request.Cookies["asdf"]!;
            int a = int.Parse(ass);

            string tisids = HttpContext.Request.Cookies["tisketnumber"]!;
            int tisid = int.Parse(tisids);

            string queids = HttpContext.Request.Cookies["questionnumber"]!;
            int queid = int.Parse(queids);

            if (queid % 10 == 0)
            {
                ViewBag.r = false;
            }
            else
            {
                ViewBag.r = true;
            }

            

            ViewBag.q = queid;
            ViewBag.t = tisid * 10 + 1;
            ViewBag.a = a;

            var til = "uz";

            if (HttpContext.Request.Cookies.ContainsKey("til"))
            {
                til = HttpContext.Request.Cookies["til"];
            }

            var question = _questionServices.QuestionsUz?.FirstOrDefault(x => x.Id == queid);

            if (til == "ki")
            {
                question = _questionServices.QuestionsKi?.FirstOrDefault(x => x.Id == queid);
            }
            else if (til == "ru")
            {
                question = _questionServices.QuestionsRu?.FirstOrDefault(x => x.Id == queid);
            }

            if (question == null)
                return View("NotFound");

            ViewBag.Question = question;

            int t = tisid * 10 + 1;
            var intlist = new List<int>();
            for (int i = 0; i <= 9; i++)
            {
                int a1 = _usersService.GetAnswers(HttpContext, t + i);
                intlist.Add(a1);
            }
            List<QuestionModel> models = new List<QuestionModel>();

            for (int i = 0; i <= 9; i++)
            {
                var question2 = _questionServices.QuestionsRu?.FirstOrDefault(x => x.Id == t + i);
                models.Add(question2);
            }

            List<bool?>? bools = new List<bool?>();

            for (int i = 0; i <= 9; i++)
            {
                if (intlist[i] == 10)
                {
                    bools.Add(null);
                }
                else
                {
                    bools.Add(models[i].choices[intlist[i]].Answer);
                }

            }
            ViewBag.f = bools;

            return View();
        }
        public IActionResult I(int id)
        {
            HttpContext.Response.Cookies.Append("questionnumber", $"{id}");
            return RedirectToAction("Question");
        }
        public IActionResult UpTi(int id,int count,bool c)
        {
            if (c == true)
            {
                string tisids = HttpContext.Request.Cookies["correctcount"]!;
                int cor = int.Parse(tisids);
                cor++;

                HttpContext.Response.Cookies.Append("correctcount", $"{cor}");
            }
            _usersService.UpTi(HttpContext,id, count);

            return RedirectToAction("Question");
        }
        public IActionResult Finish()
        {
            string tisids = HttpContext.Request.Cookies["tisketnumber"]!;
            int tisketnumber = int.Parse(tisids);

            string cor = HttpContext.Request.Cookies["correctcount"]!;
            int correctcount = int.Parse(cor);

            _usersService.AddJavob(HttpContext,tisketnumber,correctcount);

            _usersService.UpdateTicket(HttpContext,tisketnumber+1, correctcount);
            ViewBag.t = tisketnumber;
            ViewBag.c = correctcount;

            return View();
        }
    }
}
