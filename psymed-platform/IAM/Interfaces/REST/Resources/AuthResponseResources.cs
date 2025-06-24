namespace psymed_platform.IAM.Interfaces.REST.Resources
{
    public class LoginResource
    {
        public string Username { get; set; }
        public string Password { get; set; }
    }

    public class RegisterResource
    {
        public string Username { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }
        public DateTime BirthDate { get; set; }
        public string Email { get; set; }
        public string Gender { get; set; }
        public string Phone { get; set; }
        public string Ubication { get; set; }
        public string Password { get; set; }
    }

    public class AuthResponseResource
    {
        public string Username { get; set; }
        public bool Success { get; set; }
        public string Token { get; set; }
        public string Error { get; set; }
    }
}