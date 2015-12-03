﻿// The MIT License (MIT)
//
// Copyright (c) 2015 Xamarin
//
// Permission is hereby granted, free of charge, to any person obtaining a copy of
// this software and associated documentation files (the "Software"), to deal in
// the Software without restriction, including without limitation the rights to
// use, copy, modify, merge, publish, distribute, sublicense, and/or sell copies of
// the Software, and to permit persons to whom the Software is furnished to do so,
// subject to the following conditions:
//
// The above copyright notice and this permission notice shall be included in all
// copies or substantial portions of the Software.
//
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
// IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY, FITNESS
// FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR
// COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER
// IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN
// CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.
using System;
using System.Collections.Generic;

using Xamarin.Forms;
using XamarinCRM.Pages.Base;
using XamarinCRM.ViewModels.Products;
using Xamarin;
using XamarinCRM.Statics;
using XamarinCRM.Models;

namespace XamarinCRM.Pages.Products
{
    public partial class CategoryListPage : CategoryListPageXaml
    {
        public CategoryListPage()
        {
            InitializeComponent();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            if (ViewModel.IsInitialized)
                return;

            ViewModel.LoadCategoriesCommand.Execute(ViewModel.Category);

            ViewModel.IsInitialized = true;

            Insights.Track(InsightsReportingConstants.PAGE_CATEGORYLIST);
        }

        async void CategoryItemTapped (object sender, ItemTappedEventArgs e)
        {
            Category catalogCategory = ((Category)e.Item);
            if (catalogCategory.HasSubCategories)
            {
                await Navigation.PushAsync(new CategoryListPage() { BindingContext = new CategoriesViewModel(catalogCategory, null, ViewModel.IsPerformingProductSelection) { Title = catalogCategory.Name } });
            }
            else
            {
                await Navigation.PushAsync(new ProductListPage() { BindingContext = new ProductsViewModel(catalogCategory.Id) { Title = catalogCategory.Name } });
            }
        }
    }

    public abstract class CategoryListPageXaml : ModelBoundContentPage<CategoriesViewModel> { }
}

