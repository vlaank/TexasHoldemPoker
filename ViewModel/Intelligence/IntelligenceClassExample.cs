using System;
using System.Reactive.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Input;
using ReactiveUI;

namespace ViewModelLib
{
    #region ReactiveUIHelp
    //Для лучшего понимания работы ReactiveUI разбирайся с интрфейсами:
    //  public interface IObservable<T>
    //  {
    //      IDisposable Subscribe(IObserver<T> observer);
    //  }
    //  public interface IObserver<T>
    //  {
    //      void OnNext(T value);
    //      void OnCompleted();
    //      void OnError(Exception error);
    //  }
    #endregion
    public class FullNameViewModel : ReactiveObject
    {
        private string name;
        public string Name
        {
            get { return name; }
            set { this.RaiseAndSetIfChanged(ref name, value); }
        }
        private string surname;
        public string Surname
        {
            get { return surname; }
            set { this.RaiseAndSetIfChanged(ref surname, value); }
        }
        private string fullname;
        public string Fullname
        {
            get { return fullname; }
            set { this.RaiseAndSetIfChanged(ref fullname, value); }
        }
        private bool isBusy;
        public bool IsBusy
        {
            get { return isBusy; }
            set { this.RaiseAndSetIfChanged(ref isBusy, value); }
        }

        public FullNameViewModel()
        {
            //При каждом изменении свойств Name, Surname меняем Fullname: Fullname = Name + Surname(Отслеживание по нескольким свойствам).
            this.WhenAnyValue(
                vm => vm.Name,
                vm => vm.Surname
                )
            .Subscribe(_ => { Fullname = string.Join(" ", Name, Surname); });

            //Создаем условие доступности команды.
            IObservable<bool> canExecute = this.WhenAny(
                vm => vm.Name,
                vm => vm.Surname,
                (name, surname) =>
                name.Value != null && surname.Value != null);
            //Присваиваем кнопке обработчик GetFullName, при условии canExecute.
            GetFullNameCommand = ReactiveCommand.Create(GetFullNameAsync, canExecute);
        }
        public ReactiveCommand<System.Reactive.Unit, System.Reactive.Unit> GetFullNameCommand { get; }
        private async void GetFullNameAsync()
        {
            IsBusy = true;
            await Task.Delay(5000);
            Fullname = string.Join(" ", Name, Surname, " :ReactiveCommand:");
            IsBusy = false;
        }

    }
    public class IntelligenceClassExample
    {
        public static string getInterfaceAndPlayerClassNames()
            => String.Join(
                "\n",
                "Название классов в Model:",
                InterfaceLib.InterfaceClassExample.Factory().GetType().Name,
                PlayerLib.PlayerClassExample.Factory().GetType().Name
                );
    }
}
