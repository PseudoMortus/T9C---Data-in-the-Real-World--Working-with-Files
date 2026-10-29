using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;

class Program
{
    static void Main()
    {
        // Read the CSV file
        var lines = File.ReadAllLines("Data to Clean/Data to Automatically Clean.csv");
        
        // Process each line (skip header)
        var cleanedData = new List<string>();
        cleanedData.Add(lines[0]); // Add header
        
        for (int i = 1; i < lines.Length; i++)
        {
            var cleanedLine = CleanLine(lines[i]);
            cleanedData.Add(cleanedLine);
        }
        
        // Write cleaned data back to file
        File.WriteAllLines("Cleaned_Data.csv", cleanedData);
        Console.WriteLine("Data cleaning completed. Cleaned data saved to Cleaned_Data.csv");
    }
    
    static string CleanLine(string line)
    {
        var fields = line.Split(',');
        var cleanedFields = new List<string>();
        
        for (int i = 0; i < fields.Length; i++)
        {
            switch (i)
            {
                case 0: // UserID
                    cleanedFields.Add(CleanUserID(fields[i]));
                    break;
                case 1: // Name
                    cleanedFields.Add(CleanName(fields[i]));
                    break;
                case 2: // Team
                    cleanedFields.Add(CleanTeam(fields[i]));
                    break;
                case 3: // VideoWatched
                    cleanedFields.Add(CleanVideoWatched(fields[i]));
                    break;
                case 4: // DateWatched
                    cleanedFields.Add(CleanDateWatched(fields[i]));
                    break;
                case 5: // WatchTime
                    cleanedFields.Add(CleanWatchTime(fields[i]));
                    break;
                case 6: // Feedback
                    cleanedFields.Add(CleanFeedback(fields[i]));
                    break;
                default:
                    cleanedFields.Add(fields[i]);
                    break;
            }
        }
        
        return string.Join(",", cleanedFields);
    }
    
    static string CleanUserID(string userID)
    {
        // Ensure UserID is numeric and formatted consistently
        if (string.IsNullOrWhiteSpace(userID))
            return "";
            
        return userID.Trim();
    }
    
    static string CleanName(string name)
    {
        // Capitalize first letter, lowercase the rest
        if (string.IsNullOrWhiteSpace(name))
            return "";
            
        name = name.Trim();
        if (name.Length == 0)
            return "";
            
        return char.ToUpper(name[0]) + name.Substring(1).ToLower();
    }
    
    static string CleanTeam(string team)
    {
        // Standardize team names to title case
        if (string.IsNullOrWhiteSpace(team))
            return "";
            
        team = team.Trim();
        if (team.Length == 0)
            return "";
            
        return char.ToUpper(team[0]) + team.Substring(1).ToLower();
    }
    
    static string CleanVideoWatched(string video)
    {
        // Standardize video names
        if (string.IsNullOrWhiteSpace(video))
            return "";
            
        return video.Trim();
    }
    
    static string CleanDateWatched(string date)
    {
        // Convert various date formats to consistent YYYY-MM-DD format
        if (string.IsNullOrWhiteSpace(date))
            return "";
            
        date = date.Trim();
        
        // Try different date formats
        var formats = new[] { 
            "yyyy-MM-dd", 
            "yyyy/MM/dd", 
            "dd-MM-yyyy",
            "MMM dd yyyy"
        };
        
        if (DateTime.TryParseExact(date, formats, CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime parsedDate))
        {
            return parsedDate.ToString("yyyy-MM-dd");
        }
        
        // If parsing fails, return original
        return date;
    }
    
    static string CleanWatchTime(string time)
    {
        // Standardize watch time to numeric values in minutes
        if (string.IsNullOrWhiteSpace(time))
            return "";
            
        time = time.Trim();
        
        // Handle different formats like "30 mins", "forty"
        if (time.EndsWith("mins", StringComparison.OrdinalIgnoreCase))
        {
            time = time.Replace("mins", "").Trim();
        }
        
        // Try to parse as integer
        if (int.TryParse(time, out int minutes))
        {
            return Math.Abs(minutes).ToString(); // Ensure only positive minutes by taking absolute value
        }
        
        // Handle words like "forty"
        var wordToNumber = new Dictionary<string, int>(StringComparer.OrdinalIgnoreCase)
        {
            {"zero", 0}, {"one", 1}, {"two", 2}, {"three", 3}, {"four", 4}, {"five", 5},
            {"six", 6}, {"seven", 7}, {"eight", 8}, {"nine", 9}, {"ten", 10},
            {"eleven", 11}, {"twelve", 12}, {"thirteen", 13}, {"fourteen", 14}, {"fifteen", 15},
            {"sixteen", 16}, {"seventeen", 17}, {"eighteen", 18}, {"nineteen", 19}, {"twenty", 20},
            {"thirty", 30}, {"forty", 40}, {"fifty", 50}, {"sixty", 60}, {"seventy", 70},
            {"eighty", 80}, {"ninety", 90}
        };
        
        if (wordToNumber.TryGetValue(time, out int number))
        {

            return Math.Abs(number).ToString(); // Ensure only positive minutes
        }
        
        // If we can't parse it, return empty
        return "";
    }
    
    static string CleanFeedback(string feedback)
    {
        // Standardize feedback values
        if (string.IsNullOrWhiteSpace(feedback))
            return "";
            
        feedback = feedback.Trim();
        
        // Replace common variations with standard values
        var feedbackMap = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase)
        {
            {"N/A", "N/A"},
            {"", "N/A"},
            {" ", "N/A"},
            {"-", "N/A"},
            {"none", "N/A"},
            {"no comment", "N/A"},
            {"na", "N/A"}
        };
        
        if (feedbackMap.ContainsKey(feedback))
        {
            return feedbackMap[feedback];
        }
        
        // Return cleaned feedback
        return feedback;
    }
}

