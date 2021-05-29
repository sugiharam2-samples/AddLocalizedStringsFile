#if UNITY_IOS || UNITY_IPHONE
using System;
using System.Globalization;
using System.IO;
using System.Text;
using UnityEditor.iOS.Xcode;
using Debug = UnityEngine.Debug;

namespace AddLocalizedStringsFile
{
    public static class Player
    {
        const string kInfoFile = "InfoPlist.strings";

        public static void AddLocalizationToXcodeProject(string projectDirectory)
        {
            var pbxPath = PBXProject.GetPBXProjectPath(projectDirectory);
            var project = new PBXProject();
            project.ReadFromFile(pbxPath);
            project.ClearKnownRegions(); // Remove the deprecated regions that get added automatically.

            var plistDocument = new PlistDocument();
            var plistPath = Path.Combine(projectDirectory, "Info.plist");
            plistDocument.ReadFromFile(plistPath);

            var bundleLanguages = plistDocument.root.CreateArray("CFBundleLocalizations");

            var codeList = new string[] { "en", "ja" };
            foreach (var code in codeList)
            {
                project.AddKnownRegion(code);
                bundleLanguages.AddString(code);

                var localeDir = code + ".lproj";
                var dir = Path.Combine(projectDirectory, localeDir);
                Directory.CreateDirectory(dir);

                var filePath = Path.Combine(dir, kInfoFile);
                var relativePath = Path.Combine(localeDir, kInfoFile);

                GenerateLocalizedInfoPlistFile(plistDocument, filePath);
                project.AddLocaleVariantFile(kInfoFile, code, relativePath);
            }

            plistDocument.WriteToFile(plistPath);
            project.WriteToFile(pbxPath);
        }

        static void GenerateLocalizedInfoPlistFile(PlistDocument plistDocument, string filePath)
        {
            using (var stream = new StreamWriter(filePath, false, Encoding.UTF8))
            {
                stream.Write(
                    "/*\n" +
                    $"\t{kInfoFile}\n" +
                    $"\tThis file was auto-generated\n" +
                    $"*/\n");
            }
        }
    }
}
#endif
