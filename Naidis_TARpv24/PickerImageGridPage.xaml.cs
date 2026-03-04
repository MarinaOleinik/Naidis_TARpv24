namespace Naidis_TARpv24;

public partial class PickerImageGridPage : ContentPage
{
	Grid gr4x1, gr3x3;
	Picker picker;//pitide valik
	Image img;//piltide näitamiseks
    Switch s_pilt, s_grid;//piltide ja gridide kuvamiseks/peitmiseks
	Random rnd=new Random();//piltide juhuslikuks valimiseks
    public PickerImageGridPage()
	{
		gr4x1 = new Grid
		{
			RowDefinitions =
			{
				new RowDefinition { Height = new GridLength(1,GridUnitType.Star) },
				new RowDefinition { Height = new GridLength(3,GridUnitType.Star) },
				new RowDefinition { Height = new GridLength(3,GridUnitType.Star) },
				new RowDefinition { Height = new GridLength(1,GridUnitType.Star) }
			},
			ColumnDefinitions =
			{
				new ColumnDefinition { Width = new GridLength(1,GridUnitType.Star) },
				new ColumnDefinition { Width = new GridLength(1,GridUnitType.Star) },
			},
		};
		picker = new Picker
		{
			Title = "Vali pilt",
			ItemsSource = new List<string> { "Pilt 1", "Pilt 2", "Pilt 3" },
			HorizontalOptions = LayoutOptions.Center,
			VerticalOptions = LayoutOptions.Center
		};
        picker.SelectedIndexChanged += Piltide_valik;
		img = new Image
		{
			Source = "dotnet_bot.png",
			HorizontalOptions = LayoutOptions.Center,
		};
		s_pilt = new Switch 
		{ 
			HorizontalOptions = LayoutOptions.Center, 
			VerticalOptions = LayoutOptions.Center,
			IsToggled = true,
			IsEnabled = true
        };
		s_pilt.Toggled += (sender, e) =>
		{
			if (e.Value) 
			{ 
				img.IsVisible = true;
			}
			else
			{
				img.IsVisible = false;
            }
		};
        s_grid = new Switch
        {
            HorizontalOptions = LayoutOptions.Center,
            VerticalOptions = LayoutOptions.Center,
            IsToggled = false,
            IsEnabled = true
        };
		s_grid.Toggled += (sender, e) =>
		{			
            if (e.Value)
			{
				gr3x3=Täida_gr3x3();//kutsume välja funktsiooni, mis loob 3x3 gridi ja lisab sinna kastid, mis muudavad värvi, kui neid klõpsatakse

                gr4x1.Add(gr3x3, 0, 2);
				gr4x1.SetColumnSpan(gr3x3, 2);
            }
			else
			{
				gr4x1.RemoveAt(4);
			}
		};

		gr4x1.Add(picker, 0, 0);
		gr4x1.SetColumnSpan(picker, 2);
        gr4x1.Add(img, 0, 1);
		gr4x1.SetColumnSpan(img, 2);	
        gr4x1.Add(s_pilt, 0, 3);
		gr4x1.Add(s_grid, 1, 3);
        Content =gr4x1;
    }
	private Grid Täida_gr3x3() //3x3 grid, kus kastid vahetavad värvi, kui neid klõpsatakse
    {
        gr3x3 = new Grid();
        for (int i = 0; i < 3; i++)
        {
            gr3x3.RowDefinitions.Add(new RowDefinition { Height = new GridLength(1, GridUnitType.Star) });
            gr3x3.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) });
        }
        for (int r = 0; r < 3; r++)
        {
            for (int c = 0; c < 3; c++)
            {
                BoxView kast = new BoxView
                {
                    BackgroundColor = Color.FromRgb(rnd.Next(256), rnd.Next(256), rnd.Next(256)),
                };
                gr3x3.Add(kast, c, r);
                TapGestureRecognizer tap = new TapGestureRecognizer();
                tap.Tapped += (s, args) =>
                {
                    kast.BackgroundColor = Color.FromRgb(rnd.Next(256), rnd.Next(256), rnd.Next(256));
                };
                kast.GestureRecognizers.Add(tap);
            }
        }
		return gr3x3;
    }
    private void Piltide_valik(object? sender, EventArgs e)
    {
        if(picker.SelectedIndex == -1) return; // Kui midagi pole valitud, siis ei tee midagi
		if (picker.SelectedIndex == 0) img.Source = "pilt1.png";
		else if (picker.SelectedIndex == 1) img.Source = "pilt2.png";
		else if (picker.SelectedIndex == 2) img.Source = "pilt3.png";

    }
}