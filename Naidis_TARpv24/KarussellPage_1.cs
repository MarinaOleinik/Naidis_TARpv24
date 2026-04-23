namespace Naidis_TARpv24;

public class KarussellPage_1 : ContentPage
{
    public class CarouselItem
    {
        public string Title { get; set; }
        public string ImageUrl { get; set; }
    }
    
		//Klassitaseme väljad, et hoida CarouselView ja selle elemendid ja saaks neid kasutada kogu klassis
    private CarouselView carouselView;
    private List<CarouselItem> items;
    private int position = 0;

    public KarussellPage_1()
    {
        Title = "Karusselli näide";

        // 📸 Andmete allikas - karuselli sisuks
        // Iga objekt sisaldab pealkirja ja pildi URL-i
        var items = new List<CarouselItem>
        {
            new CarouselItem { Title = "Päikesetõus", ImageUrl = "https://picsum.photos/id/1015/600/400" },
            new CarouselItem { Title = "Metsavaikus", ImageUrl = "https://picsum.photos/id/1016/600/400" },
            new CarouselItem { Title = "Järvepeegel", ImageUrl = "https://picsum.photos/id/1018/600/400" }
        };

        // 🎠 CarouselView - MAUI komponent horisontaalseks kerimiseks
        var carouselView = new CarouselView
        {
            ItemsSource = items,
            HeightRequest = 300,
            IsBounceEnabled = true,

            ItemTemplate = new DataTemplate(() =>
            {
                // Frame - ümardatud servade ja varjuga konteiner
                var frame = new Frame
                {
                    CornerRadius = 20,
                    HasShadow = true,
                    Padding = 0,
                    Margin = new Thickness(10),
                    BackgroundColor = Colors.Transparent
                };

                var grid = new Grid();

                var image = new Image
                {
                    Aspect = Aspect.AspectFill
                };
                image.SetBinding(Image.SourceProperty, "ImageUrl");

                // 🎨 Gradient-taust, et tekst oleks paremini loetav
                var gradient = new BoxView
                {
                    Background = new LinearGradientBrush
                    {
                        StartPoint = new Point(0, 1),
                        EndPoint = new Point(0, 0),
                        GradientStops = new GradientStopCollection
                        {
                            new GradientStop(Colors.Black.WithAlpha(0.6f), 0),
                            new GradientStop(Colors.Transparent, 1)
                        }
                    },
                    Opacity = 0.7
                };

                var label = new Label
                {
                    TextColor = Colors.White,
                    FontSize = 24,
                    Margin = new Thickness(20),
                    VerticalOptions = LayoutOptions.End,
                    HorizontalOptions = LayoutOptions.Start
                };
                label.SetBinding(Label.TextProperty, "Title");

                // 👆 Klikkimise sündmus
                var tap = new TapGestureRecognizer();
                tap.Tapped += async (s, e) =>
                {
                    var tappedItem = ((Frame)s).BindingContext as CarouselItem;
                    await DisplayAlert("Valisid:", tappedItem?.Title ?? "Tundmatu", "OK");
                };
                frame.GestureRecognizers.Add(tap);

                grid.Children.Add(image);
                grid.Children.Add(gradient);
                grid.Children.Add(label);

                frame.Content = grid;
                return frame;
            })
        };

        // ⚪ IndicatorView - väikesed punktid, mis näitavad mitmes pilt parasjagu on
        var indicatorView = new IndicatorView
        {
            IndicatorColor = Colors.Gray,
            SelectedIndicatorColor = Colors.Blue,
            HorizontalOptions = LayoutOptions.Center,
            Margin = new Thickness(0, 10)
        };

        // Seosta IndicatorView CarouselView-ga
        carouselView.IndicatorView = indicatorView;

        // 🔄 Automaatne kerimine iga 4 sekundi järel
        Device.StartTimer(TimeSpan.FromSeconds(4), () =>
        {
            if (items.Count == 0)
                return false;

            position = (position + 1) % items.Count;
            carouselView.Position = position;

            return true; // jätkab taimerit
        });

        // 📱 Lehekülje paigutus (StackLayout - vertikaalne paigutus)
        Content = new StackLayout
        {
            Padding = 20,
            Children =
            {
                carouselView,
                indicatorView
            }
        };
    }
}
