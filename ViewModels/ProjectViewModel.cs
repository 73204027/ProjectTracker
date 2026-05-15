class ProjectViewModel
{
    public int id_project { get; set; }
    public String name_project { get; set; } = "";
    public StateViewModel state { get; set; }
    public List<StageViewModel> stages { get; set; }
    public bool active { get; set; }
}