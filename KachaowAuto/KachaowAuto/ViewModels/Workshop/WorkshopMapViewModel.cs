using KachaowAuto.ViewModels.Workshop;

namespace KachaowAuto.ViewModels.Workshop
{
    public class WorkshopMapViewModel
    {
        public string? SelectedCity { get; set; }

        public List<WorkshopMapItemViewModel> Workshops { get; set; } = new();
    }
}
