﻿using System;
using System.IO;

namespace Randomizer.Tools
{
    internal static class FileFolderHelper
    {
        private static readonly string AppDataPath =
            Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);

        internal static readonly string ClientFolderPath =
            Path.Combine(AppDataPath, "Randomizer");

        internal static readonly string LogFolderPath =
            Path.Combine(ClientFolderPath, "Log");

        internal static readonly string LogFilepath = Path.Combine(LogFolderPath,
            "App_" + DateTime.Now.ToString("YYYY_MM_DD") + ".txt");

        internal static readonly string StorageFilePath =
            Path.Combine(ClientFolderPath, "Storage.rand");

        internal static readonly string LastUserFilePath =
            Path.Combine(ClientFolderPath, "LastUser.rand");

        internal static readonly string LastUserHistoryPath =
            Path.Combine(ClientFolderPath, "LastUserHistory.rand");

        internal static void CheckAndCreateFile(string filePath)
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
