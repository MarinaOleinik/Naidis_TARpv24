using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.Json;
using System.Windows.Input;

namespace Naidis_TARpv24
{
    public class TerminViewModel : INotifyPropertyChanged
    {
        private string _uusNimi;
        private string _uusSelgitus;
        private readonly string _failiTee;

        public ObservableCollection<TerminItem> Terminid { get; set; } = new();

        public string UusNimi
        {
            get => _uusNimi;
            set { _uusNimi = value; OnPropertyChanged(); }
        }

        public string UusSelgitus
        {
            get => _uusSelgitus;
            set { _uusSelgitus = value; OnPropertyChanged(); }
        }

        public ICommand LisaCommand { get; }
        public ICommand KustutaCommand { get; }

        public TerminViewModel()
        {
            // Määrame turvalise asukoha, kuhu fail salvestatakse
            _failiTee = Path.Combine(FileSystem.AppDataDirectory, "terminid.json");

            LisaCommand = new Command(LisaTermin);
            KustutaCommand = new Command<TerminItem>(KustutaTermin);

            LaeAndmed();
        }

        private void LisaTermin()
        {
            // Kontrollime, et väljad poleks tühjad
            if (string.IsNullOrWhiteSpace(UusNimi) || string.IsNullOrWhiteSpace(UusSelgitus))
                return;

            var uusItem = new TerminItem
            {
                Nimi = UusNimi,
                Selgitus = UusSelgitus,
                KasSelge = false
            };

            // Paneme programmi kuulama, kui selle termini märkeruutu vajutatakse
            uusItem.PropertyChanged += Item_PropertyChanged;
            Terminid.Add(uusItem);

            // Tühjendame tekstikastid
            UusNimi = string.Empty;
            UusSelgitus = string.Empty;

            SalvestaAndmed();
        }

        private void KustutaTermin(TerminItem item)
        {
            if (item != null && Terminid.Contains(item))
            {
                Terminid.Remove(item);
                SalvestaAndmed();
            }
        }

        private void SalvestaAndmed()
        {
            var json = JsonSerializer.Serialize(Terminid);
            File.WriteAllText(_failiTee, json);
            // Trükib salvestamise hetkel JSON-i sisu Visual Studio Output aknasse
            System.Diagnostics.Debug.WriteLine($"\n--- MINU JSON FAIL --- \n{json}\n----------------------");
        }
        

        private void LaeAndmed()
        {
            if (File.Exists(_failiTee))
            {
                var json = File.ReadAllText(_failiTee);
                var laetudTerminid = JsonSerializer.Deserialize<List<TerminItem>>(json);

                if (laetudTerminid != null)
                {
                    Terminid.Clear();
                    foreach (var t in laetudTerminid)
                    {
                        t.PropertyChanged += Item_PropertyChanged;
                        Terminid.Add(t);
                    }
                }
            }
        }

        private void Item_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            // Kui õpilane märgib termini "selgeks", salvestatakse fail automaatselt
            SalvestaAndmed();
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
