using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySecondBrain.Domain.Services;
using MySecondBrain.Infrastructure.DB;
using MySecondBrain.Application.ViewModels;

namespace MySecondBrain.Application.Services
{
    public class TagControllerService
    {
        public TagListViewModel GetTagListViewModel()
        {
            TagService TagService = new TagService();

            var TagList = TagService.GetTags();

            var ViewModel = new TagListViewModel();

            ViewModel.Tags = TagList;

            return ViewModel;
        }

        public static TagDetailViewModel GetTagDetail(int tagId)
        {
            Tag tag = TagService.GetTag(tagId);

            if (tag == null)
            {
                return null;
            }

            TagDetailViewModel vm = new TagDetailViewModel()
            {
                Tag = tag
            };

            return vm;
        }

        public static List<Tag> GetTagsListOfUser(string userId)
        {
            var tags = TagService.GetAllTagsOfUser(userId);

            TagListViewModel vm = new TagListViewModel()
            {
                Tags = tags,
            };

            return vm.Tags;
        }

        public static void CreateTag(Tag tag, string userId)
        {
            TagService.CreateTag(tag, userId);
        }

        public static void EditTag(Tag tag)
        {
            TagService.EditTag(tag);
        }

        public static void DeleteTag(int tagId)
        {
            TagService.DeleteTag(tagId);
        }
    }
}
