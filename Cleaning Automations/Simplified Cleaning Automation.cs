using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;

class Program
{
    static void Main()
    {
        var lines = File.ReadAllLines("Data to Clean/Data to Automatically Clean.csv");
        var cleanedData = new List<string> { lines[0] };
        for (int i = 1; i < lines.Length; i++) cleanedData.Add(CleanLine(lines[i]));
        File.WriteAllLines("Cleaned_Data.csv", cleanedData);
        Console.WriteLine("Data cleaning completed. Cleaned data saved to Cleaned_Data.csv");
    }
    
    static string CleanLine(string line)
    {
        var fields = line.Split(',');
        return string.Join(",", fields.Select((field, i) => i == 0 ? CleanUserID(field) : i == 1 ? CleanName(field) : i == 2 ? CleanTeam(field) : i == 3 ? CleanVideoWatched(field) : i == 4 ? CleanDateWatched(field) : i == 5 ? CleanWatchTime(field) : i == 6 ? CleanFeedback(field) : field.Trim()));
    }
    
    static string CleanUserID(string userID) => string.IsNullOrWhiteSpace(userID) ? "" : userID.Trim();

    static string CleanName(string name) => string.IsNullOrWhiteSpace(name) ? "" : char.ToUpper(name.Trim()[0]) + name.Trim().Substring(1).ToLower();

    static string CleanTeam(string team) => string.IsNullOrWhiteSpace(team) ? "" : char.ToUpper(team.Trim()[0]) + team.Trim().Substring(1).ToLower();

    static string CleanVideoWatched(string video) => string.IsNullOrWhiteSpace(video) ? "" : video.Trim();
    
    static string CleanDateWatched(string date)
    {
        if (string.IsNullOrWhiteSpace(date)) return "";
        date = date.Trim();
        var formats = new[] { "yyyy-MM-dd", "yyyy/MM/dd", "dd-MM-yyyy", "MMM dd yyyy" };
        return DateTime.TryParseExact(date, formats, CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime parsedDate) ? parsedDate.ToString("yyyy-MM-dd") : date;
    }
    
    static string CleanWatchTime(string time)
    {
        if (string.IsNullOrWhiteSpace(time)) return "";
        time = time.Trim();
        if (time.EndsWith("mins", StringComparison.OrdinalIgnoreCase)) time = time.Replace("mins", "").Trim();
        if (int.TryParse(time, out int minutes)) return Math.Abs(minutes).ToString();
        var wordToNumber = new Dictionary<string, int>(StringComparer.OrdinalIgnoreCase) { {"zero", 0}, {"one", 1}, {"two", 2}, {"three", 3}, {"four", 4}, {"five", 5}, {"six", 6}, {"seven", 7}, {"eight", 8}, {"nine", 9}, {"ten", 10}, {"eleven", 11}, {"twelve", 12}, {"thirteen", 13}, {"fourteen", 14}, {"fifteen", 15}, {"sixteen", 16}, {"seventeen", 17}, {"eighteen", 18}, {"nineteen", 19}, {"twenty", 20}, {"thirty", 30}, {"forty", 40}, {"fifty", 50}, {"sixty", 60}, {"seventy", 70}, {"eighty", 80}, {"ninety", 90} };
        return wordToNumber.TryGetValue(time, out int number) ? Math.Abs(number).ToString() : "";
    }
    
    static string CleanFeedback(string feedback)
    {
        if (string.IsNullOrWhiteSpace(feedback)) return "";
        feedback = feedback.Trim();
        var feedbackMap = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase) { {"N/A", "N/A"}, {"", "N/A"}, {" ", "N/A"}, {"-", "N/A"}, {"none", "N/A"}, {"no comment", "N/A"}, {"na", "N/A"} };
        return feedbackMap.ContainsKey(feedback) ? feedbackMap[feedback] : feedback;
    }
}
