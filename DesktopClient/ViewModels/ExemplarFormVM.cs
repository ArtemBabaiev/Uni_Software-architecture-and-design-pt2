using DesktopClient.Data.DAOs;
using DesktopClient.Network.Requests;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesktopClient.ViewModels
{
    internal class ExemplarFormVM
    {
        private static ExemplarDao exemplarDao = new();

        private ExemplarPost _exemplar;

        public ExemplarFormVM()
        {
            Exemplar = new ExemplarPost();
        }

        public ExemplarPost Exemplar
        {
            get => _exemplar;
            set
            {
                _exemplar = value;
                OnPropertyChanged(nameof(Exemplar));
            }
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        protected void OnPropertyChanged(string name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }

        public async Task SaveExemplar()
        {
            await exemplarDao.CreateExemplar(_exemplar);
            _exemplar = new();
        }
    }
}
