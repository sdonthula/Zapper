
using System;

namespace Zapper;

public class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("Hello, World!");

        // Assert user Id

        var userId = 1005;
        var featureId = 7;

        bool isEnabled = CheckIfTheFeatureIsEnabled("000000000", featureId);

        var fileName = $"zapper_{DateTime.UtcNow.ToString("ddMMyyyy")}.txt";
        var contentToWrite = $"{userId}-{featureId}-{isEnabled}";

        WriteToFile(fileName, contentToWrite);


        featureId = 7;
        isEnabled = CheckIfTheFeatureIsEnabled("000000010", 7);
        contentToWrite = $"{userId}-{featureId}-{isEnabled}";
        WriteToFile(fileName, contentToWrite);



        featureId = 4;
        isEnabled = CheckIfTheFeatureIsEnabled("11111111", 4);
        contentToWrite = $"{userId}-{featureId}-{isEnabled}";
        WriteToFile(fileName, contentToWrite);


        var strContentArray = ReadFromFile(fileName,userId.ToString());
        Console.ForegroundColor = ConsoleColor.Green;
       
        Console.WriteLine(string.Join("\n", strContentArray));
    }

    private static bool CheckIfTheFeatureIsEnabled(string strValue, int featureId)
    {
        return strValue[featureId] == '1';
    }

    public static void WriteToFile(string filePath, string content)
    {
        File.AppendAllText(filePath, $"{content}{Environment.NewLine}");
    }

    // Function to read text from a file
    public static List<string> ReadFromFile(string filePath, string searchKey)
    {
        List<string> result = new();
        if (File.Exists(filePath))
        {
            var matchingLines = File.ReadLines(filePath)
                                .Where(line => line.Contains(searchKey))
                                .ToList();
            result.Add($"Below the user settings for User {searchKey} {Environment.NewLine}");
            foreach (var line in matchingLines)
            {
                result.Add(ExtractSettingString(line));
            }
        }
        return result;

    }
    private static string ExtractSettingString(string line)
    {
       var settingArray = line.Split("-", StringSplitOptions.RemoveEmptyEntries);
        var settingValue = GetSettingString(settingArray[1]);
        return settingValue;
    }

    private static string GetSettingString(string settingCharacter)
    {
        var settingDescription = "";
        switch (settingCharacter)
        {
            case "1":
                settingDescription = Utilities.Notifications.SMS.ToString();
                break;
            case "2":
                settingDescription = Utilities.Notifications.Push.ToString();
                break;
            case "3":
                settingDescription = Utilities.Notifications.Bio_Metric.ToString();
                break;
            case "4":
                settingDescription = Utilities.Notifications.Camera.ToString();
                break;
            case "5":
                settingDescription = Utilities.Notifications.Location.ToString();
                break;
            case "6":
                settingDescription = Utilities.Notifications.Nfc.ToString();
                break;
            case "7":
                settingDescription = Utilities.Notifications.Vouchers.ToString();
                break;
            case "8":
                settingDescription = Utilities.Notifications.Loyalty.ToString();
                break;
            default:
                settingDescription = "";
                break;
        }
        return settingDescription;
    }

    
}
