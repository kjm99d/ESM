using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.InteropServices;
using System.Windows.Input;
using System.Windows.Navigation;
using ESM.ViewModels.Command;
using ESM.ViewModels.SDK;

namespace ESM.ViewModels
{
    
    public class MainViewModel : INotifyPropertyChanged
    {
        // ------------------------------
        // 속성
        // ------------------------------
        public ObservableCollection<TaskItemViewModel> Tasks { get; set; } = new();

        private TaskItemViewModel? _selectedTask;
        public TaskItemViewModel? SelectedTask
        {
            get => _selectedTask;
            set { _selectedTask = value; OnPropertyChanged(nameof(SelectedTask)); }
        }

        private string _statusMessage = "Ready";
        public string StatusMessage
        {
            get => _statusMessage;
            set { _statusMessage = value; OnPropertyChanged(nameof(StatusMessage)); }
        }

        // ------------------------------
        // Commands
        // ------------------------------
        public ICommand AddScheduleCommand { get; }

        // ------------------------------
        // 생성자
        // ------------------------------
        public MainViewModel()
        {
#if false
            // 더미 데이터

            Tasks.Add(new TaskItemViewModel
            {
                Name = "Daily Backup",
                ActionPath = @"C:\backup.exe",
                Trigger = "Daily 09:00"
            });

            Tasks.Add(new TaskItemViewModel
            {
                Name = "Report",
                ActionPath = @"C:\report.exe",
                Trigger = "Once 2025-09-11 10:00"
            });
#endif

            // Command 연결
            AddScheduleCommand = new RelayCommand(AddSchedule);
        }

        // ------------------------------
        // 메서드
        // ------------------------------
        private void AddSchedule()
        {
            const int ERROR_SUCCESS = 0;
            // 실제로는 SelectedTask 속성들을 DLL API 파라미터로 넘겨야 함
            // 지금은 단순히 호출만
            int result = ESMSDK.AddSchedule();

            if (ERROR_SUCCESS == result)
                StatusMessage = $"[{SelectedTask?.Name}] 스케줄 등록 성공";
            else
                StatusMessage = $"[{SelectedTask?.Name}] 스케줄 등록 실패 (Error={result})";
        }

        // ------------------------------
        // INotifyPropertyChanged 구현
        // ------------------------------
        public event PropertyChangedEventHandler? PropertyChanged;
        protected void OnPropertyChanged(string propertyName)
            => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}
