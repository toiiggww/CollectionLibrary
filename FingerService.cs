using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace TEArts.Etc.CollectionLibrary
{

    public class FingerService
    {
        private static FingerService mbrInstance;

        public int PrefixIndex
        {
            get;
            set;
        }

        public double PrefixPercent
        {
            get;
            set;
        }

        public int PostfixIndex
        {
            get;
            set;
        }

        public double PostfixPercent
        {
            get;
            set;
        }

        public static FingerService Instance
        {
            get
            {
                if (mbrInstance == null)
                {
                    mbrInstance = new FingerService();
                }
                return mbrInstance;
            }
        }

        public ByteFinger Result(string path)
        {
            if (File.Exists(path))
            {
                return this.Result(new FileInfo(path));
            }
            throw new FileNotFoundException();
        }

        public ByteFinger Result(FileInfo fileInfo)
        {
            ByteFinger result;
            using (FileStream fs = new FileStream(fileInfo.FullName, FileMode.Open, FileAccess.Read))
            {
                bool canRead = fs.CanRead;
                if (!canRead)
                {
                    throw new FileCantAccessedException(fileInfo.FullName);
                }
                ByteFinger finger = ByteFinger.New();
                long i = fileInfo.Length;
                if (i <= 8L)
                {
                    fs.Read(finger.Header, 0, (int)fileInfo.Length);
                    finger.Postion = FailPostion.Header;
                    result = finger;
                }
                else
                {
                    fs.Read(finger.Header, 0, 8);
                    if (i >= 24L)
                    {
                        fs.Position = 20L;
                        fs.Read(finger.PerfixIndex, 0, 4);
                        fs.Position = (long)((double)i * this.PrefixPercent);
                        fs.Read(finger.PerfixPercent, 0, 4);
                        if (i >= 84L)
                        {
                            fs.Position = 80L;
                            fs.Read(finger.PostfixIndex, 0, 4);
                            fs.Position = (long)((double)i * this.PostfixPercent);
                            fs.Read(finger.PostPercent, 0, 4);
                            fs.Position = i - 8L;
                            fs.Read(finger.Tailer, 0, 8);
                            finger.Postion = FailPostion.Successed;
                            result = finger;
                        }
                        else
                        {
                            fs.Position = i - 4L;
                            fs.Read(finger.PostfixIndex, 0, 4);
                            finger.Postion = FailPostion.PostfixIndex;
                            result = finger;
                        }
                    }
                    else
                    {
                        fs.Position = i - 4L;
                        fs.Read(finger.PerfixIndex, 0, 4);
                        finger.Postion = FailPostion.PostfixIndex;
                        result = finger;
                    }
                }
            }
            return result;
        }

        public FingerService()
        {
            this.PrefixIndex = 20;
            this.PrefixPercent = 0.2;
            this.PostfixIndex = 80;
            this.PostfixPercent = 0.8;
        }
        public class FileCantAccessedException : Exception
        {
            public string File
            {
                get;
                set;
            }

            public FileCantAccessedException(string file)
            {
                this.File = file;
            }
        }
    }
}
