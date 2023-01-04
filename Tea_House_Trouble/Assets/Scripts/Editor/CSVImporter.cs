using Codice.Client.BaseCommands.Download;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;
using UnityEditor;
using UnityEngine;

public class CSVImporter : MonoBehaviour
{
    private const string spawnTableURLLink = "https://docs.google.com/spreadsheets/d/1zctBcz0nISZpKmrzYu4GWijw0Z0QRMw8l8-87NFoJUw/edit#gid=0";

    private const string spawnTableDownloadLink = "https://docs.google.com/spreadsheets/d/e/2PACX-1vTYXMQHe8C1ppyoONnjMm0y0mj_xna_Ja8GrJEUnHtFV0BmtpIINm_Pdm1r7xZSu2SkfXuiT2HuQ3Sx/pub?output=csv";

    private static string spawnTableCSVFilePath => Path.Combine(Application.dataPath, "Spawn_Tabelle.csv");

   // [MenuItem("Links/Open SpawnTable")]
    public static void MenuOpenURL()
    {
        //Debug.Log(spawnTableCSVFilePath);
        Application.OpenURL(spawnTableURLLink);
    }

   // [MenuItem("Links/SpawnTable Download")]
    public static void MenuDownloadSpawnTable()
    {
        //Debug.Log(spawnTableCSVFilePath);
        string tableAsString = GetWWWViaLink(spawnTableDownloadLink, "SpawnTable");

        if (tableAsString.CompareTo(File.ReadAllText(spawnTableCSVFilePath)) == 0)
        {
            Debug.Log("Downloaded SpawnTable, no Differences found.");
            return;
        }
        else
        {
            if (!Directory.Exists(Path.GetDirectoryName(spawnTableCSVFilePath)))
                Directory.CreateDirectory(Path.GetDirectoryName(spawnTableCSVFilePath));

            File.WriteAllText(spawnTableCSVFilePath, tableAsString, Encoding.UTF8);

            Debug.Log("Downloaded SpawnTable, differences found, SpawnTable was updated.");
        }
    }

    /// <summary>
    /// Returns website given by link as string.
    /// </summary>
    public static string GetWWWViaLink(string link, string friendlyName)
    {
        if (string.IsNullOrWhiteSpace(link)) return "";

#pragma warning disable CS0618 // obsolete
        WWW www = new WWW(link);
#pragma warning restore CS0618

        DateTime maxWaiting = DateTime.UtcNow;
        maxWaiting = maxWaiting.AddSeconds(60);

        //wait until it's done
        while (!www.isDone)
        {
            if (DateTime.UtcNow.CompareTo(maxWaiting) > 0)
            {
                Debug.LogError($"Import\tCould not download {friendlyName} within 60 seconds, download aborted.\n{link}\n");
                return "";
            }
        }

        // worked without errors
        if (string.IsNullOrWhiteSpace(www.error)) return www.text;

        // display error
        Debug.LogError($"Import\t{friendlyName}: WWW ERROR!\n\t{www.error}\n{link}\n");
        return "";
    }
}
