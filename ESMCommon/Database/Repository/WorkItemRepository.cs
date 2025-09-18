using ESMCommon.Database.Model;
using Microsoft.Data.Sqlite;

namespace ESMCommon.Database.Repository
{
    /*
    static void Main()
    {
        WorkItemRepository.Init();

        var item = new WorkItem
        {
            Name = "메모장 실행",
            Command = "notepad.exe",
            Arguments = "",
            ScheduleTime = DateTime.Now.AddMinutes(5),
            RepeatType = "None",
            Enabled = true
        };

        int newId = WorkItemRepository.Insert(item);
        Console.WriteLine($"새 WorkItem 등록됨: Id={newId}");

        var all = WorkItemRepository.GetAll();
        foreach (var w in all)
        {
            Console.WriteLine($"[{w.Id}] {w.Name} → {w.Command} {w.Arguments}");
        }
    }
     */


    public static class WorkItemRepository
    {
        private static readonly string DbPath = Path.Combine(
            Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData),
            "ESM", "ESM.db");

        private static readonly string ConnString = $"Data Source={DbPath}";

        public static void Init()
        {
            Directory.CreateDirectory(Path.GetDirectoryName(DbPath)!);

            using var connection = new SqliteConnection(ConnString);
            connection.Open();

            var cmd = connection.CreateCommand();
            cmd.CommandText = @"
                CREATE TABLE IF NOT EXISTS WorkItems (
                    Id INTEGER PRIMARY KEY AUTOINCREMENT,
                    Name TEXT NOT NULL,
                    Command TEXT NOT NULL,
                    Arguments TEXT,
                    ScheduleTime TEXT NOT NULL,
                    RepeatType TEXT NOT NULL,
                    Enabled INTEGER NOT NULL,
                    CreatedAt TEXT NOT NULL,
                    UpdatedAt TEXT NOT NULL
                );
            ";
            cmd.ExecuteNonQuery();
        }

        public static int Insert(WorkItem item)
        {
            using var connection = new SqliteConnection(ConnString);
            connection.Open();

            var cmd = connection.CreateCommand();
            cmd.CommandText = @"
                INSERT INTO WorkItems 
                (Name, Command, Arguments, ScheduleTime, RepeatType, Enabled, CreatedAt, UpdatedAt)
                VALUES ($name, $cmd, $args, $time, $repeat, $enabled, $created, $updated);
                SELECT last_insert_rowid();
            ";
            cmd.Parameters.AddWithValue("$name", item.Name);
            cmd.Parameters.AddWithValue("$cmd", item.Command);
            cmd.Parameters.AddWithValue("$args", item.Arguments ?? "");
            cmd.Parameters.AddWithValue("$time", item.ScheduleTime.ToString("yyyy-MM-dd HH:mm:ss"));
            cmd.Parameters.AddWithValue("$repeat", item.RepeatType);
            cmd.Parameters.AddWithValue("$enabled", item.Enabled ? 1 : 0);
            cmd.Parameters.AddWithValue("$created", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
            cmd.Parameters.AddWithValue("$updated", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));

            return Convert.ToInt32(cmd.ExecuteScalar());
        }

        public static List<WorkItem> GetAll()
        {
            var list = new List<WorkItem>();
            using var connection = new SqliteConnection(ConnString);
            connection.Open();

            var cmd = connection.CreateCommand();
            cmd.CommandText = @"SELECT * FROM WorkItems ORDER BY ScheduleTime ASC;";

            using var reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                list.Add(new WorkItem
                {
                    Id = reader.GetInt32(0),
                    Name = reader.GetString(1),
                    Command = reader.GetString(2),
                    Arguments = reader.GetString(3),
                    ScheduleTime = DateTime.Parse(reader.GetString(4)),
                    RepeatType = reader.GetString(5),
                    Enabled = reader.GetInt32(6) == 1,
                    CreatedAt = DateTime.Parse(reader.GetString(7)),
                    UpdatedAt = DateTime.Parse(reader.GetString(8))
                });
            }
            return list;
        }

        public static void Update(WorkItem item)
        {
            using var connection = new SqliteConnection(ConnString);
            connection.Open();

            var cmd = connection.CreateCommand();
            cmd.CommandText = @"
                UPDATE WorkItems 
                SET Name=$name, Command=$cmd, Arguments=$args, 
                    ScheduleTime=$time, RepeatType=$repeat, Enabled=$enabled,
                    UpdatedAt=$updated
                WHERE Id=$id;
            ";
            cmd.Parameters.AddWithValue("$id", item.Id);
            cmd.Parameters.AddWithValue("$name", item.Name);
            cmd.Parameters.AddWithValue("$cmd", item.Command);
            cmd.Parameters.AddWithValue("$args", item.Arguments ?? "");
            cmd.Parameters.AddWithValue("$time", item.ScheduleTime.ToString("yyyy-MM-dd HH:mm:ss"));
            cmd.Parameters.AddWithValue("$repeat", item.RepeatType);
            cmd.Parameters.AddWithValue("$enabled", item.Enabled ? 1 : 0);
            cmd.Parameters.AddWithValue("$updated", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));

            cmd.ExecuteNonQuery();
        }

        public static void Delete(int id)
        {
            using var connection = new SqliteConnection(ConnString);
            connection.Open();

            var cmd = connection.CreateCommand();
            cmd.CommandText = @"DELETE FROM WorkItems WHERE Id=$id;";
            cmd.Parameters.AddWithValue("$id", id);

            cmd.ExecuteNonQuery();
        }
    }
}
