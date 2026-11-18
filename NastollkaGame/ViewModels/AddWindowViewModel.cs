using System;

namespace NastollkaGame.ViewModels;

public partial class AddWindowViewModel:ViewModelBase
{
    private readonly IServiceProvider _serviceProvider;

    public AddWindowViewModel(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }
    
    
}