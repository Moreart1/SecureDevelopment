using FullSearchSamples.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FullSearchSamples.Services.Impl
{
    public class DocumentRepository : IDocumentRepository
    {
        #region Services

        private readonly DocumentDbContext _context;

        #endregion

        #region Constructors

        public DocumentRepository(
            DocumentDbContext context)
        {
            _context = context;
        }

        #endregion

        public void LoadDocuments()
        {
            using (var streamReader = new StreamReader(AppContext.BaseDirectory + "data.txt"))
            {
                while (!streamReader.EndOfStream)
                {
                    var doc = streamReader.ReadLine().Split('\t');
                    if (doc.Length > 1 && int.TryParse(doc[0], out int id))
                    {
                        _context.Documents.Add(new Document
                        {
                            Id = id,
                            Content = doc[1]
                        });
                        _context.SaveChanges();
                    }
                }
            }
        }
    }
}
