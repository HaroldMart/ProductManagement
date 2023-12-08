namespace ProductManagement.Core.Application.Interfaces.Services.Generics
{
    public interface IAditionalMethodsService<SaveViewModel, ViewModel> : IGenericService<SaveViewModel, ViewModel>
        where SaveViewModel : class
        where ViewModel : class
    {
        Task<ICollection<ViewModel>> GetAllViewModelWithInclude(string valid, string[] properties);
        Task<SaveViewModel?> GetSaveViewModel(string valid, int id);
        Task<ViewModel?> GetViewModelWithInclude(string valid, int id, string[] collections, string[] references);
    }
}