public interface ITemplateProvider
{
    IEntityTemplate GetTemplate(int id);

    T GetTemplate<T>(int id) where T: class, IEntityTemplate;
}