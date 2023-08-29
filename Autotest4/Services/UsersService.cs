using Autotest4.Models;
using Autotest4.Repositories;

namespace Autotest4.Services;

public class UsersService
{
    private const string UserIdCookieKey = "user_id";
    public  readonly UserRepository _userRepository;
    public  readonly AnswersRepository _answersRepository;
    public  readonly NameRepositoriy _nameRepositoriy;
    public  readonly NatijaRepository _natijaRepository;
    public  readonly TicketRepository _ticketRepository;

    public UsersService(UserRepository userRepository, AnswersRepository answersRepository, NameRepositoriy nameRepositoriy,NatijaRepository natijaRepository, TicketRepository ticketRepository)
    {
        _userRepository = userRepository;
        _answersRepository = answersRepository;
        _nameRepositoriy = nameRepositoriy;
        _ticketRepository = ticketRepository;
        _natijaRepository = natijaRepository;
    }
    public  bool IsLoggedIn(HttpContext context)
    {
        if (!context.Request.Cookies.ContainsKey(UserIdCookieKey)) 
            return false;

        return true;
    }
    public  bool Key(string user)
    {
        bool key = _nameRepositoriy.Have(user);
        return key;
    }
    public  void Register(SingUpUser createUser, HttpContext httpContext)
    {
        var user = new User
        {
            Id = Guid.NewGuid().ToString(),
            Password = createUser.Password!,
            UserName = createUser.UserName!,
            PhotoPath = SavePhoto(createUser.Photo!)
        };

        _nameRepositoriy.AddName(createUser.UserName);
        _answersRepository.AddAnswers(user.Id);
        _ticketRepository.AddTicket(user.Id);
        _userRepository.AddUser(user);

        httpContext.Response.Cookies.Append(UserIdCookieKey, user.Id);
    }
    private  string SavePhoto(IFormFile file)
    {
        if (!Directory.Exists("wwwroot/UserImages"))
            Directory.CreateDirectory("wwwroot/UserImages");

        var fileName = Guid.NewGuid() + ".jpg";
        var ms = new MemoryStream();
        file.CopyTo(ms);
        System.IO.File.WriteAllBytes(Path.Combine("wwwroot", "UserImages", fileName), ms.ToArray());

        return "/UserImages/" + fileName;
    }
    public  bool LogIn(SingINUser signInUserModel, HttpContext httpContext)
    {
        var user = _userRepository.GetUser(signInUserModel.UserName);

        if (user == null || user.Password != signInUserModel.Password)
            return false;

        httpContext.Response.Cookies.Append(UserIdCookieKey, user.Id);

        return true;
    }
    public  User? GetCurrentUser(HttpContext context)
    {
        if (context.Request.Cookies.ContainsKey(UserIdCookieKey))
        {
            var userId = context.Request.Cookies[UserIdCookieKey];
            var user = _userRepository.GetUserId(userId);

            return user;
        }

        return null;
    }
    public  void Delete(HttpContext context)
    {
        if (context.Request.Cookies.ContainsKey(UserIdCookieKey))
        {
            var userId = context.Request.Cookies[UserIdCookieKey];
            var user = new User();
            user = _userRepository.GetUserId(userId);

            _answersRepository.DeleteAnsweres(userId);
            _nameRepositoriy.DeleteName(user.UserName);
            _userRepository.DeleteUserId(userId);
            _ticketRepository.Delete(userId);


            context.Response.Cookies.Delete(UserIdCookieKey);
        }
    }
    public  void UpdateUserName(HttpContext context,string userName)
    {
        var userId = context.Request.Cookies[UserIdCookieKey];
        var user = new User();

        user = _userRepository.GetUserId(userId);

        _nameRepositoriy.DeleteName(user.UserName);
        _nameRepositoriy.AddName(userName);
        
        user.UserName = userName;
        _userRepository.Update(user);
        context.Response.Cookies.Append("user_id", userId);
    }
    public  void UpdatePassword(HttpContext context, string password)
    {
        var userId = context.Request.Cookies[UserIdCookieKey];
        var user = new User();
        user = _userRepository.GetUserId(userId);
        user.Password = password;
        _userRepository.Update(user);
        context.Response.Cookies.Append("user_id", userId);
    }
    public  int GetAnswers(HttpContext context, int n)
    {
        var userId = context.Request.Cookies[UserIdCookieKey];
        
        int son = _answersRepository.number(n, userId);

        return son;
    }
    public  void Obv(HttpContext context)
    {
        var userId = context.Request.Cookies[UserIdCookieKey];
        _answersRepository.Obnovit(userId);
        _ticketRepository.Obv(userId);
    }
    public  void UpTi(HttpContext context,int id,int v)
    {
        var userId = context.Request.Cookies[UserIdCookieKey];
        _answersRepository.Update($"tisket{id}",v,userId);
    }
    public  void AddJavob(HttpContext context,int tisketid,int correctcount)
    {
        var userId = context.Request.Cookies[UserIdCookieKey];
        var natija = new Natija()
        {
            Userid = userId,
            TisketId = tisketid,
            correctCount = correctcount
        };


        _natijaRepository.AddNatija(natija);
    }
    public  List<Natija> GetNatija(HttpContext context)
    {
        var userId = context.Request.Cookies[UserIdCookieKey];

        return _natijaRepository.GetNatija(userId);
    }
    public  Ticket GetTicket(HttpContext context)
    {
        var userId = context.Request.Cookies[UserIdCookieKey];
        Ticket ticket = new Ticket();
        ticket = _ticketRepository.GetTicket(userId);
        return ticket;
    }
    public  void UpdateTicket(HttpContext context,int ticketNumber,int value)
    {
        var userId = context.Request.Cookies[UserIdCookieKey];
        _ticketRepository.Update($"ticket{ticketNumber}", value, userId);
    }
}
