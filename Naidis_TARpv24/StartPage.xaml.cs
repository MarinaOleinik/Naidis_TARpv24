using System.Threading.Tasks;

namespace Naidis_TARpv24;

public partial class StartPage : ContentPage
{
	VerticalStackLayout vst;
	ScrollView sv;
	public List<ContentPage> Lehed = new List<ContentPage>() { new TextPage(), new FigurePage(),new Valgusfoor(),new DateTimePage(),new StepperSliderPage() };
	public List<string> LeheNimed = new List<string>() { "Tekst", "Kujund", "Valgusfoor", "Kuupäev/Aeg", "Liigur" };
    public StartPage()
	{
		//Title = "Avaleht";
		vst=new VerticalStackLayout { Padding=20,Spacing=15 };
		for (int i=0; i < Lehed.Count; i++)
		{
			Button nupp = new Button
			{
				Text = LeheNimed[i],
				FontSize = 36,
				FontFamily="Luffio",
                BackgroundColor = Colors.LightGray,
				TextColor = Colors.Black,
				CornerRadius = 10,
				HeightRequest = 60,
				ZIndex = i
			};
			vst.Add(nupp);
			nupp.Clicked += (sender, e) =>
			{
				var valik = Lehed[nupp.ZIndex];
				Navigation.PushAsync(valik);
			};
		}
		sv=new ScrollView { Content=vst };
		Content = sv;
    }
}