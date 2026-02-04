namespace KachaowAuto.ViewModels
{
    public class AdminUserViewModel
    {
        public int UserId { get; set; }
        public string Email { get; set; } = null!;
        public string FullName { get; set; } = null!;
        public string CurrentRole { get; set; } = "";
        public List<string> AllRoles { get; set; } = new();
    }
}
