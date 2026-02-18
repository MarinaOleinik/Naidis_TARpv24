

namespace Naidis_TARpv24;

public partial class Valgusfoor : ContentPage
{
	bool SisseValja = false;
    HorizontalStackLayout hst;
    VerticalStackLayout vst;
    Label pealdis;
    List<string> sisse_välja_nuppud = new List<string> { "Sisse", "Välja" };
    public Valgusfoor()
	{
        TapGestureRecognizer tap = new TapGestureRecognizer();
        vst = new VerticalStackLayout();
        pealdis= new Label
        {
            Text="Valgusfoor",
            FontSize = 48,
            FontFamily = "Luffio",
            HorizontalOptions = LayoutOptions.Center,
            VerticalOptions = LayoutOptions.Center

        };
        vst.Add(pealdis);
        for (int i = 0; i < 3; i++)
        {
            BoxView boxView = new BoxView
            {
                Color = Colors.Gray,
                WidthRequest = 150,
                HeightRequest = 150,
                CornerRadius=50,
                HorizontalOptions = LayoutOptions.Center,
                VerticalOptions = LayoutOptions.Center
            };
            vst.Add(boxView);
            boxView.GestureRecognizers.Add(tap);
            tap.Tapped += (sender, e) =>
            {
                if (SisseValja)
                {
                    if (boxView == vst.Children[1])
                    {
                        pealdis.Text = "Punane tuli/Seisa!";
                    }
                    else if (boxView == vst.Children[2])
                    {
                        pealdis.Text = "Kollane tuli/Valmistu!";
                    }
                    else if (boxView == vst.Children[3])
                    {
                        pealdis.Text = "Roheline tuli/Sõida!";
                    }
                }
                else
                {
                    pealdis.Text = "Valgusfoor on vaja sisse panna";
                }
            };
        }

        
        hst = new HorizontalStackLayout { Padding = 20, Spacing = 15 };
        for (int i = 0; i < 2; i++)
        {
            Button nupp = new Button
            {
                Text = sisse_välja_nuppud[i],
                FontSize = 36,
                FontFamily = "Luffio",
                BackgroundColor = Colors.LightGray,
                TextColor = Colors.Black,
                CornerRadius = 10,
                HeightRequest = 60,
                ZIndex = i,

            };
            hst.Add(nupp);
            nupp.Clicked += (sender, e) =>
            {
                SisseValja = !SisseValja;
                if (SisseValja) 
                { 
                    foreach (var child in vst.Children)
                    {
                        if (child is BoxView box)
                        {
                            if (box == vst.Children[1])
                            {
                                box.Color = Colors.Red;
                            }
                            else if (box == vst.Children[2])
                            {
                                box.Color = Colors.Yellow;
                            }
                            else if (box == vst.Children[3])
                            {
                                box.Color = Colors.Green;
                            }
                        }
                    }
                }
                else { 
                    foreach (var child in vst.Children)
                    {
                        if (child is BoxView box)
                        {
                            box.Color = Colors.Gray;
                        }
                    }
                }

            };
        }
        vst.Add(hst);
        Content = vst;
    }
}