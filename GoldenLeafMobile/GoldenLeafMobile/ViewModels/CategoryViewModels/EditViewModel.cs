﻿using GoldenLeafMobile.Models;
using GoldenLeafMobile.Models.CategoryModels;
using GoldenLeafMobile.Models.ClerkModels;
using System.Net.Http;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace GoldenLeafMobile.ViewModels.CategoryViewModels
{
    public class EditViewModel : BaseEntryPage
    {


        public EditViewModel(Category _category)
        {
            Category = _category;
            SaveCategoryComand = new Command
                (
                    () =>
                    {
                        MessagingCenter.Send<Category>(Category, ASK);
                    },
                      () =>
                      {
                          return !string.IsNullOrEmpty(Category.Title);
                      }

                );
        }


    }
}
