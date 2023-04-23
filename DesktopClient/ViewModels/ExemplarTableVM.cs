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
    internal class ExemplarTableVM
    {
        private static ExemplarDao exemplarDao = new();

        private ObservableCollection<Exemplar> _exemplarList;
        public ObservableCollection<Exemplar> ExemplarList { get => _exemplarList; set => _exemplarList = value; }

        public ExemplarTableVM()
        {
            ExemplarList = new ObservableCollection<Exemplar>();
        }

        public async Task GetExemplars()
        {
            ClearList();
            var exemplars = await exemplarDao.GetAllExemplars();
            foreach (var item in exemplars)
            {
                ExemplarList.Add(item);
            }
        }

        public async Task DeleteExemplar(long id)
        {
            await exemplarDao.DeleteExemplar(id);
            await GetExemplars();
        }

        public async Task UpdateExemplar(long id)
        {
            var toUpdate = ExemplarList.First(auth => auth.Id == id);
            if (toUpdate == null)
            {
                return;
            }
            Exemplar updated = await exemplarDao.UpdateExemplar(toUpdate);
            var insertionIndex = ExemplarList.IndexOf(toUpdate);
            ExemplarList.Remove(toUpdate);
            ExemplarList.Insert(insertionIndex, updated);
        }

        public void ClearList()
        {
            ExemplarList.Clear();
        }
    }
}
