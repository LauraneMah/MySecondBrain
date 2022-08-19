using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySecondBrain.Infrastructure.DB;

namespace MySecondBrain.Domain.Services
{
    public class TagService
    {
        public static List<Tag> GetTags()
        {
            MySecondBrain_LMContext mySecondBrainContext = new MySecondBrain_LMContext();

            List<Tag> tag1 = new List<Tag>();

            var tags = mySecondBrainContext.Tags.ToList();

            foreach (var getTag in tags)
            {
                Tag tag = new Tag();

                tag.Idtag = getTag.Idtag;
                tag.Nom = getTag.Nom;
                tag.UserId = getTag.UserId;
                tag1.Add(tag);
            }

            return tag1;
        }
        public static Tag GetTag(int tagId)
        {
            using (MySecondBrain_LMContext db = new MySecondBrain_LMContext())
            {
                return db.Tags.Find(tagId);
            }
        }

        public static List<Tag> GetAllTagsOfUser(string userId)
        {
            using (MySecondBrain_LMContext db = new MySecondBrain_LMContext())
            {
                return db.Tags.Where(n => n.User.Id == userId).ToList();
            }
        }

        public static void CreateTag(Tag tag, string userId)
        {
            tag.UserId = userId;

            MySecondBrain_LMContext db = new MySecondBrain_LMContext();

            db.Tags.Add(tag);
            db.SaveChanges();
        }

        public static void EditTag(Tag tag)
        {
            MySecondBrain_LMContext db = new MySecondBrain_LMContext();

            Tag tagToUpdate = db.Tags.Find(tag.Idtag);
            {
                if (tagToUpdate != null)
                {
                    tagToUpdate.Nom = tag.Nom;
                    db.Update(tagToUpdate);
                }
            }
            db.SaveChanges();
        }


        public static void DeleteTag(int tagId)
        {
            MySecondBrain_LMContext db = new MySecondBrain_LMContext();

            var tagToRemove = db.Tags.SingleOrDefault(a => a.Idtag == tagId);
            if (tagToRemove != null)
            {
                db.Tags.Remove(tagToRemove);
                db.SaveChanges();
            }

        }
    }
}
