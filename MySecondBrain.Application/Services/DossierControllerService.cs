using MySecondBrain.Application.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySecondBrain.Domain.Services;
using MySecondBrain.Infrastructure.DB;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace MySecondBrain.Application.Services
{
    public class DossierControllerService
    {
        public DossierListViewModel GetDossierListViewModel()
        {
            DossierService dossierService = new DossierService();

            var DossierList = DossierService.GetDossiers();

            var ViewModel = new DossierListViewModel();

            ViewModel.Dossiers = DossierList;

            return ViewModel;
        }

        public static DossierDetailViewModel GetDossierDetail(int dossierId)
        {
            Dossier dossier = DossierService.GetDossier(dossierId);

            if (dossier == null)
            {
                return null;
            }

            DossierDetailViewModel vm = new DossierDetailViewModel()
            {
                Dossier = dossier
            };

            return vm;
        }

        public static DossierDetailViewModel GetDossierDetail()
        {

            DossierDetailViewModel vm = new DossierDetailViewModel()
            {
                DossierList = GetDossierList("d504ee08-20f8-45c6-9ae3-b06a30f13ab5")//change for a variable
            };

            return vm;
        }

        public static List<SelectListItem> GetDossierList(string userId)
        {
            var dossierList = DossierService.GetAllDossiersOfUser(userId);

            var list = new List<SelectListItem>();

            foreach (var dossier in dossierList)
            {
                list.Add(new SelectListItem
                {
                    Text = dossier.Nom,
                    Value = dossier.Iddossier.ToString()
                });
            }

            return list;
        }

        public static List<Dossier> GetDossiersListOfUser(string userId)
        {
            var dossiers = DossierService.GetAllDossiersOfUser(userId);

            DossierListViewModel vm = new DossierListViewModel()
            {
                Dossiers = dossiers,
            };

            return vm.Dossiers;
        }

        public static void CreateDossier(Dossier dossier, string userId, int idDossierParent)
        {
            DossierService.CreateDossier(dossier, userId, idDossierParent);
        }

        public static void EditDossier(Dossier dossier)
        {
            DossierService.EditDossier(dossier);
        }

        public static void DeleteDossier(int dossierId)
        {
            DossierService.DeleteDossier(dossierId);
        }
    }
}
