namespace PetClinicAPI.Services
{
    public interface IRepository<T, TId>
    {
        IList<T> GetAll(int id = 0);
        T GetById(TId id);
        int Create(T item);
        int Update(T item);
        int Delete(TId id);
    }
}
