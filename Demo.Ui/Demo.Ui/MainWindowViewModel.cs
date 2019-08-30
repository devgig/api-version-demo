using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace Demo.Ui
{
    public class MainWindowViewModel : ViewModelBase
    {
        private readonly FileUploader _fileUploader;

        public MainWindowViewModel()
        {
            _fileUploader = new FileUploader();
            

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

        public ICommand SearchCommand => new AsyncCommand(() => SearchAsync(), () => CanExecuteSearch());

        private bool CanExecuteSearch()
        {
            return !String.IsNullOrEmpty(NumberOfDays)
                 && !String.IsNullOrEmpty(SearchCriteria)
                 && NumberOfDays.ToNumber() > 0;
        }

        private async Task SearchAsync()
        {

            
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
                var finished =  _fileUploader.Upload(uploadFile);
                if (finished)
                {
                    MessageBox.Show("File uploaded");
                }
            }


        }
    }
}
