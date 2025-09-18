using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ESMCommon.Common
{

    /*
    static void Main()
    {
        using var processMutex = new ProcessMutex("ESM.ProcessMutex");
        if (!processMutex.IsSingleInstance)
        {
            Console.WriteLine("이미 실행 중인 인스턴스가 있습니다.");
            return;
        }

        Console.WriteLine("애플리케이션 실행 중...");
        Console.ReadLine();
    }
    */
    public static class ProcessMutexName
    {
        public const string SERVICE = "ESM.Svc";
        public const string AGENT = "ESM.Agent";
    }
    

    public class ProcessMutex : IDisposable
    {
        private readonly Mutex _mutex;
        private readonly bool _hasHandle;

        public ProcessMutex(string mutexName)
        {
            if (string.IsNullOrWhiteSpace(mutexName))
                throw new ArgumentException("뮤텍스 이름은 반드시 지정해야 합니다.", nameof(mutexName));

            try
            {
                // 지정된 이름으로 뮤텍스 생성
                _mutex = new Mutex(initiallyOwned: true, name: mutexName, createdNew: out _hasHandle);
            }
            catch (UnauthorizedAccessException)
            {
                _hasHandle = false;
            }
        }

        /// <summary>
        /// 현재 프로세스가 단일 실행 인스턴스인지 여부
        /// </summary>
        public bool IsSingleInstance => _hasHandle;

        public void Dispose()
        {
            if (_hasHandle)
            {
                _mutex.ReleaseMutex();
                _mutex.Dispose();
            }
        }
    }
}
