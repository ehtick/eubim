using Demo01.ViewModels;

namespace Demo01.Views;

public sealed partial class Demo01View
{
    public Demo01View(Demo01ViewModel viewModel)
    {
        DataContext = viewModel;
        InitializeComponent();
    }
}