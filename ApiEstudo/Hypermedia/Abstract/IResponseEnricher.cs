namespace ApiEstudo.Hypermedia.Abstract
{
    using Microsoft.AspNetCore.Mvc.Filters;

    public interface IResponseEnricher
    {
        bool CanEnrich(ResultExecutingContext resultExecutingContext);

        Task Enrich(ResultExecutingContext resultExecutingContext);
    }
}
