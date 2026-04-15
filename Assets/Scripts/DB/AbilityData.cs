using SQLite;

public class AbilityData
{
    [PrimaryKey]
    public string Name { get; set; }

    public bool Unlocked { get; set; }
}