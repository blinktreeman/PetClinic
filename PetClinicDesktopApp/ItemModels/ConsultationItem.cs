using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetClinicDesktopApp.ViewModels
{
    class ConsultationItem
    {
        public int Id { get; set; }
        public string ClientName { get; set; }
        public string PetName { get; set; }
        public DateTime ConsultationDate { get; set; }
        public string Description { get; set; }
    }
}
