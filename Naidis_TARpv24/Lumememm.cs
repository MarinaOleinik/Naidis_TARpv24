using Microsoft.Maui.Layouts;

namespace Naidis_TARpv24;

public class Lumememm : ContentPage
{
    // Kuulutame muutujad klassi tasemel, et kõik funktsioonid pääseksid neile ligi
    AbsoluteLayout lumememmeAla;
    BoxView amber;
    Frame pea, keha, alakeha;
    Picker tegevusPicker;
    Label infoLabel;
    Slider lapiPaistvusSlider;
    Stepper kiirusStepper;
    Button teostaNupp;
    Switch ooreziimLuliti; // Boonus!
    Label kiirusLabel;

    Random rnd = new Random();

    public Lumememm()
    {
        Title = "Lumememm";
        BackgroundColor = Colors.LightBlue; // Päevane taust

        // 1. LOOME LUMEMEMME OSAD
        amber = new BoxView { Color = Colors.DarkGray };
        pea = new Frame { BackgroundColor = Colors.White, CornerRadius = 40, HasShadow = false, BorderColor = Colors.LightGray };
        keha = new Frame { BackgroundColor = Colors.White, CornerRadius = 60, HasShadow = false, BorderColor = Colors.LightGray };
        alakeha = new Frame { BackgroundColor = Colors.White, CornerRadius = 80, HasShadow = false, BorderColor = Colors.LightGray };

        // 2. ABSOLUTELAYOUT - PAIGUTAME OSAD TÄPSELT
        lumememmeAla = new AbsoluteLayout { HeightRequest = 400 };

        // Ämber (X on 0.5 ehk keskel, Y on 10 pikslit ülevalt)
        AbsoluteLayout.SetLayoutFlags(amber, AbsoluteLayoutFlags.XProportional);
        AbsoluteLayout.SetLayoutBounds(amber, new Rect(0.5, 20, 60, 50));

        // Pea
        AbsoluteLayout.SetLayoutFlags(pea, AbsoluteLayoutFlags.XProportional);
        AbsoluteLayout.SetLayoutBounds(pea, new Rect(0.5, 60, 80, 80));

        // Keha
        AbsoluteLayout.SetLayoutFlags(keha, AbsoluteLayoutFlags.XProportional);
        AbsoluteLayout.SetLayoutBounds(keha, new Rect(0.5, 130, 120, 120));

        // Alakeha
        AbsoluteLayout.SetLayoutFlags(alakeha, AbsoluteLayoutFlags.XProportional);
        AbsoluteLayout.SetLayoutBounds(alakeha, new Rect(0.5, 230, 160, 160));

        // Lisame osad alale (järjekord on oluline - viimane joonistatakse kõige peale!)
        lumememmeAla.Children.Add(alakeha);
        lumememmeAla.Children.Add(keha);
        lumememmeAla.Children.Add(pea);
        lumememmeAla.Children.Add(amber);

        // 3. KASUTAJALIIDESE ELEMENDID (JUHTIMINE)
        infoLabel = new Label
        {
            Text = "Vali tegevus ja vajuta nuppu!",
            FontSize = 18,
            HorizontalOptions = LayoutOptions.Center,
            FontAttributes = FontAttributes.Bold
        };

        tegevusPicker = new Picker { Title = "Vali tegevus..." };
        tegevusPicker.Items.Add("Peida lumememm");
        tegevusPicker.Items.Add("Näita lumememm");
        tegevusPicker.Items.Add("Muuda värvi");
        tegevusPicker.Items.Add("Sulata");
        tegevusPicker.Items.Add("Tantsi");

        teostaNupp = new Button { Text = "Käivita tegevus!", BackgroundColor = Colors.Blue, TextColor = Colors.White };
        teostaNupp.Clicked += TeostaNupp_Clicked;

        // Slider (Läbipaistvus 0.0 kuni 1.0)
        lapiPaistvusSlider = new Slider { Minimum = 0, Maximum = 1, Value = 1 };
        lapiPaistvusSlider.ValueChanged += LapiPaistvusSlider_ValueChanged;

        // Stepper (Kiirus: 100ms kuni 2000ms, samm 100)
        kiirusStepper = new Stepper { Minimum = 100, Maximum = 2000, Increment = 100, Value = 500 };
        kiirusLabel = new Label { Text = $"Animatsiooni kiirus: {kiirusStepper.Value} ms" };
        kiirusStepper.ValueChanged += KiirusStepper_ValueChanged;
        // Boonus: Ööreþiim
        ooreziimLuliti = new Switch { IsToggled = false };
        ooreziimLuliti.Toggled += OoreziimLuliti_Toggled;

        // 4. KOKKUPANEK
        VerticalStackLayout juhtPaneel = new VerticalStackLayout
        {
            Padding = 20,
            Spacing = 10,
            Children =
            {
                lumememmeAla,
                infoLabel,
                tegevusPicker,
                teostaNupp,
                new Label { Text = "Läbipaistvus:" },
                lapiPaistvusSlider,
                kiirusLabel,
                kiirusStepper,
                new HorizontalStackLayout { Spacing = 10, Children = { new Label { Text = "Ööreþiim:", VerticalOptions = LayoutOptions.Center }, ooreziimLuliti } }
            }
        };

        Content = new ScrollView { Content = juhtPaneel };
    }

    private void KiirusStepper_ValueChanged(object? sender, ValueChangedEventArgs e)
    {
        // Uuendame sildi teksti iga kord, kui kasutaja plussi või miinust vajutab
        kiirusLabel.Text = $"Animatsiooni kiirus: {e.NewValue} ms";
    }

    // --- SÜNDMUSED (EVENTS) ---

    private void LapiPaistvusSlider_ValueChanged(object? sender, ValueChangedEventArgs e)
    {
        // Muudame kõigi osade läbipaistvust vastavalt Slideri väärtusele
        double udu = e.NewValue;
        amber.Opacity = udu;
        pea.Opacity = udu;
        keha.Opacity = udu;
        alakeha.Opacity = udu;
    }

    private void OoreziimLuliti_Toggled(object? sender, ToggledEventArgs e)
    {
        // Boonusülesanne: Ööreþiim
        if (e.Value) // Kui lüliti on sees
        {
            this.BackgroundColor = Colors.DarkBlue;
            infoLabel.TextColor = Colors.White;
        }
        else // Kui lüliti on väljas
        {
            this.BackgroundColor = Colors.LightBlue;
            infoLabel.TextColor = Colors.Black;
        }
    }

    private async void TeostaNupp_Clicked(object? sender, EventArgs e)
    {
        if (tegevusPicker.SelectedIndex == -1)
        {
            await DisplayAlertAsync("Viga", "Palun vali Pickerist tegevus!", "OK");
            return;
        }

        string valik = tegevusPicker.SelectedItem.ToString();
        infoLabel.Text = $"Viimane tegevus: {valik}";

        // Loeme Stepperist kiiruse väärtuse (millisekundites)
        uint kiirus = (uint)kiirusStepper.Value;

        switch (valik)
        {
            case "Peida lumememm":
                lumememmeAla.IsVisible = false;
                break;

            case "Näita lumememm":
                lumememmeAla.IsVisible = true;

                // 1. Tühistame kõik pooleli olevad animatsioonid (väga oluline!)
                lumememmeAla.CancelAnimations();
                pea.CancelAnimations();
                keha.CancelAnimations();
                alakeha.CancelAnimations();
                amber.CancelAnimations();

                // 2. Taastame asukoha (toome maa alt välja ja paneme keskele)
                lumememmeAla.TranslationX = 0;
                lumememmeAla.TranslationY = 0;
                amber.Rotation = 0; //  Paneme kaabu uuesti otseks!

                // 3. Taastame suuruse (kui oli ära sulanud)
                pea.Scale = 1; keha.Scale = 1; alakeha.Scale = 1; amber.Scale = 1;

                // 4. Taastame läbipaistvuse
                pea.Opacity = 1; keha.Opacity = 1; alakeha.Opacity = 1; amber.Opacity = 1;

                // 5. Paneme liuguri tagasi maksimumi peale, et kasutajaliides klapiks
                lapiPaistvusSlider.Value = 1;

                break;

            case "Muuda värvi":
                bool vastus = await DisplayAlertAsync("Kinnitus", "Kas soovid lumememme värvi muuta?", "Jah", "Ei");
                if (vastus)
                {
                    Color uusVarv = Color.FromRgb(rnd.Next(0, 256), rnd.Next(0, 256), rnd.Next(0, 256));
                    pea.BackgroundColor = uusVarv;
                    keha.BackgroundColor = uusVarv;
                    alakeha.BackgroundColor = uusVarv;
                    amber.Color = Color.FromRgb(rnd.Next(0, 256), rnd.Next(0, 256), rnd.Next(0, 256)); // Ämber saab eraldi värvi (Boonus)
                }
                break;

            case "Sulata":
                // 1. VÄRIN (Kasutame 'await', et oodata iga liigutuse lõppu)
                uint varinaKiirus = 50; // 50 millisekundit = väga kiire liigutus
                await lumememmeAla.TranslateToAsync(-10, 0, varinaKiirus); // Vasakule
                await lumememmeAla.TranslateToAsync(10, 0, varinaKiirus);  // Paremale
                await lumememmeAla.TranslateToAsync(-10, 0, varinaKiirus); // Vasakule
                await lumememmeAla.TranslateToAsync(10, 0, varinaKiirus);  // Paremale
                await lumememmeAla.TranslateToAsync(0, 0, varinaKiirus);   // Tagasi keskele

                // 2. SULAMINE (Ilma 'await'-ita, et kogu sulamisprotsess toimuks sünkroonselt/korraga)
                lumememmeAla.TranslateToAsync(0, 250, kiirus); // Kogu konteiner vajub maa alla

                pea.FadeToAsync(0, kiirus); pea.ScaleToAsync(0, kiirus);
                keha.FadeToAsync(0, kiirus); keha.ScaleToAsync(0, kiirus);
                alakeha.FadeToAsync(0, kiirus); alakeha.ScaleToAsync(0, kiirus);
                amber.FadeToAsync(0, kiirus); amber.ScaleToAsync(0, kiirus);
                break;

            case "Tantsi":
                //Boonus: Räägib enne tantsimist!
                await TextToSpeech.Default.SpeakAsync("Jõulud tulevad!");

                // Samm 1: Elemendid lähevad eri suundadesse (ja ämber pöörleb)
                // Task.WhenAll ootab, kuni kõik sulgudes olevad animatsioonid korraga lõpevad
                await Task.WhenAll(
                    amber.TranslateToAsync(20, -20, kiirus / 2), // Ämber liigub paremale ja üles
                    amber.RotateToAsync(25, kiirus / 2),         // ...ja läheb veidi viltu
                    pea.TranslateToAsync(-15, 0, kiirus / 2),    // Pea liigub vasakule
                    keha.TranslateToAsync(20, 0, kiirus / 2),    // Keha paremale
                    alakeha.TranslateToAsync(-20, 0, kiirus / 2) // Alakeha vasakule
                );

                // Samm 2: Elemendid liiguvad vastassuunda (suurem hoog, läbivad pikema maa)
                await Task.WhenAll(
                    amber.TranslateToAsync(-20, -20, kiirus),
                    amber.RotateToAsync(-25, kiirus),
                    pea.TranslateToAsync(15, 0, kiirus),
                    keha.TranslateToAsync(-20, 0, kiirus),
                    alakeha.TranslateToAsync(20, 0, kiirus)
                );

                // Samm 3: Kõik tagasi algasendisse (keskele)
                await Task.WhenAll(
                    amber.TranslateToAsync(0, 0, kiirus / 2),
                    amber.RotateToAsync(0, kiirus / 2),
                    pea.TranslateToAsync(0, 0, kiirus / 2),
                    keha.TranslateToAsync(0, 0, kiirus / 2),
                    alakeha.TranslateToAsync(0, 0, kiirus / 2)
                );
                break;
        }
    }
}