using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;

namespace Naidis_TARpv24
{
    public class TerminItem: INotifyPropertyChanged
    {
        private bool _kasSelge;

        // Unikaalne ID aitab vältida vigu kustutamisel
        public string Id { get; set; } = Guid.NewGuid().ToString();
        public string Nimi { get; set; }
        public string Selgitus { get; set; }

        public bool KasSelge
        {
            get => _kasSelge;
            set
            {
                if (_kasSelge != value)
                {
                    _kasSelge = value;
                    OnPropertyChanged();
                }
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
