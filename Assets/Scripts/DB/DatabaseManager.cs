using UnityEngine;
using SQLite;
using System.IO;

public class DatabaseManager : MonoBehaviour
{
    public static DatabaseManager Instance;

    private SQLiteConnection db;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            InitDB();
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void InitDB()
    {
        string path = Path.Combine(Application.persistentDataPath, "game.db");
        db = new SQLiteConnection(path);

        db.CreateTable<AbilityData>();

        InsertIfNotExists("Dash");
        InsertIfNotExists("WallSlide");
        InsertIfNotExists("WallJump");
    }
    private void EnsureDatabase()
    {
        if (db == null)
        {
            InitDB();
        }
    }
    private void InsertIfNotExists(string name)
    {
        var data = db.Find<AbilityData>(name);
        if (data == null)
        {
            db.Insert(new AbilityData { Name = name, Unlocked = false });
        }
    }

    public void SaveAbility(string name, bool unlocked)
    {
        EnsureDatabase();

        db.InsertOrReplace(new AbilityData
        {
            Name = name,
            Unlocked = unlocked
        });
    }

    public bool LoadAbility(string name)
    {
        EnsureDatabase();

        var data = db.Find<AbilityData>(name);
        return data != null && data.Unlocked;
    }

    [ContextMenu("Reset All Abilities")]
    [ContextMenu("Reset All Abilities")]
    public void ResetAllAbilities()
    {
        EnsureDatabase();

        SaveAbility("Dash", false);
        SaveAbility("WallSlide", false);
        SaveAbility("WallJump", false);

        Debug.Log("All abilities reset.");
    }
}