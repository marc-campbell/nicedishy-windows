using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace NiceDishyCore
{
    class WebRequester
    {
        public enum ReqType
        {
            download,
            upload
        }

        public ReqType reqType;
        public int reqIndex;
        public int reqSize;
        public string reqURL;
        public DateTime startTime;

        static int pieceCount = 20;
        int pieceIndex;
        double[] pieceSpeed;
        long byteAdded;

        WebClient webClient;

        bool isStopped;

        public WebRequester(ReqType rt, int sz, string url)
        {
            reqType = rt;
            reqSize = sz;
            reqURL = url;

            webClient = new WebClient();

        }
        public double averageSpeed
        {
            get
            {
                double sum = 0;
                double cnt = 0;
                foreach (int ps in pieceSpeed)
                {
                    if (ps < 0)
                        continue;
                    sum += ps;
                    cnt += 1;
                }
                if (cnt > 0)
                {
                    return sum / cnt;
                }
                return 0;
            }
        }

        public bool isCompleted
        {
            get
            {
                return !webClient.IsBusy;
            }
        }

        public void Start()
        {
            isStopped = false;

            pieceIndex = 0;
            pieceSpeed = Enumerable.Repeat(-1.0, pieceCount).ToArray();

            startTime = DateTime.Now;
            byteAdded = 0;

            if (reqType == ReqType.download)
            {
                Download();
            }
            else
            {
                Upload();
            }
        }
        public void Stop()
        {
            isStopped = true;

            webClient.CancelAsync();
        }

        void Download()
        {
            webClient.DownloadDataCompleted += new DownloadDataCompletedEventHandler(DownloadCompleted);
            webClient.DownloadProgressChanged += new DownloadProgressChangedEventHandler(DownloadProgress);
            webClient.DownloadDataAsync(new Uri(reqURL));
        }

        void Upload()
        {
            webClient.UploadDataCompleted += new UploadDataCompletedEventHandler(UploadCompleted);
            webClient.UploadProgressChanged += new UploadProgressChangedEventHandler(UploadProgress);

            byte[] randomData = new byte[reqSize];
            Random rnd = new Random();
            rnd.NextBytes(randomData);

            webClient.UploadDataAsync(new Uri(reqURL), randomData);
        }

        #region
        void DownloadCompleted(object sender, DownloadDataCompletedEventArgs e)
        {
            System.Diagnostics.Debug.WriteLine("{0} - Download Completed with speed ({1} kbps)", reqIndex, averageSpeed / 1024);
        }
        void DownloadProgress(object sender, DownloadProgressChangedEventArgs e)
        {
            if (isStopped)
            {
                return;
            }

            if (e.ProgressPercentage % 5 == 0)
                AddPieceSpeed(e.BytesReceived);
        }
        void UploadCompleted(object sender, UploadDataCompletedEventArgs e)
        {
            System.Diagnostics.Debug.WriteLine("{0} - Upload Completed with speed ({1} kbps)", reqIndex, averageSpeed / 1024);
        }
        void UploadProgress(object sender, UploadProgressChangedEventArgs e)
        {
            if (isStopped)
            {
                return;
            }

            if (e.ProgressPercentage % 2 == 0)
                AddPieceSpeed(e.BytesSent);
        }
        #endregion

        void AddPieceSpeed(long bytes)
        {
            DateTime curTm = DateTime.Now;
            double delta = (curTm - startTime).TotalSeconds;
            double dSize = (double)bytes;
            double speed = dSize * 8 / delta;
            pieceSpeed[pieceIndex] = speed;
            pieceIndex = (pieceIndex + 1) % pieceCount;
            startTime = curTm;
        }
    }
}
