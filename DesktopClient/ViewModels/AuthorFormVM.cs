using DesktopClient.Data.DAOs;
using DesktopClient.Network.Requests;
using System.ComponentModel;
using System.Threading.Tasks;

namespace DesktopClient.ViewModels
{
    internal class AuthorFormVM : INotifyPropertyChanged
    {
        private static AuthorDao authorDao = new();

        private AuthorPost _author;

        public AuthorFormVM()
        {
            Author = new AuthorPost();
        }

        public AuthorPost Author
        {
            get => _author;
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
            _author = new();
        }

    }
}
