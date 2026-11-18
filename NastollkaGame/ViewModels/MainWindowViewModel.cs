using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using Microsoft.Extensions.DependencyInjection;
using NastollkaGame.DB.Repository;
using NastollkaGame.Models;

namespace NastollkaGame.ViewModels;

public partial class MainWindowViewModel : ViewModelBase
{
   private readonly IServiceProvider _serviceProvider;
   [ObservableProperty] ObservableCollection<Game> _games;
   private string _searchText;

   public string SearchText
   {
      get => _searchText;
      set
      {
         _searchText = value;
         OnPropertyChanged(nameof(SearchText));
         
         Task.Run(async ()=> await SearchDoctors());
      }
   }
   public MainWindowViewModel(IServiceProvider serviceProvider)
   {
      _serviceProvider = serviceProvider;

      using (var rep = serviceProvider.GetRequiredService<GameRepository>())
      {
         Games = new ObservableCollection<Game>(rep.GetGames());
      }
   }
   
   async Task SearchDoctors()
   {
      if (string.IsNullOrWhiteSpace(SearchText))
      {
         using (var rep = _serviceProvider.GetRequiredService<GameRepository>())
         {
            Games= new ObservableCollection<Game>(rep.GetGames());
         }
      }
      else
      {
         using (var rep = _serviceProvider.GetRequiredService<GameRepository>())
         {
            Games = new ObservableCollection<Game>(
               rep.GetGames().Where(s =>
                  s.Name.Contains(SearchText, StringComparison.CurrentCultureIgnoreCase) ||
                  s.Genre.Contains(SearchText, StringComparison.CurrentCultureIgnoreCase)));
         }
       
      }
   }
   
}