using FileCleanLib;
using System.Runtime.InteropServices;
namespace Utils
{

    public class Logger
    {
        [DllImport("CppLogger.dll",EntryPoint = "InitLogger", CallingConvention = CallingConvention.Cdecl)]
        private static extern void InitLogger_(string log_dir,string err_idr);

        [DllImport("CppLogger.dll", EntryPoint = "Log", CallingConvention = CallingConvention.Cdecl)]
        private static extern void Log_(string str);

        [DllImport("CppLogger.dll", EntryPoint = "Error", CallingConvention = CallingConvention.Cdecl)]
        private static extern void Error_(string str);

        [DllImport("CppLogger.dll",EntryPoint ="CloseLogger", CallingConvention = CallingConvention.Cdecl)]
        private static extern void CloseLogger_();

        
        public static Logger GetInstance()
        {
            return logger_;
        }

        public void SetDir(string dir,bool clean=true)
        {
            CloseLogger_();
            if (clean) {
                cleaner.StartClean();
            }
            Init(dir);
        }

        public void Log(string content)
        {
            Log_(content);
        }

        public void Error(string content)
        {
            Error_(content);
        }
        private static readonly Logger logger_ = new Logger();
        private IFileCleaner cleaner;
        private Logger()
        {
            cleaner = new FileCleaner();
            Init("./");
        }
        ~Logger()
        {
            CloseLogger_();
        }
        private void Init(string dir)
        {
            cleaner.AddFile(dir + "log.txt");
            cleaner.AddFile(dir + "err.txt");
            InitLogger_(dir + "log.txt", dir + "err.txt");
        }
    }
}
