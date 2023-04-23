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
    internal class BookFormVM
    {
        private static BookDao bookDao = new();

        private BookPost _book;

        public BookFormVM()
        {
            Book = new BookPost();
        }

        public BookPost Book
        {
            get => _book;
            set
            {
                _book = value;
                OnPropertyChanged(nameof(Book));
            }
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        protected void OnPropertyChanged(string name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }

        public async Task SaveBook()
        {
            await bookDao.CreateBook(_book);
            _book = new();
        }
    }
}
