using System;
using System.IO;

namespace Randomizer.Tools
{
    public static class FileFolderHelper
    {
        private static readonly string AppDataPath =
            Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);

        public static readonly string ClientFolderPath =
            Path.Combine(AppDataPath, "Randomizer");

        public static readonly string LogFolderPath =
            Path.Combine(ClientFolderPath, "Log");

        public static readonly string LogFilepath = Path.Combine(LogFolderPath,
            "App_" + DateTime.Now.ToString("YYYY_MM_DD") + ".txt");

        public static readonly string StorageFilePath =
            Path.Combine(ClientFolderPath, "Storage.rand");

        public static readonly string LastUserFilePath =
            Path.Combine(ClientFolderPath, "LastUser.rand");

        public static readonly string LastUserHistoryPath =
            Path.Combine(ClientFolderPath, "LastUserHistory.rand");

        public static void CheckAndCreateFile(string filePath)
        {
            try
            {
                FileInfo file = new FileInfo(filePath);
                if (!file.Directory.Exists)
                {
                    file.Directory.Create();
                }
                if (!file.Exists)
                {
                    file.Create().Close();
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        internal static void DeleteFile(string filePath)
        {
            try
            {
                FileInfo file = new FileInfo(filePath);
                if (file.Directory.Exists)
                {
                    file.Delete();
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
