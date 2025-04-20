namespace Mat3am_Elhabaib.DataBase.Repo.Interface
{
    public interface IEntityBaserepo<T> where T : class , IEntityBase
    {
        Task<IEnumerable<T>> GetAll();
        Task<T> GetbyId(int id);
        Task Add(T entity);
        Task Update(int Id ,T entity);
        Task DeleteById(int Id);

    }
}
