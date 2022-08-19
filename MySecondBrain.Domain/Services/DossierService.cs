using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySecondBrain.Infrastructure.DB;

namespace MySecondBrain.Domain.Services
{
    public class DossierService
    {
        public static List<Dossier> GetDossiers()
        {
            MySecondBrain_LMContext mySecondBrainContext = new MySecondBrain_LMContext();

            List<Dossier> dossiers1 = new List<Dossier>();

            var dossiers = mySecondBrainContext.Dossiers.ToList();

            foreach (var getDossier in dossiers)
            {
                Dossier dossier = new Dossier();

                dossier.Iddossier = getDossier.Iddossier;
                dossier.Nom = getDossier.Nom;
                dossier.IddossierParent = getDossier.IddossierParent;
                dossier.UserId = getDossier.UserId;
                dossiers1.Add(dossier);
            }

            return dossiers1;
        }
        public static Dossier GetDossier(int dossierId)
        {
            using (MySecondBrain_LMContext db = new Infrastructure.DB.MySecondBrain_LMContext())
            {
                return db.Dossiers.Find(dossierId);
            }
        }

        public static List<Dossier> GetAllDossiersOfUser(string userId)
        {
            using (MySecondBrain_LMContext db = new MySecondBrain_LMContext())
            {
                return db.Dossiers.Where(n => n.User.Id == userId).ToList();
            }
        }

        public static void CreateDossier(Dossier dossier, string userId)
        {
            dossier.UserId = userId;

            MySecondBrain_LMContext db = new MySecondBrain_LMContext();

            db.Dossiers.Add(dossier);
            db.SaveChanges();
        }

        public static void EditDossier(Dossier dossier)
        {
            MySecondBrain_LMContext db = new MySecondBrain_LMContext();

            Dossier dossierToUpdate = db.Dossiers.Find(dossier.Iddossier);
            {
                if (dossierToUpdate != null)
                {
                    dossierToUpdate.Nom = dossier.Nom;
                    dossierToUpdate.IddossierParent = dossier.IddossierParent;
                    db.Update(dossierToUpdate);
                }
            }
            db.SaveChanges();
        }


        public static void DeleteDossier(int dossierId)
        {
            MySecondBrain_LMContext db = new MySecondBrain_LMContext();

            var dossierToRemove = db.Dossiers.SingleOrDefault(a => a.Iddossier == dossierId);
            if (dossierToRemove != null)
            {
                db.Dossiers.Remove(dossierToRemove);
                db.SaveChanges();
            }

        }
    }
}
