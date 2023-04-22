using DesktopClient.Dao;
using DesktopClient.Entities;
using DesktopClient.Requests;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace DesktopClient.ViewModel
{
    internal class AuthorFormVM: INotifyPropertyChanged
    {
        AuthorDao authorDao = new();

        private AuthorPost _author;

        public AuthorFormVM()
        {
            Author = new AuthorPost();
        }

        public AuthorPost Author { get => _author; 
            set
            {
                _author = value;
                OnPropertyChanged(nameof(Author));
            }
        }

        public event PropertyChangedEventHandler? PropertyChanged;


        protected void OnPropertyChanged(string name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }

        public async Task SaveAuthor()
        {
            await authorDao.CreateAuthor(_author);
        }

    }
}
