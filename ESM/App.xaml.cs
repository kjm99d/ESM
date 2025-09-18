using ESMCommon.Common;
using System.Windows;

namespace ESM
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private ProcessMutex? _processMutex;

        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            _processMutex = new ProcessMutex(ProcessMutexName.AGENT);
            if (!_processMutex.IsSingleInstance)
            {
                MessageBox.Show("이미 실행 중인 인스턴스가 있습니다.", "알림",
                    MessageBoxButton.OK, MessageBoxImage.Information);
                Shutdown();
                return;
            }
        }

        protected override void OnExit(ExitEventArgs e)
        {
            _processMutex?.Dispose();
            base.OnExit(e);
        }
    }


}
