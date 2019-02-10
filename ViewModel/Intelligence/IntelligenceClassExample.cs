using System;
using System.Threading;
using System.Threading.Tasks;
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
        private readonly PlayerLib.ReactiveExample example;

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

        public FullNameViewModel() : this(new PlayerLib.ReactiveExample()) { }

        public FullNameViewModel(PlayerLib.ReactiveExample _example)
        {
            example = _example;
            //При каждом изменении свойства Name переводит Name в верхний регистр(Отслеживание по изменению одного свойства).
            this.ObservableForProperty(
                vm => vm.Name)
                .Subscribe(_ => nameToUpper());

            //При каждом изменении свойств Name, Surname меняем Fullname: Fullname = Name + Surname(Отслеживание по нескольким свойствам).
            this.WhenAnyValue(
                vm => vm.Name,
                vm => vm.Surname
                )
            .Subscribe(_ => { Fullname = string.Join(" ", Name, Surname); });

            #region Error
            ////!!!!!При нажатии на кнопку генерируется исключение!!!!!!
            ////Можно скачать предлагаемый RxApp.cs и увидеть что генерирует исключение.
            ////Создаем условие, при котором кнопка становится доступной.
            //IObservable<bool> canExecute = this.WhenAny(
            //    vm => vm.Name,
            //    vm => vm.Surname,
            //    (name, surname) =>
            //    name.Value != null && surname.Value != null);
            ////Присваиваем кнопке обработчик GetFullName, при условии canExecute.
            //GetFullNameCommand = ReactiveCommand.Create(GetFullName, canExecute);
            #endregion

        }
        public ReactiveCommand<System.Reactive.Unit, System.Reactive.Unit> GetFullNameCommand { get; }

        private void GetFullName()
        {
            IsBusy = true;
            Fullname = string.Join(" ", Name, Surname);//example.GetFullName();
            IsBusy = false;
        }

        private void nameToUpper()
            => Name = Name.ToUpper();

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
