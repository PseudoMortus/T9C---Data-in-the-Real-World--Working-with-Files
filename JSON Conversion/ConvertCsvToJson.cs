using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

    class Program
    {
    static void Main()
        {
        var data = ReadCsv("Cleaned_Data.csv");
        WriteJson(data, "Cleaned_Data.json");
                Console.WriteLine("Conversion completed successfully!");
            }

        static List<UserData> ReadCsv(string filePath)
        {
            var data = new List<UserData>();
            string[] lines = File.ReadAllLines(filePath);

        for (int i = 1; i < lines.Length; i++)
            {
            string line = lines[i].Trim();
            if (string.IsNullOrWhiteSpace(line)) continue;

            string[] fields = line.Split(',');

            // Handle case where there are fewer than 7 fields
            var userData = new UserData
            {
                UserID = fields.Length > 0 ? int.Parse(fields[0].Trim()) : 0,
                Name = fields.Length > 1 ? fields[1].Trim() : "",
                Team = fields.Length > 2 ? fields[2].Trim() : "",
                VideoWatched = fields.Length > 3 ? fields[3].Trim() : "",
                DateWatched = fields.Length > 4 ? fields[4].Trim() : "",
                WatchTime = fields.Length > 5 && !string.IsNullOrEmpty(fields[5].Trim()) ? int.Parse(fields[5].Trim()) : (int?)null,
                Feedback = fields.Length > 6 ? fields[6].Trim() : ""
            };
            data.Add(userData);
        }
            return data;
        }
        
        static void WriteJson(List<UserData> data, string filePath)
        {
        var json = "[\n" + string.Join(",\n", data.ConvertAll(item =>
            $"  {{\n    \"UserID\": {item.UserID},\n    \"Name\": \"{item.Name}\",\n    \"Team\": \"{item.Team}\",\n    \"VideoWatched\": \"{item.VideoWatched}\",\n    \"DateWatched\": \"{item.DateWatched}\",\n    \"WatchTime\": {(item.WatchTime.HasValue ? item.WatchTime.Value.ToString() : "null")},\n    \"Feedback\": \"{item.Feedback}\"\n  }}"));
        json += "\n]";
        File.WriteAllText(filePath, json);
    }
}

    class UserData
    {
        public int UserID { get; set; }
        public string Name { get; set; }
        public string Team { get; set; }
        public string VideoWatched { get; set; }
        public string DateWatched { get; set; }
        public int? WatchTime { get; set; }
        public string Feedback { get; set; }
    }

