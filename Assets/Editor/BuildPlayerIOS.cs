#if UNITY_IOS || UNITY_IPHONE
using UnityEditor.Build;
using UnityEditor.Build.Reporting;
using UnityEngine;
using AddLocalizedStringsFile;

namespace AddLocalizedStringsFile
{
    class LocalizationBuildPlayerIOS : IPostprocessBuildWithReport
    {
        public int callbackOrder => 1;

        public void OnPostprocessBuild(BuildReport report)
        {
            var path = report.summary.outputPath;
            Player.AddLanguages(path);
            Player.AddLocalization(path, "InfoPlist.strings");
            Player.AddLocalization(path, "Localizable.strings");
        }
    }
}
#endif
