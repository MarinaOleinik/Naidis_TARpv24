namespace Naidis_TARpv24;

public partial class TextPage : ContentPage
{
	Label lbl;
	Editor editor;
	HorizontalStackLayout hsl;
	List<string> nupud= new List<string>() { "Tagasi", "Avaleht", "Edasi" };
	VerticalStackLayout vsl;
    public TextPage() //(int i)
	{
		lbl = new Label
		{
			Text = "Pealkiri",
			FontSize = 36,
			FontFamily = "Luffio",
			TextColor = Colors.Black,
			HorizontalOptions = LayoutOptions.Center,
			FontAttributes = FontAttributes.Bold
		};
		editor = new Editor
		{
			Placeholder = "Sisesta tekst...",
			PlaceholderColor = Colors.Red,
			FontSize = 18,
			FontAttributes = FontAttributes.Italic,
            HorizontalOptions = LayoutOptions.Center,

		};
		editor.TextChanged += (sender, e) =>
		{
			lbl.Text = editor.Text;
		};
        hsl = new HorizontalStackLayout { Spacing = 20, HorizontalOptions = LayoutOptions.Center };
		for (int j = 0; j < nupud.Count; j++)
		{
			Button nupp = new Button
			{
				Text = nupud[j],
				FontSize = 28,
				FontFamily = "Luffio",
				TextColor = Colors.BlueViolet,
				BackgroundColor = Colors.LightGray,
				CornerRadius = 10,
				HeightRequest = 50,
				ZIndex = j
			};
			hsl.Add(nupp);
            nupp.Clicked += Liikumine;
        }
        Button tekst_to_sp = new Button
        {
            Text = "Räägi",
            FontSize = 28,
            FontFamily = "Luffio",
            TextColor = Colors.BlueViolet,
            BackgroundColor = Colors.LightGray,
            CornerRadius = 10,
            HeightRequest = 50
        };
		
        vsl = new VerticalStackLayout 
		{ 
			Padding = 20, 
			Spacing = 15,
			Children = { lbl, editor, hsl, tekst_to_sp },
			HorizontalOptions = LayoutOptions.Center
        };
		Content = vsl;
    }

    private void Liikumine(object? sender, EventArgs e)
    {
        Button nupp = sender as Button;
		if(nupp.ZIndex == 0)
		{
			Navigation.PopAsync();
		}
		else if (nupp.ZIndex == 1)
		{
			Navigation.PopToRootAsync();
		}
		else if (nupp.ZIndex == 2)
		{
            Navigation.PushAsync(new FigurePage());
        }	
    }
	
}