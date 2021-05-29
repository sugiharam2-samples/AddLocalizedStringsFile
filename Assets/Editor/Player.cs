#if UNITY_IOS || UNITY_IPHONE
using System.IO;
using UnityEditor.iOS.Xcode;
using Debug = UnityEngine.Debug;
using AddLocalizedStringsFile;

namespace AddLocalizedStringsFile
{
    public static class Player
    {
        static string[] codeList = new string[] {
            "en",
            "ja",
        };

        public static void AddLanguages(string projectDirectory)
        {
            // Open project
            var pbxPath = PBXProject.GetPBXProjectPath(projectDirectory);
            var project = new PBXProject();
            project.ReadFromFile(pbxPath);
            project.ClearKnownRegions(); // Remove the deprecated regions that get added automatically.

            // Open plist
            var plistDocument = new PlistDocument();
            var plistPath = Path.Combine(projectDirectory, "Info.plist");
            plistDocument.ReadFromFile(plistPath);

            var bundleLanguages = plistDocument.root.CreateArray("CFBundleLocalizations");

            foreach (var code in codeList)
            {
                // Add code
                project.AddKnownRegion(code);
                bundleLanguages.AddString(code);
            }

            // Close
            plistDocument.WriteToFile(plistPath);
            project.WriteToFile(pbxPath);
        }

        public static void AddLocalization(string projectDirectory, string kInfoFile)
        {
            // Open project
            var pbxPath = PBXProject.GetPBXProjectPath(projectDirectory);
            var project = new PBXProject();
            project.ReadFromFile(pbxPath);

            foreach (var code in codeList)
            {
                // Add directory
                var localeDir = $"{code}.lproj";
                var dir = Path.Combine(projectDirectory, localeDir);
                Directory.CreateDirectory(dir);

                var filePath = Path.Combine(dir, kInfoFile);
                var relativePath = Path.Combine(localeDir, kInfoFile);

                // Add file
                Debug.Log(relativePath);
                File.WriteAllText(filePath, $"// {kInfoFile} ({code})");
                project.AddLocaleVariantFile(kInfoFile, code, relativePath);
            }

            // Close
            project.WriteToFile(pbxPath);
        }
    }
}
#endif
