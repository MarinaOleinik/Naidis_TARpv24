using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;

namespace Naidis_TARpv24
{
    public class TerminItem: INotifyPropertyChanged
    {
        private string _nimi;
        private string _selgitus;
        private bool _kasSelge;

        // Unikaalne ID aitab vältida vigu kustutamisel
        public string Id { get; set; } = Guid.NewGuid().ToString();
        // Tekst peab olema Property, muidu Binding ei leia seda üles
        public string Nimi
        {
            get => _nimi;
            set { _nimi = value; OnPropertyChanged(); }
        }

        public string Selgitus
        {
            get => _selgitus;
            set { _selgitus = value; OnPropertyChanged(); }
        }

        public bool KasSelge
        {
            get => _kasSelge;
            set { _kasSelge = value; OnPropertyChanged(); }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
