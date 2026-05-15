public class PlanViewModel
{
    public int id_plan { get; set; }
    public String name_plan { get; set; } = "";
    public StateViewModel state { get; set; }
    public List<StageViewModel> stages { get; set; }
    public bool active { get; set; }
}