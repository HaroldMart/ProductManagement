namespace ProductManagement.Core.Application.Interfaces.Services.Generics
{
    public interface IGenericService<SaveViewModel, ViewModel>
        where SaveViewModel : class
        where ViewModel : class
    {
        Task<ICollection<ViewModel>> GetAllViewModel(string valid);
        Task<ViewModel?> GetViewModel(string valid, int id);
        Task<string> Add(SaveViewModel entity);
        Task<string> Update(SaveViewModel entity);
        Task<string> Delete(int id);
    }
}
