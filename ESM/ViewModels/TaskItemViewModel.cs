using System;
using System.ComponentModel;

namespace ESM.ViewModels
{
    public class TaskItemViewModel : INotifyPropertyChanged
    {
        private string _name = "";
        private string _actionPath = "";
        private string _args = "";
        private string _trigger = "";
        private string _state = "Ready";
        private DateTime? _nextRunTime;

        public string Name
        {
            get => _name;
            set { 
                _name = value; 
                OnPropertyChanged(nameof(Name)); 
            }
        }

        public string ActionPath
        {
            get => _actionPath;
            set { 
                _actionPath = value; 
                OnPropertyChanged(nameof(ActionPath)); 
            }
        }

        public string Args
        {
            get => _args;
            set { 
                _args = value; 
                OnPropertyChanged(nameof(Args)); 
            }
        }

        public string Trigger
        {
            get => _trigger;
            set { 
                _trigger = value; 
                OnPropertyChanged(nameof(Trigger)); 
            }
        }

        public string State
        {
            get => _state;
            set { 
                _state = value; 
                OnPropertyChanged(nameof(State)); 
            }
        }

        public DateTime? NextRunTime
        {
            get => _nextRunTime;
            set { 
                _nextRunTime = value; 
                OnPropertyChanged(nameof(NextRunTime)); 
            }
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        protected void OnPropertyChanged(string name)
            => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));

    }
}
