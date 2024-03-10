namespace ApiEstudo.Hypermedia.Filters
{
    using ApiEstudo.Hypermedia.Abstract;

    public class HyperMediaFilterOptions
    {
        public List<IResponseEnricher> ContentResponseEnricherList { get; set;} = new List<IResponseEnricher>();
    }
}
