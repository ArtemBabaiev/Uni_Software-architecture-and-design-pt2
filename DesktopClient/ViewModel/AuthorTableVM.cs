using DesktopClient.Dao;
using DesktopClient.Entities;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesktopClient.ViewModel
{
    internal class AuthorTableVM
    {
        AuthorDao authorDao = new();

        private ObservableCollection<Author> _authorList;
        public ObservableCollection<Author> AuthorList { get => _authorList; set => _authorList = value; }

        public AuthorTableVM()
        {
            AuthorList = new ObservableCollection<Author>();
        }

        public async Task GetAuthors()
        {
            AuthorList.Clear();
            var authors = await authorDao.GetAllAuthors();
            foreach (var item in authors)
            {
                AuthorList.Add(item);
            }
        }

        public async Task DeleteAuthor(long id)
        {
            await authorDao.DeleteAuthor(id);
            await GetAuthors();
        }

        public async Task UpdateAuthor(long id)
        {
            var toUpdate = AuthorList.First(auth =>  auth.Id == id);
            if (toUpdate == null)
            {
                return;
            }
            Author updated = await authorDao.UpdateAuthor(toUpdate);
            var insertionIndex = AuthorList.IndexOf(toUpdate);
            AuthorList.Remove(toUpdate);
            AuthorList.Insert(insertionIndex, updated);
        }
    }
}
