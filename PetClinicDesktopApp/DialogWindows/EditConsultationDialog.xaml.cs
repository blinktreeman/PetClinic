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

namespace PetClinicDesktopApp.DialogWindows
{
    /// <summary>
    /// Interaction logic for EditConsultationDialog.xaml
    /// </summary>
    public partial class EditConsultationDialog : Window
    {
        private int _consultationId;
        private Consultation _consultation = new();
        public EditConsultationDialog(int consultationId)
        {
            InitializeComponent();
            PrepareDialog(consultationId);
        }

        private void PrepareDialog(int consultationId)
        {
            _consultationId = consultationId;

            HttpClient httpClient = new();
            ClinicServiceClient clinicServiceClient = new(MainWindow.BASEURL, httpClient);

            _consultation = clinicServiceClient.GetConsultationByIdAsync(consultationId).Result;
            Client client = clinicServiceClient.GetClientByIdAsync(_consultation.ClientId).Result;
            Pet pet = clinicServiceClient.GetPetByIdAsync(_consultation.PetId).Result;

            ClientNameLabel.Content = client.SurName + " " + client.FirstName + " " + client.Patronymic;
            PetNameLabel.Content = pet.Name;
            ConsultationDatePicker.SelectedDate = _consultation.ConsultationDate.DateTime;
            ConsultationCommentTextBox.Text = _consultation.Description;
        }

        private void SaveConsultationButton_Click(object sender, RoutedEventArgs e)
        {
            HttpClient httpClient = new();
            ClinicServiceClient clinicServiceClient = new(MainWindow.BASEURL, httpClient);

            if (ConsultationDatePicker.SelectedDate != null)
            {
                UpdateConsultationRequest updateConsultation = new()
                {
                    ConsultationId = _consultationId,
                    ClientId = _consultation.ClientId,
                    PetId = _consultation.PetId,
                    ConsultationDate = (DateTimeOffset)ConsultationDatePicker.SelectedDate,
                    Description = ConsultationCommentTextBox.Text
                };
                clinicServiceClient.UpdateConsultationAsync(updateConsultation);
            }
        }
    }
}
