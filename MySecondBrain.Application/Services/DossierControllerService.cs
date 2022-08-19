using MySecondBrain.Application.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySecondBrain.Domain.Services;
using MySecondBrain.Infrastructure.DB;

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

        public static List<Dossier> GetDossiersListOfUser(string userId)
        {
            var dossiers = DossierService.GetAllDossiersOfUser(userId);

            DossierListViewModel vm = new DossierListViewModel()
            {
                Dossiers = dossiers,
            };

            return vm.Dossiers;
        }

        public static void CreateDossier(Dossier dossier, string userId)
        {
            DossierService.CreateDossier(dossier, userId);
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
