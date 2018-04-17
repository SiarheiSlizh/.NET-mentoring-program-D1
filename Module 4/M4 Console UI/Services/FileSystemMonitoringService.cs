using M4_Console_UI.Configuration;
using M4_Console_UI.Interfaces;
using M4_Console_UI.Resources;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace M4_Console_UI.Services
{
    public class FileSystemMonitoringService
    {
        private readonly FileSystemSettings settings;
        private readonly ILogger logger;
        private FileSystemWatcher[] watchers;

        public FileSystemMonitoringService(FileSystemSettings settings, ILogger logger)
        {
            this.settings = settings;
            this.logger = logger;

            this.ValidateSettings(settings);
        }

        public void StartWatch() => watchers.All(w => 
        {
            w.Created += OnCreated;
            w.EnableRaisingEvents = true;
            return true;
        });

        public void EndWatch() => watchers.All(w => 
        {
            w.EnableRaisingEvents = false;
            return true;
        });
        

        private void ValidateSettings(FileSystemSettings settings)
        {
            try
            {
                if (settings == null)
                {
                    logger.Error(Resource.NoSettings);
                    throw new ArgumentNullException(Resource.NoSettings);
                }

                ValidateFolders(settings.Folders);
            }
            catch (DirectoryNotFoundException exc)
            {
                logger.Error(exc.Message);
                throw;
            }
            catch (ArgumentNullException exc)
            {
                logger.Error(exc.Message);
                throw;
            }
        }

        private void ValidateFolders(FolderForListeningCollection folders)
        {
            watchers = new FileSystemWatcher[folders.Count];
            int i = 0;

            foreach (FolderForListening folder in folders)
            {
                if (!Directory.Exists(folder.Path))
                {
                    throw new DirectoryNotFoundException(string.Format(Resource.DirectoryNotFound, folder.Path));
                }
                watchers[i++] = new FileSystemWatcher(folder.Path);
            }
        }

        private void OnCreated(object sender, FileSystemEventArgs e)
        {
            var sourcePath = ((FileSystemWatcher)sender).Path;
            logger.Info(string.Format(Resource.FileCreated, e.Name, sourcePath, DateTime.Now));

            this.MoveFiles(sourcePath);
        }

        private void MoveFiles(string sourcePath)
        {
            foreach (FileSystemRule rule in settings.Rules)
            {
                if (!Directory.Exists(rule.Path))
                {
                    logger.Error(string.Format(Resource.FileMoveError, rule.Path));
                    continue;
                }

                var files = Directory.GetFiles(sourcePath);
                
                Regex regExp = new Regex(rule.Filter);
                int serial = 0;

                foreach (var file in files)
                {
                    var fileName = Path.GetFileName(file);
                    if (regExp.IsMatch(fileName))
                    {
                        if (rule.AddSerial)
                        {
                            fileName = $"{serial++}_{fileName}";
                        }
                        if (rule.AddDateTime)
                        {
                            fileName = $"{DateTime.Now.Day}_{DateTime.Now.Month}_{DateTime.Now.Year}_{fileName}";
                        }

                        try
                        {
                            File.Move(file, $"{rule.Path}\\{fileName}");
                            logger.Info(string.Format(Resource.FileMoveSuccess, fileName, rule.Path));
                        }
                        catch(IOException ex)
                        {
                            logger.Error(ex.Message);
                            throw;
                        }
                    }
                }
            }
        }
    }
}
