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
    internal class GenreFormVM
    {
        private static GenreDao genreDao = new();

        private GenrePost _genre;

        public GenreFormVM()
        {
            Genre = new GenrePost();
        }

        public GenrePost Genre
        {
            get => _genre;
            set
            {
                _genre = value;
                OnPropertyChanged(nameof(Genre));
            }
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        protected void OnPropertyChanged(string name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }

        public async Task SaveGenre()
        {
            await genreDao.CreateGenre(_genre);
            _genre = new();
        }
    }
}
