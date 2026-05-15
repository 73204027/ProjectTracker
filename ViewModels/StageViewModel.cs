public class StageViewModel
{
    public int id_stage { get; set; }
    public String name_stage { get; set; } = "";
    public bool active { get; set; }
    
    public int? id_plan { get; set; }
    public int? id_project { get; set; }
    public int? id_task { get; set; }
    public int? id_subtask { get; set; }

    // validate that there are at least one FK
}