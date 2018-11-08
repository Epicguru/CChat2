using System;
using System.IO;
using System.IO.Compression;

namespace CrapChat
{
    /// <summary>
    /// Represents a file or directory that is either uploading or downloading.
    /// </summary>
    public class NetFile
    {
        public static string BaseLocation = Environment.CurrentDirectory;
        public static string LocalDownloads = Path.Combine(BaseLocation, "Downloads");
        public static string LocalUploads = Path.Combine(BaseLocation, "Uploads");
        // If true, once zipped uploads are completed the local zip file is deleted.
        public static bool DeleteZippedUploads = true;

        // The local, absolute path to the target file or folder.
        public string LocalPath;
        // True if the target file or directory is a directory. False if it is a file.
        public bool IsDirectory;
        // The local absolute path to the zip file that contains the contents of
        // the target directory. Only used if IsDirectory is true.
        public string ZipPath;
        // True if this NetFile represents a downloading file.
        public bool IsDownload;

        public FileStream Stream;

        public bool FileExists
        {
            get
            {
                return File.Exists(LocalPath);
            }
        }

        public bool DirectoryExists
        {
            get
            {
                return Directory.Exists(LocalPath);
            }
        }

        public bool IsZipped
        {
            get
            {
                return File.Exists(ZipPath);
            }
        }

        public void Zip()
        {
            Directory.CreateDirectory(LocalUploads);

            if (File.Exists(ZipPath))
            {
                // Delete the existing zip file.
                Main.Log("Deleting existing zip file.");
                File.Delete(ZipPath);
                Main.Log("Done!");
            }

            Main.Log("Started zipping from " + LocalPath + " to " + ZipPath);
            ZipFile.CreateFromDirectory(LocalPath, ZipPath, CompressionLevel.Optimal, true);
            Main.Log("Done!");
        }

        public void UnZip()
        {
            Main.Log("Started unzipping from " + ZipPath + " into " + LocalPath);
            ZipFile.ExtractToDirectory(ZipPath, LocalPath);
            Main.Log("Done");
        }
    }
}
