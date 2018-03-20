using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Task_1.EventsArgs;

namespace Task_1
{
    public enum Action
    {
        Continue,
        Stop,
        Skip
    }

    public enum FileSystemResourceType
    {
        Directory,
        File
    }

    public class FileSystemVisitor
    {
        #region Events
        public event EventHandler<StartEventArgs> StartNotification;
        public event EventHandler<EndEventArgs> EndNotification;
        public event EventHandler<FileFindedEventArgs> FileFindedNotification;
        public event EventHandler<DirectoryFindedEventArgs> DirectoryFindedNotification;
        public event EventHandler<FilteredFileFindedEventArgs> FilteredFileFindedNotification;
        public event EventHandler<FilteredDirectoryFindedEventArgs> FilteredDirectoryFindedNotification;
        #endregion

        #region API
        public IEnumerable<string> GetFilesAndFoldersSequence(string rootPath, Predicate<string> filter = null)
        {
            if (!Directory.Exists(rootPath))
            {
                throw new DirectoryNotFoundException($"The directory {rootPath} doesn't exist");
            }

            this.OnNotify(StartNotification, new StartEventArgs("Search was started!"));

            foreach (var folder in this.GetFilesAndFolders(rootPath, filter))
            {
                yield return folder;
            }

            this.OnNotify(EndNotification, new EndEventArgs("Search was ended!"));
        }
        #endregion

        #region private methods
        private IEnumerable<string> GetFilesAndFolders(string rootPath, Predicate<string> filter = null)
        {
            var dir = new DirectoryInfo(rootPath);
            int level = 0;

            DirectoryInfo[] subDirs = dir.GetDirectories();
            FileInfo[] subFiles = dir.GetFiles();
            
            if (filter == null)
            {
                this.OnNotify(DirectoryFindedNotification, new DirectoryFindedEventArgs("Directory was found"));
                yield return dir.Name;
            }
            else if (filter(dir.Name))
            {
                this.OnNotify(FilteredDirectoryFindedNotification, new FilteredDirectoryFindedEventArgs("Filtered directory was found"));
                yield return dir.Name;
            }

            foreach (var file in subFiles)
            {
                if (filter == null)
                {
                    this.OnNotify(FileFindedNotification, new FileFindedEventArgs("File was found"));
                    yield return file.Name;
                }
                else if (filter(file.Name))
                {
                    this.OnNotify(FilteredFileFindedNotification, new FilteredFileFindedEventArgs($"Filtered file {file.Name} was found"));
                    yield return file.Name;
                }
            }

            while (subDirs.Count() > level)
            {
                foreach (var folder in this.GetFilesAndFolders(subDirs[level++].FullName, filter))
                {
                    if (filter == null || filter(folder))
                    {
                        yield return folder;
                    }
                }
            }
        }

        //private IEnumerable<string> GetResourceName(string name, Predicate<string> filter, Action action, FileSystemResourceType type)
        //{
        //    if (filter == null)
        //    {
        //        this.OnNotify(FileFindedNotification, new FileFindedEventArgs("File was found"));
        //        yield return name;
        //    }

        //    switch (action)
        //    {
        //        case Action.Continue:
        //            break;
        //        case Action.Skip:
        //            break;
        //        case Action.Stop:
        //            yield break;
        //    } 

        //    if (filter(name))
        //    {
        //        this.OnNotify(FilteredFileFindedNotification, new FilteredFileFindedEventArgs("Filtered file was found"));
        //        yield return name;
        //    }
        //}

        private void OnNotify<TArg>(EventHandler<TArg> someEvent, TArg e) => someEvent?.Invoke(this, e);
        #endregion
    }
}
