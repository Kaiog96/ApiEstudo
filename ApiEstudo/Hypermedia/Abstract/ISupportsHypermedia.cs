namespace ApiEstudo.Hypermedia.Abstract
{
    public interface ISupportsHypermedia
    {
        List<HyperMediaLink> Links { get; set; }
    }
}
