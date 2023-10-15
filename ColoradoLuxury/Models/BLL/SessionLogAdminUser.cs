namespace ColoradoLuxury.Models.BLL
{
    public class SessionLogAdminUser: AbstractEntity
    {
        public string Username { get; set; }
        public string Password { get; set; }

        public bool LoggedIn { get; set; }

    }
}
