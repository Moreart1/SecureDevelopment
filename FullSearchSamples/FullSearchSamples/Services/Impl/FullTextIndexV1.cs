using FullSearchSamples.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FullSearchSamples.Services.Impl
{
    public class FullTextIndexV1
    {
        private readonly DocumentDbContext _context;
        private readonly Lexer _lexer = new Lexer();

        public FullTextIndexV1(DocumentDbContext context = null)
        {
            _context = context;
        }

        public void BuildIndex()
        {
            foreach (var document in _context.Documents.ToArray())
            {
                foreach (var token in _lexer.GetTokens(document.Content))
                {
                    var word = _context.Words.FirstOrDefault(w => w.Text == token);
                    int wordId = 0;
                    if (word == null)
                    {
                        var wordObj = new Word
                        {
                            Text = token
                        };
                        _context.Words.Add(wordObj);
                        _context.SaveChanges();
                        wordId = wordObj.Id;
                    }
                    else
                        wordId = word.Id;

                    var wordDocument = _context.WordDocuments.FirstOrDefault(wd => wd.WordId == wordId && wd.DocumentId == document.Id);
                    if (wordDocument == null)
                    {
                        _context.WordDocuments.Add(new WordDocument
                        {
                            DocumentId = document.Id,
                            WordId = wordId
                        });
                        _context.SaveChanges();
                    }
                }
            }
        }
    }
}
