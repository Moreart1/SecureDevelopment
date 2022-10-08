using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FullSearchSamples.Entity
{
    [Table("WordDocuments")]
    [Index(nameof(WordId))]
    [Index(nameof(WordId), nameof(DocumentId), IsUnique = true)]
    public class WordDocument
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [ForeignKey(nameof(Word))]
        public int WordId { get; set; }

        [ForeignKey(nameof(Document))]
        public int DocumentId { get; set; }

        public virtual Document Document { get; set; }
        public virtual Word Word { get; set; }
    }
}
