using Naidis_TARpv24.Strings; // Veendu, et nimeruum klapib
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Naidis_TARpv24;

public partial class AnimalViewModel : INotifyPropertyChanged
{
    private string _currentAnimalImage;

    public string CurrentAnimalImage
    {
        get => _currentAnimalImage;
        set
        {
            if (_currentAnimalImage != value)
            {
                _currentAnimalImage = value;
                OnPropertyChanged();
            }
        }
    }

    public AnimalViewModel()
    {
        InitializeComponent();
        BindingContext = this;
        // Algseis .resx failist
        CurrentAnimalImage = AppResources.AnimalCat;
    }

    // NUPPUDE MEETODID:
    private void OnDogClicked(object sender, EventArgs e) => CurrentAnimalImage = AppResources.AnimalDog;
    private void OnFishClicked(object sender, EventArgs e) => CurrentAnimalImage = AppResources.AnimalFish;

    public event PropertyChangedEventHandler PropertyChanged;
    protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}