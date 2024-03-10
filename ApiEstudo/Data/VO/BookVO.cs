namespace ApiEstudo.Data.VO
{
    using ApiEstudo.Hypermedia;
    using ApiEstudo.Hypermedia.Abstract;
    using System.Collections.Generic;

    public class BookVO : ISupportsHypermedia
    {
        public long Id { get; set; }

        public string Title { get; set; }

        public string Author { get; set; }

        public decimal Price { get; set; }

        public DateTime LaunchDate { get; set; }

        public List<HyperMediaLink> Links { get; set; } = new List<HyperMediaLink>();
    }
}
