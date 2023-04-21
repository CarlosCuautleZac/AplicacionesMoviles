namespace RifasMAUIApp.Views;

public partial class Main : ContentPage
{
	public Main()
	{
		InitializeComponent();
		BindingContext = App.ViewModel;
	}
}