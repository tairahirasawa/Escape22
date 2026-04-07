using UnityEngine;
using UnityEditor;
using UnityEditor.Callbacks;
using UnityEditor.iOS.Xcode;
using System.IO;

public class PostProcessBuildScript {
    [PostProcessBuild]
    public static void OnPostprocessBuild(BuildTarget target, string path) {
        if (target == BuildTarget.iOS) {
            // Xcodeプロジェクトファイルのパス取得
            string projPath = PBXProject.GetPBXProjectPath(path);
            PBXProject proj = new PBXProject();
            proj.ReadFromFile(projPath);

            // ターゲットGUIDを、Unity2019以降の推奨メソッドで取得
            string targetGuid = proj.GetUnityMainTargetGuid();

            // UserNotifications.frameworkの追加
            proj.AddFrameworkToProject(targetGuid, "UserNotifications.framework", false);

            // Entitlementsファイルの作成
            string entitlementsFileName = "Unity-iPhone.entitlements";
            string entitlementsFilePath = Path.Combine(path, entitlementsFileName);
            PlistDocument entitlementsPlist = new PlistDocument();
            entitlementsPlist.root.SetString("aps-environment", "development"); // 本番の場合は "production"
            entitlementsPlist.WriteToFile(entitlementsFilePath);

            // EntitlementsファイルのパスをXcodeプロジェクトに設定
            proj.SetBuildProperty(targetGuid, "CODE_SIGN_ENTITLEMENTS", entitlementsFileName);

            // Info.plistの更新
            string plistPath = Path.Combine(path, "Info.plist");
            PlistDocument plist = new PlistDocument();
            plist.ReadFromFile(plistPath);

            // UIBackgroundModesにremote-notificationを追加
            PlistElementArray bgModes;
            if (plist.root.values.ContainsKey("UIBackgroundModes"))
                bgModes = plist.root["UIBackgroundModes"].AsArray();
            else
                bgModes = plist.root.CreateArray("UIBackgroundModes");

            bool alreadyAdded = false;
            foreach (var mode in bgModes.values) {
                if (mode.AsString() == "remote-notification") {
                    alreadyAdded = true;
                    break;
                }
            }
            if (!alreadyAdded) {
                bgModes.AddString("remote-notification");
            }

            // Info.plistにaps-environmentを設定
            plist.root.SetString("aps-environment", "development"); // 本番の場合は "production"
            plist.WriteToFile(plistPath);

            // 修正内容をXcodeプロジェクトファイルに書き戻す
            proj.WriteToFile(projPath);
        }
    }
}