using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace MonkeyHubApp.ViewModels
{
    public class MainViewModel: BaseViewModel
    {
        private string _searchTerm;

        public string SearchTerm
        {
            get { return _searchTerm; }
            set
            {
                if (SetProperty(ref _searchTerm, value))
                    SearchCommand.ChangeCanExecute();
            }
        }

        public ObservableCollection<string> Resultados { get; }

        public Command SearchCommand { get; }

        public MainViewModel()
        {

            SearchCommand = new Command(ExecuteSearchCommand, CanExecuteSearchCommand);

            Resultados = new ObservableCollection<string>(new[] { "abc", "cde", "1", "2", "3", "4", "5", "6", "7", "8" });

            //Task.Delay(3000).ContinueWith(async t =>
            //{
            //    Descricao = "Meu texto mudou";

            //    for (int i = 0; i < 10; i++)
            //    {
            //        await Task.Delay(1000);
            //        Descricao = $"O texto mudou {i}";
            //    }
            //});
        }

        public async void ExecuteSearchCommand()
        {
            //Debug.WriteLine($"Clicou no botão {DateTime.Now}");
            await Task.Delay(2000);
            bool resposta = await App.Current.MainPage.DisplayAlert("MonkeyHubApp", $"Você pesquisou por '{SearchTerm}'?", "Sim", "Não");
            Resultados.Clear();

            if (resposta)
            {
                await App.Current.MainPage.DisplayAlert("MonkeyHubApp", "Obrigado", "Ok");

                for (int i = 0; i < 10; i++)
                {
                    Resultados.Add($"Sim {i}");
                }

            }
            else
            {
                await App.Current.MainPage.DisplayAlert("MonkeyHubApp", "De Nada", "Ok");
                Resultados.Add("Não");
            }

        }

        public bool CanExecuteSearchCommand()
        {
            return string.IsNullOrWhiteSpace(SearchTerm) == false;
        }
    }
}
