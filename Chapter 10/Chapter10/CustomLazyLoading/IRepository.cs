namespace Chapter10.CustomLazyLoading
{
    public interface IRepository<T>
    {
        T GetById(int id);
    }
}