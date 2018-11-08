using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CrapChat
{
    public static class UploadThread
    {
        public static List<NetFile> CurrentUploads = new List<NetFile>();
        public static int MaxUploadSpeed = (1024 * 1024) / 10; // 0.1 MB/s
        public static int CurrentRate;
        public static float CurrentUploadRatePercentage
        {
            get
            {
                return (float)CurrentRate / MaxUploadSpeed * 100f;
            }
        }
        public static bool Run;

        public static Thread Thread;
        public static object Key = new object();
        private static int byteCounter = 0;

        public static void InitThread()
        {
            if(Thread != null)
            {
                Main.Log("Thread has already been started.");
                return;
            }

            Run = true;
            Thread = new Thread(RunUpdate);
            Thread.Start();
        }

        public static void StopThread()
        {
            if (Thread == null)
            {
                Main.Log("The thread is not running!");
                return;
            }
            Run = false;
            Thread = null;
        }

        private static void RunUpdate()
        {
            const int SLEEP_TIME = 20;
            byte[] buffer = new byte[2048]; // Just two kilobytes at a time. Might increase.
            int readBytes = 0;
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();

            while (Run)
            {
                if (CurrentUploads.Count == 0)
                {
                    Thread.Sleep(SLEEP_TIME);
                    continue;
                }
                if(stopwatch.ElapsedMilliseconds >= 1000)
                {
                    stopwatch.Restart();
                    CurrentRate = byteCounter;
                    byteCounter = 0;

                    if(CurrentRate != 0)
                    {
                        Main.Log("Uploading at " + CurrentRate + " bytes/second. " + CurrentUploadRatePercentage.ToString("N1") + "% of the max upload speed.");
                    }
                }
                if (byteCounter >= MaxUploadSpeed) // Stop sending data if we exceed the max rate per second.
                    continue;

                lock (Key)
                {
                    for (int i = 0; i < CurrentUploads.Count; i++)
                    {
                        var item = CurrentUploads[i];
                        if(item.Stream == null)
                        {
                            if(item.IsDirectory && item.IsZipped)
                            {
                                item.Stream = new FileStream(item.ZipPath, FileMode.Open);
                            }
                            else if (!item.IsDirectory)
                            {
                                item.Stream = new FileStream(item.LocalPath, FileMode.Open);
                            }
                            else
                            {
                                // It's a directory but the zip file is missing...?
                                continue;
                            }
                        }
                        else
                        {

                            // Read to fill as much of the buffer as possible.
                            readBytes = item.Stream.Read(buffer, 0, buffer.Length);

                            if (readBytes == 0)
                            {
                                // TODO: Do something.
                                Main.Log("The uploading of file " + item + " is complete! Neato.");
                                CurrentUploads.RemoveAt(i);
                                break;
                            }

                            // Give these to the client so they can be sent to the server.
                            int size = (buffer.Length + 5) * 8;
                            var msg = Net.Client.CreateMessage(size);
                            msg.Write((byte)DataType.FILE);
                            msg.Write(readBytes);
                            msg.Write(buffer);

                            Net.Client.SendMessage(msg, Lidgren.Network.NetDeliveryMethod.ReliableOrdered);
                            byteCounter += size;

                            //Main.Log("Sent " + buffer.Length + " bytes of the file to the server.");
                        }
                    }
                }
            }
        }
    }
}
