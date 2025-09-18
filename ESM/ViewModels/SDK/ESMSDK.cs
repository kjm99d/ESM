using System.Runtime.InteropServices;

namespace ESM.ViewModels.SDK
{
    
    // ------------------------------
    // DLL Import (ESMCore.dll)
    // ------------------------------
    public static class ESMSDK
    {
        const string ESMSDK_NAME = "ESMCore.dll";

        [DllImport(ESMSDK_NAME, EntryPoint = "ESMCore_Initalize", CallingConvention = CallingConvention.Cdecl)]
        public static extern int Initialize();

        [DllImport(ESMSDK_NAME, EntryPoint = "ESMCore_Finalize", CallingConvention = CallingConvention.Cdecl)]
        public static extern int Finalize();

        [DllImport(ESMSDK_NAME, EntryPoint = "ESMCore_AddSchedule", CallingConvention = CallingConvention.Cdecl)]
        public static extern int AddSchedule();

    }
}
