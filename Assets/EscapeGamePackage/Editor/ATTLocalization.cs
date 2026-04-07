using System.IO;
using UnityEditor;
using UnityEditor.Callbacks;
using UnityEditor.iOS.Xcode;
using UnityEngine;
public class ATTLocalization
{
    private static string englishAppName = "Mini Trip";
    private static string japaneseAppName = "Mini Trip";
    private static string simplifiedChineseAppName = "Mini Trip";
    private static string traditionalChineseAppName = "Mini Trip";
    private static string koreanAppName = "Mini Trip";
    private static string russianAppName = "Mini Trip";
    private static string spanishAppName = "Mini Trip";
    private static string portugueseAppName = "Mini Trip";
    private static string frenchAppName = "Mini Trip";
    private static string germanAppName = "Mini Trip";

    private static string englishATTDescription = "You Will Be More Likely To See Ads That Interest You.";
    private static string japaneseATTDescription = "興味のある広告が表示されやすくなります.";
    private static string simplifiedChineseATTDescription = "更有可能看到您感兴趣的广告。";
    private static string traditionalChineseATTDescription = "更有可能看到您感興趣的廣告。";
    private static string koreanATTDescription = "관심 있는 광고를 볼 가능성이 높아집니다.";
    private static string russianATTDescription = "Вы чаще будете видеть рекламу, которая вам интересна.";
    private static string spanishATTDescription = "Es más probable que vea anuncios que le interesen.";
    private static string portugueseATTDescription = "Você verá anúncios que podem ser do seu interesse com mais frequência.";
    private static string frenchATTDescription = "Vous verrez plus souvent des publicités susceptibles de vous intéresser.";
    private static string germanATTDescription = "Sie werden häufiger Werbung sehen, die Sie interessiert.";

    [PostProcessBuild]
    public static void OnPostProcessBuild(BuildTarget target, string pathToBuiltProject)
    {
        if (target == BuildTarget.iOS)
        {
            string plistPath = Path.Combine(pathToBuiltProject, "Info.plist");
            Debug.Log("Plist Path: " + plistPath);

            if (!File.Exists(plistPath))
            {
                Debug.LogError("Info.plist not found at: " + plistPath);
                return;
            }

            string enStringsPath = Path.Combine(pathToBuiltProject, "en.lproj", "InfoPlist.strings");
            string jaStringsPath = Path.Combine(pathToBuiltProject, "ja.lproj", "InfoPlist.strings");
            string zhHansStringsPath = Path.Combine(pathToBuiltProject, "zh-Hans.lproj", "InfoPlist.strings");
            string zhHantStringsPath = Path.Combine(pathToBuiltProject, "zh-Hant.lproj", "InfoPlist.strings");
            string koStringsPath = Path.Combine(pathToBuiltProject, "ko.lproj", "InfoPlist.strings");
            string ruStringsPath = Path.Combine(pathToBuiltProject, "ru.lproj", "InfoPlist.strings");
            string esStringsPath = Path.Combine(pathToBuiltProject, "es.lproj", "InfoPlist.strings");
            string ptStringsPath = Path.Combine(pathToBuiltProject, "pt.lproj", "InfoPlist.strings");
            string frStringsPath = Path.Combine(pathToBuiltProject, "fr.lproj", "InfoPlist.strings");
            string deStringsPath = Path.Combine(pathToBuiltProject, "de.lproj", "InfoPlist.strings");

            // Create directories if they do not exist
            Directory.CreateDirectory(Path.Combine(pathToBuiltProject, "en.lproj"));
            Directory.CreateDirectory(Path.Combine(pathToBuiltProject, "ja.lproj"));
            Directory.CreateDirectory(Path.Combine(pathToBuiltProject, "zh-Hans.lproj"));
            Directory.CreateDirectory(Path.Combine(pathToBuiltProject, "zh-Hant.lproj"));
            Directory.CreateDirectory(Path.Combine(pathToBuiltProject, "ko.lproj"));
            Directory.CreateDirectory(Path.Combine(pathToBuiltProject, "ru.lproj"));
            Directory.CreateDirectory(Path.Combine(pathToBuiltProject, "es.lproj"));
            Directory.CreateDirectory(Path.Combine(pathToBuiltProject, "pt.lproj"));
            Directory.CreateDirectory(Path.Combine(pathToBuiltProject, "fr.lproj"));
            Directory.CreateDirectory(Path.Combine(pathToBuiltProject, "de.lproj"));

            // Add localized strings for NSUserTrackingUsageDescription
            File.WriteAllText(enStringsPath, "\"NSUserTrackingUsageDescription\" = \"" + englishATTDescription + "\";");
            File.WriteAllText(jaStringsPath, "\"NSUserTrackingUsageDescription\" = \"" + japaneseATTDescription + "\";");
            File.WriteAllText(zhHansStringsPath, "\"NSUserTrackingUsageDescription\" = \"" + simplifiedChineseATTDescription + "\";");
            File.WriteAllText(zhHantStringsPath, "\"NSUserTrackingUsageDescription\" = \"" + traditionalChineseATTDescription + "\";");
            File.WriteAllText(koStringsPath, "\"NSUserTrackingUsageDescription\" = \"" + koreanATTDescription + "\";");
            File.WriteAllText(ruStringsPath, "\"NSUserTrackingUsageDescription\" = \"" + russianATTDescription + "\";");
            File.WriteAllText(esStringsPath, "\"NSUserTrackingUsageDescription\" = \"" + spanishATTDescription + "\";");
            File.WriteAllText(ptStringsPath, "\"NSUserTrackingUsageDescription\" = \"" + portugueseATTDescription + "\";");
            File.WriteAllText(frStringsPath, "\"NSUserTrackingUsageDescription\" = \"" + frenchATTDescription + "\";");
            File.WriteAllText(deStringsPath, "\"NSUserTrackingUsageDescription\" = \"" + germanATTDescription + "\";");

            // Add localized strings for CFBundleDisplayName
            File.AppendAllText(enStringsPath, "\n\"CFBundleDisplayName\" = \"" + englishAppName + "\";");
            File.AppendAllText(jaStringsPath, "\n\"CFBundleDisplayName\" = \"" + japaneseAppName + "\";");
            File.AppendAllText(zhHansStringsPath, "\n\"CFBundleDisplayName\" = \"" + simplifiedChineseAppName + "\";");
            File.AppendAllText(zhHantStringsPath, "\n\"CFBundleDisplayName\" = \"" + traditionalChineseAppName + "\";");
            File.AppendAllText(koStringsPath, "\n\"CFBundleDisplayName\" = \"" + koreanAppName + "\";");
            File.AppendAllText(ruStringsPath, "\n\"CFBundleDisplayName\" = \"" + russianAppName + "\";");
            File.AppendAllText(esStringsPath, "\n\"CFBundleDisplayName\" = \"" + spanishAppName + "\";");
            File.AppendAllText(ptStringsPath, "\n\"CFBundleDisplayName\" = \"" + portugueseAppName + "\";");
            File.AppendAllText(frStringsPath, "\n\"CFBundleDisplayName\" = \"" + frenchAppName + "\";");
            File.AppendAllText(deStringsPath, "\n\"CFBundleDisplayName\" = \"" + germanAppName + "\";");

            Debug.Log("Localized strings written.");

            // Modify the Info.plist file to include the tracking usage description and CFBundleDisplayName
            PlistDocument plist = new PlistDocument();
            plist.ReadFromFile(plistPath);
            PlistElementDict rootDict = plist.root;
            rootDict.SetString("NSUserTrackingUsageDescription", englishATTDescription);
            rootDict.SetString("CFBundleDisplayName", englishAppName); // Default display name in English
            plist.WriteToFile(plistPath);

            Debug.Log("Info.plist updated.");

            // Add the localized files to the Xcode project
            string projPath = PBXProject.GetPBXProjectPath(pathToBuiltProject);
            PBXProject proj = new PBXProject();
            proj.ReadFromFile(projPath);

            string targetGUID = proj.GetUnityMainTargetGuid();

            // Add localization files to the Xcode project
            proj.AddFile(enStringsPath, "en.lproj/InfoPlist.strings", PBXSourceTree.Source);
            proj.AddFile(jaStringsPath, "ja.lproj/InfoPlist.strings", PBXSourceTree.Source);
            proj.AddFile(zhHansStringsPath, "zh-Hans.lproj/InfoPlist.strings", PBXSourceTree.Source);
            proj.AddFile(zhHantStringsPath, "zh-Hant.lproj/InfoPlist.strings", PBXSourceTree.Source);
            proj.AddFile(koStringsPath, "ko.lproj/InfoPlist.strings", PBXSourceTree.Source);
            proj.AddFile(ruStringsPath, "ru.lproj/InfoPlist.strings", PBXSourceTree.Source);
            proj.AddFile(esStringsPath, "es.lproj/InfoPlist.strings", PBXSourceTree.Source);
            proj.AddFile(ptStringsPath, "pt.lproj/InfoPlist.strings", PBXSourceTree.Source);
            proj.AddFile(frStringsPath, "fr.lproj/InfoPlist.strings", PBXSourceTree.Source);
            proj.AddFile(deStringsPath, "de.lproj/InfoPlist.strings", PBXSourceTree.Source);

            proj.AddFileToBuild(targetGUID, proj.FindFileGuidByProjectPath("en.lproj/InfoPlist.strings"));
            proj.AddFileToBuild(targetGUID, proj.FindFileGuidByProjectPath("ja.lproj/InfoPlist.strings"));
            proj.AddFileToBuild(targetGUID, proj.FindFileGuidByProjectPath("zh-Hans.lproj/InfoPlist.strings"));
            proj.AddFileToBuild(targetGUID, proj.FindFileGuidByProjectPath("zh-Hant.lproj/InfoPlist.strings"));
            proj.AddFileToBuild(targetGUID, proj.FindFileGuidByProjectPath("ko.lproj/InfoPlist.strings"));
            proj.AddFileToBuild(targetGUID, proj.FindFileGuidByProjectPath("ru.lproj/InfoPlist.strings"));
            proj.AddFileToBuild(targetGUID, proj.FindFileGuidByProjectPath("es.lproj/InfoPlist.strings"));
            proj.AddFileToBuild(targetGUID, proj.FindFileGuidByProjectPath("pt.lproj/InfoPlist.strings"));
            proj.AddFileToBuild(targetGUID, proj.FindFileGuidByProjectPath("fr.lproj/InfoPlist.strings"));
            proj.AddFileToBuild(targetGUID, proj.FindFileGuidByProjectPath("de.lproj/InfoPlist.strings"));

            proj.WriteToFile(projPath);

            Debug.Log("Localization files added to Xcode project.");
        }
    }
}
