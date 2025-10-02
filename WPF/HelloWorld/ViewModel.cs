using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace HelloWorld
{
    class ViewModel : BindableBase
    {
        public ViewModel() {
            ChangeMessageCommand = new DelegateCommand(
                ()=> GreetingMessage = "See ya");
        }

        private string _greetingMessage = "Hellow World";
        public string GreetingMessage {
            get => _greetingMessage;
            set => SetProperty(ref _greetingMessage, value);
                
                //{
                //if (_greetingMessage != value) {
                //    _greetingMessage = value;
                //    PropertyChanged?.Invoke(
                //        this, new PropertyChangedEventArgs(nameof(GreetingMessage)));
                //}
            //}
        }

        public DelegateCommand ChangeMessageCommand { get; }
    }
}
