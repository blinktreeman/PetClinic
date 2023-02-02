using ClinicServiceNamespace;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace PetClinicDesktopApp.Dialogs
{
    /// <summary>
    /// Interaction logic for ConsultationDialog.xaml
    /// </summary>
    public partial class AddConsultationDialog : Window
    {
        public AddConsultationDialog()
        {
            InitializeComponent();
            PrepareDialog();
        }

        private void PrepareDialog()
        {
            HttpClient httpClient = new();
            ClinicServiceClient clinicServiceClient = new(MainWindow.BASEURL, httpClient);
            ConsultationClientComboBox.ItemsSource = clinicServiceClient.GetAllClientsAsync().Result;
        }

        private void SaveConsultationButton_Click(object sender, RoutedEventArgs e)
        {
            HttpClient httpClient = new();
            ClinicServiceClient clinicServiceClient = new(MainWindow.BASEURL, httpClient);

            if (ConsultationClientComboBox.SelectedIndex != -1 &&
                ConsultationPetComboBox.SelectedIndex != -1 &&
                ConsultationDatePicker.SelectedDate != null)
            {
                Client client = (Client)ConsultationClientComboBox.SelectedItem;
                Pet pet = (Pet)ConsultationPetComboBox.SelectedItem;

                CreateConsultationRequest request = new()
                {
                    ClientId = client.ClientId,
                    PetId = pet.PetId,
                    ConsultationDate = (DateTimeOffset)ConsultationDatePicker.SelectedDate,
                    Description = ConsultationCommentTextBox.Text
                };
                clinicServiceClient.CreateConsultationAsync(request);
            }
        }

        private void ConsultationClientComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            HttpClient httpClient = new();
            ClinicServiceClient clinicServiceClient = new(MainWindow.BASEURL, httpClient);

            Client client = (Client)ConsultationClientComboBox.SelectedItem;
            ConsultationPetComboBox.ItemsSource = clinicServiceClient.GetAllPetsAsync(client.ClientId).Result;
        }
    }
}
