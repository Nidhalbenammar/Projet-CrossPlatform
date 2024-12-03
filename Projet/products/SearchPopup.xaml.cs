using Rg.Plugins.Popup.Pages;
using Rg.Plugins.Popup.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Projet.products
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SearchPopup : PopupPage
    {
        public event Action<string> SearchCompleted;

        public SearchPopup()
        {
            InitializeComponent();
        }

        private void OnSearchButtonClicked(object sender, EventArgs e)
        {
            
            var searchQuery = SearchEntry.Text;
            SearchCompleted?.Invoke(searchQuery);
            PopupNavigation.Instance.PopAsync();
        }
    }
}