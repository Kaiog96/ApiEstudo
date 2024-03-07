namespace ApiEstudo.Model.Base
{
    using System.ComponentModel.DataAnnotations.Schema;

    public class BaseEntity
    {
        [Column("id")]
        public long Id { get; set; }
    }
}
