using PendientesApp.ViewModels;

namespace PendientesApp.Views;

public partial class ActividadView : ContentPage
{
	public ActividadView()
	{
		InitializeComponent();
	}

    private void ToolbarItem_Clicked(object sender, EventArgs e)
    {
		this.Focus();
		((PendientesViewModel)this.BindingContext).GuardarCommand.Execute(null);
    }
}