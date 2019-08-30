using Demo.Ui.Core;
using Demo.Ui.Services;
using System;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using Demo.Shared.Extensions;
using System.Collections.ObjectModel;
using Demo.Shared.Model;
using Demo.Ui.Models;
using System.Linq;

namespace Demo.Ui
{
    public class MainWindowViewModel : ViewModelBase
    {
        private readonly RentalUploader _rentalUploader;
        private readonly IRentalV1Provider _rentalProvider;
        private readonly IRentalV2Provider _rentalV2Provider;

        public MainWindowViewModel()
        {
            _rentalUploader = new RentalUploader();
            _rentalProvider = new RentalV1Provider();
            _rentalV2Provider = new RentalV2Provider();
            RentalItems = new ObservableCollection<RentalResult>();

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
            set
            {
                _searchCriteria = value;
                NotifyPropertyChanged();
            }
        }

        public string Year { get => year; set { year = value; NotifyPropertyChanged(); } }
        public string Make { get => make; set { make = value; NotifyPropertyChanged(); } }
        public string Model { get => model; set { model = value; NotifyPropertyChanged(); } }

        #endregion

        public ICommand SearchCommand => new AsyncCommand(() => SearchAsync());


        private async Task SearchAsync()
        {
            await Application.Current.Dispatcher.BeginInvoke(new Action(() => { RentalItems.Clear(); }));

            var searchCriteria = SearchCriteria;
            var numberOfDays = NumberOfDays.ToNumber();
            var year = Year;
            var make = Make;
            var model = Model;

            var rentals = SelectedVersion == ApiVersionEnum.V1
              ? await _rentalProvider.GetByCriteria(searchCriteria, numberOfDays)
              : await _rentalV2Provider.GetByCriteria(year, make, model, numberOfDays);

            await Application.Current.Dispatcher.BeginInvoke(new Action(() =>
            {

                foreach (var rental in rentals)
                    RentalItems.Add(rental);
            }));

        }


        private ApiVersionEnum selectedVersion;
        private string year;
        private string make;
        private string model;

        public ApiVersionEnum SelectedVersion
        {
            get => selectedVersion; set
            {
                selectedVersion = value;
                NotifyPropertyChanged();
                RentalItems.Clear();
            }
        }



        public ObservableCollection<RentalResult> RentalItems { get; set; }

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
                var finished = await _rentalUploader.Upload(uploadFile);
                if (finished)
                {
                    MessageBox.Show("File uploaded");
                }
            }


        }
    }
}
