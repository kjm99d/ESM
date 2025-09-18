using ESMCommon.Common;
using ESMSvc;

internal class Program
{
    private static ProcessMutex? _processMutex;

    public static void Main(string[] args)
    {
        // ���� ���� ����
        _processMutex = new ProcessMutex(ProcessMutexName.SERVICE);
        if (!_processMutex.IsSingleInstance)
        {
            // �̹� ���� ���̸� ���� �������� ����
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
