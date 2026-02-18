

using System.Security.Cryptography.X509Certificates;

namespace Naidis_TARpv24;

public partial class Valgusfoor : ContentPage
{
	bool SisseValja = false;
    HorizontalStackLayout hst;
    VerticalStackLayout vst;
    Label pealdis;
    List<string> sisse_välja_nuppud = new List<string> { "Sisse", "Välja" };
    BoxView punane, kollane, roheline;
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
            if (i == 0) punane = boxView;
            else if (i == 1) kollane = boxView;
            else if (i == 2) roheline = boxView;
            vst.Add(boxView);
            
            tap.Tapped += (sender, e) =>{
                BoxView vajutatudKast = (BoxView)sender; 
                Naita_Tekst(vajutatudKast);};
            boxView.GestureRecognizers.Add(tap);
        }
        
        
        hst = new HorizontalStackLayout { Padding = 20, Spacing = 15, HorizontalOptions=LayoutOptions.Center};
        Button sisse = new Button { Text = "Sisse",
            FontFamily = "Luffio",
            FontSize = 38,
            HorizontalOptions = LayoutOptions.Center,
            VerticalOptions = LayoutOptions.Center /*...*/
        };
        sisse.Clicked += Sisse_Clicked; // Teeme eraldi meetodi puhtuse mõttes

        Button valja = new Button { Text = "Välja", 
            FontFamily = "Luffio",
            FontSize = 38,
            HorizontalOptions = LayoutOptions.Center,
            VerticalOptions = LayoutOptions.Center, /*...*/ };
        valja.Clicked += Valja_Clicked;
        hst.Add( sisse );
        hst.Add( valja );
        vst.Add(hst);
        Content = vst;
    }

    private void Valja_Clicked(object? sender, EventArgs e)
    {
        SisseValja = false;
        // Me ei pea otsima neid Children hulgast, meil on muutujad olemas!
        punane.Color = Colors.Gray;
        kollane.Color = Colors.Gray;
        roheline.Color = Colors.Gray;
        pealdis.Text = "Foor on välja lülitatud";
    }

    private void Sisse_Clicked(object? sender, EventArgs e)
    {
        SisseValja = true;
        punane.Color = Colors.Red;
        kollane.Color = Colors.Yellow;
        roheline.Color = Colors.Green;
        pealdis.Text = "Foor on sisse lülitatud";
    }

    public async void Naita_Tekst(BoxView vajutatudBox)
    {
        if (!SisseValja)
        {
            pealdis.Text = "Foor on vaja sisse panna";
            return;
        }

        // 3. Võrdleme otse muutujatega (palju selgem kui indeksid)
        if (vajutatudBox == punane)
        {
            pealdis.Text = "Punane tuli/Seisa!";
            
        }
        else if (vajutatudBox == kollane)
        {
            pealdis.Text = "Kollane tuli/Valmistu!";
        }
        else if (vajutatudBox == roheline)
        {
            pealdis.Text = "Roheline tuli/Sõida!";
        }
        await vajutatudBox.ScaleTo(1.2, 150);
        await vajutatudBox.ScaleTo(1.0, 150);

        //TeeAnimatsioon(vajutatudBox);
    }
    private async Task TeeAnimatsioon(View element)
    {
        // Suureneb ja muutub läbipaistvamaks
        await Task.WhenAll(
            element.ScaleTo(1.2, 150),
            element.FadeTo(0.5, 150)
        );

        // Taastub algsesse olekusse
        await Task.WhenAll(
            element.ScaleTo(1.0, 150),
            element.FadeTo(1.0, 150)
        );
    }
}