#if UNITY_IOS || UNITY_IPHONE
using UnityEditor.Build;
using UnityEditor.Build.Reporting;
using UnityEngine;
using AddLocalizedStringsFile;

namespace AddLocalizedStringsFile
{
    class LocalizationBuildPlayerIOS : IPostprocessBuildWithReport
    {
        const string k_Placeholder = "<CONTROLLED BY LOCALIZATION>";
        public int callbackOrder => 1;

        public void OnPostprocessBuild(BuildReport report)
        {
            Player.AddLocalizationToXcodeProject(report.summary.outputPath);
        }
    }
}
#endif
