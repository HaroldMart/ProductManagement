namespace ProductManagement.Core.Application.Interfaces.Services.Generics
{
    public interface IAditionalMethodsService<SaveViewModel, ViewModel> : IGenericService<SaveViewModel, ViewModel>
        where SaveViewModel : class
        where ViewModel : class
    {
        Task<ICollection<ViewModel>> GetAllViewModelWithInclude(string[] properties);
        Task<SaveViewModel?> GetSaveViewModel(int id);
        Task<ViewModel?> GetViewModelWithInclude(int id, string[] collections, string[] references);
    }
}