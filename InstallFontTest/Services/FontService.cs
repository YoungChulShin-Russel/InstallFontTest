using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Drawing.Text;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;

namespace InstallFontTest.Services
{
    public static class FontService
    {
        [DllImport("gdi32.dll")]
        public static extern int AddFontResource([In][MarshalAs(UnmanagedType.LPWStr)]string lpFileName);
        
        private static string _fongRegKeyName = @"HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Windows NT\CurrentVersion\Fonts";

        private static Dictionary<string, string> _requriedFontList = new Dictionary<string, string>()
        {
            {"나눔바른고딕", "Assets\\Fonts\\NanumBarunGothic.ttf"},
            {"나눔바른고딕 Light", "Assets\\Fonts\\NanumBarunGothicLight.ttf"}
        };

        public static void CheckFontData()
        {
            foreach(KeyValuePair<string, string> fontInfo in _requriedFontList)
            {
                if (CheckFontIsInstalled(fontInfo.Key) == false)
                {
                    InstallFont(fontInfo.Key, fontInfo.Value);
                }
            }
        }

        private static bool CheckFontIsInstalled(string fontName)
        {
            var installedFonts = new InstalledFontCollection().Families;

            return installedFonts.Where(font => font.Name == fontName).Count() > 0;
        }
        
        private static void InstallFont(string fontName, string fontFilePath)
        {
            var installFontFileName = Path.GetFileName(fontFilePath);
            var installFontFilePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Fonts), installFontFileName);

            if (File.Exists(installFontFilePath) == false)
            {
                File.Copy(fontFilePath, installFontFilePath);
            }

            using (var fontCollection = new PrivateFontCollection())
            {
                fontCollection.AddFontFile(installFontFilePath);
                AddFontResource(installFontFilePath);
                Registry.SetValue(_fongRegKeyName, fontCollection.Families[0].Name, installFontFileName, RegistryValueKind.String);
            }
        }
    }
}
