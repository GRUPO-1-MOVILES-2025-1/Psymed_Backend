namespace psymed_platform.Profiles.Domain.Model.Commands;

public class CreateProfileCommand
{
    public string UserId { get; set; } = string.Empty;
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string Phone { get; set; } = string.Empty;
    public string Role { get; set; } = "Doctor";
    public string Height { get; set; } = string.Empty;
    public string Weight { get; set; } = string.Empty;
    public DateTime BirthDate { get; set; }
    public string Ubication { get; set; } = string.Empty;
}