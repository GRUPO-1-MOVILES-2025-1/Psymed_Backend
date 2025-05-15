namespace psymed_platform.IAM.Domain.Model.Commands
{
    public class RegisterUserCommand
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
}