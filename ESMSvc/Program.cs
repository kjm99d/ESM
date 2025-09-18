using ESMCommon.Common;
using ESMSvc;

internal class Program
{
    private static ProcessMutex? _processMutex;

    public static void Main(string[] args)
    {
        // 단일 실행 보장
        _processMutex = new ProcessMutex(ProcessMutexName.SERVICE);
        if (!_processMutex.IsSingleInstance)
        {
            // 이미 실행 중이면 서비스 시작하지 않음
            return;
        }

        var builder = Host.CreateApplicationBuilder(args);
        builder.Services.AddWindowsService(options =>
        {
            options.ServiceName = "Easy Schedule Manager Service";
        });
        builder.Services.AddHostedService<Worker>();

        var host = builder.Build();
        host.Run();

        _processMutex.Dispose();
    }
}
