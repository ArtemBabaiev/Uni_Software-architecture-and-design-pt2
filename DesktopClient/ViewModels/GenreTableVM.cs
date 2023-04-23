using DesktopClient.Data.DAOs;
using DesktopClient.Data.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesktopClient.ViewModels
{
    internal class GenreTableVM
    {
        private static GenreDao genreDao = new();

        private ObservableCollection<Genre> _genreList;
        public ObservableCollection<Genre> GenreList { get => _genreList; set => _genreList = value; }

        public GenreTableVM()
        {
            GenreList = new ObservableCollection<Genre>();
        }

        public async Task GetGenres()
        {
            ClearList();
            var genres = await genreDao.GetAllGenres();
            foreach (var item in genres)
            {
                GenreList.Add(item);
            }
        }

        public async Task DeleteGenre(long id)
        {
            await genreDao.DeleteGenre(id);
            await GetGenres();
        }

        public async Task UpdateGenre(long id)
        {
            var toUpdate = GenreList.First(auth => auth.Id == id);
            if (toUpdate == null)
            {
                return;
            }
            Genre updated = await genreDao.UpdateGenre(toUpdate);
            var insertionIndex = GenreList.IndexOf(toUpdate);
            GenreList.Remove(toUpdate);
            GenreList.Insert(insertionIndex, updated);
        }

        public void ClearList()
        {
            GenreList.Clear();
        }
    }
}
