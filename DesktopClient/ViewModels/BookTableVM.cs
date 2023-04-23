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
    internal class BookTableVM
    {
        private static BookDao bookDao = new();

        private ObservableCollection<Book> _bookList;
        public ObservableCollection<Book> BookList { get => _bookList; set => _bookList = value; }

        public BookTableVM()
        {
            BookList = new ObservableCollection<Book>();
        }

        public async Task GetBooks()
        {
            ClearList();
            var books = await bookDao.GetAllBooks();
            foreach (var item in books)
            {
                BookList.Add(item);
            }
        }

        public async Task DeleteBook(long id)
        {
            await bookDao.DeleteBook(id);
            await GetBooks();
        }

        public async Task UpdateBook(long id)
        {
            var toUpdate = BookList.First(auth => auth.Id == id);
            if (toUpdate == null)
            {
                return;
            }
            Book updated = await bookDao.UpdateBook(toUpdate);
            var insertionIndex = BookList.IndexOf(toUpdate);
            BookList.Remove(toUpdate);
            BookList.Insert(insertionIndex, updated);
        }

        public void ClearList()
        {
            BookList.Clear();
        }
    }
}
