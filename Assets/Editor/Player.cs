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

        static string[] fileList = new string[] {
            "InfoPlist.strings",
            "Localizable.strings",
        };

        public static void AddLocalizationToXcodeProject(string projectDirectory)
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

                // Add directory
                var localeDir = code + ".lproj";
                var dir = Path.Combine(projectDirectory, localeDir);
                Directory.CreateDirectory(dir);

                foreach (var kInfoFile in fileList)
                {
                    var filePath = Path.Combine(dir, kInfoFile);
                    var relativePath = Path.Combine(localeDir, kInfoFile);

                    // Add file
                    Debug.Log(relativePath);
                    File.WriteAllText(filePath,
                        "/*\n" +
                        $"\t{kInfoFile} ({code})\n" +
                        $"\tThis file was auto-generated\n" +
                        $"*/\n");
                    project.AddLocaleVariantFile(kInfoFile, code, relativePath);
                }
            }

            // Close
            plistDocument.WriteToFile(plistPath);
            project.WriteToFile(pbxPath);
        }
    }
}
#endif
