using PetClinicAPI.Models;

namespace PetClinicAPI.Services
{
    public interface IPetRepository : IRepository<Pet, int>
    {
    }
}
