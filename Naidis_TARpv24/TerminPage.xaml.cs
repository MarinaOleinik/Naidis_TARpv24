namespace Naidis_TARpv24;

public partial class TerminPage : ContentPage
{
	public TerminPage()
	{
		InitializeComponent();
        // Siin me ühendame visuaalse poole (View) ja andmete poole (ViewModel)
        BindingContext = new TerminViewModel();
    }
}