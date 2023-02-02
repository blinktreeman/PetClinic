using ClinicServiceNamespace;
using PetClinicDesktopApp.Dialogs;
using PetClinicDesktopApp.DialogWindows;
using PetClinicDesktopApp.ViewModels;
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
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Xml.Resolvers;

namespace PetClinicDesktopApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public static string BASEURL = "http://bcomms.ru";
        public MainWindow()
        {
            InitializeComponent();
            LoadConsultations();
        }

        private void LoadConsultations() {

            HttpClient httpClient = new();
            ClinicServiceClient clinicServiceClient = new(BASEURL, httpClient);
            ICollection<Consultation> consultations = clinicServiceClient.GetAllConsultationsAsync().Result;

            List <ConsultationItem> consultationItems = new();

            foreach (Consultation consultation in consultations)
            {
                Client client = clinicServiceClient.GetClientByIdAsync(consultation.ClientId).Result;
                Pet pet = clinicServiceClient.GetPetByIdAsync(consultation.PetId).Result;

                ConsultationItem item = new()
                {
                    Id = consultation.ConsultationId,
                    ClientName = client.SurName + " " + client.FirstName + " " + client.Patronymic,
                    PetName = pet.Name,
                    ConsultationDate = consultation.ConsultationDate.DateTime,
                    Description = consultation.Description
                };
                consultationItems.Add(item);
            }
            ConsultationListView.ItemsSource = consultationItems;
        }

        private void DeleteConsultationButton_Click(object sender, RoutedEventArgs e)
        {
            ConsultationItem item = (ConsultationItem)ConsultationListView.SelectedItem;
            if (item != null)
            {
                HttpClient httpClient = new();
                ClinicServiceClient clinicServiceClient = new(BASEURL, httpClient);
                clinicServiceClient.DeleteConsultationAsync(item.Id);
            }
            LoadConsultations();
        }

        private void AddConsultationButton_Click(object sender, RoutedEventArgs e)
        {
            AddConsultationDialog consultationDialog = new()
            {
                Owner = this
            };
            consultationDialog.ShowDialog();
            LoadConsultations();
        }

        private void UpdateConsultationButton_Click(object sender, RoutedEventArgs e)
        {
            ConsultationItem item = (ConsultationItem)ConsultationListView.SelectedItem;
            if (item != null)
            {
                EditConsultationDialog consultationDialog = new EditConsultationDialog(item.Id)
                {
                    Owner = this
                };
                consultationDialog.ShowDialog();
            }
            LoadConsultations();
        }
    }
}
