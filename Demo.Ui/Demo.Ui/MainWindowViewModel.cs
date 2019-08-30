using Demo.Ui.Core;
using Demo.Ui.Services;
using System;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using Demo.Shared.Extensions;

namespace Demo.Ui
{
    public class MainWindowViewModel : ViewModelBase
    {
        private readonly RentalUploader _rentalUploader;
        private readonly RentalProvider _rentalProvider;

        public MainWindowViewModel()
        {
            _rentalUploader = new RentalUploader();
            _rentalProvider = new RentalProvider();
            

        }

        #region Properties
        private string _numberOfDays;

        public string NumberOfDays
        {
            get { return _numberOfDays; }
            set
            {
                _numberOfDays = value;
                NotifyPropertyChanged();
            }
        }

        private string _searchCriteria;

        public string SearchCriteria
        {
            get { return _searchCriteria; }
            set {
                _searchCriteria = value;
                NotifyPropertyChanged();
            }
        }

        #endregion

        public ICommand SearchCommand => new AsyncCommand(() => SearchAsync());

       
        private async Task SearchAsync()
        {
            var result =  _rentalProvider.GetByCriteria(SearchCriteria);

            
        }

        public ICommand UploadFileCommand => new AsyncCommand(() => OpenFileDialogAsync());

        private async Task OpenFileDialogAsync()
        {

            var openFileDialog = new Microsoft.Win32.OpenFileDialog
            {
                Filter = @"CSV file (*.csv)|*.csv",
                CheckFileExists = true,
                CheckPathExists = true,
                Multiselect = false
            };

            var uploadFile = openFileDialog.ShowDialog().GetValueOrDefault() ? openFileDialog.FileName : string.Empty;
            if (string.IsNullOrEmpty(uploadFile))
            {
                MessageBox.Show("No file selected.");
            }
            else
            {
                var finished =  _rentalUploader.Upload(uploadFile);
                if (finished)
                {
                    MessageBox.Show("File uploaded");
                }
            }


        }
    }
}
