using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FullSearchSamples.Services.Impl
{
    public class FullTextIndexV3
    {
        private readonly Dictionary<string, HashSet<int>> _index = new Dictionary<string, HashSet<int>>();
        private readonly List<string> _content = new List<string>();
        private readonly Lexer _lexer = new Lexer();
        private readonly SimpleSearcherV2 _searcher = new SimpleSearcherV2();


        public void AddStringToIndex(string text)
        {
            int documentId = _content.Count;
            foreach (var token in _lexer.GetTokens(text))
            {
                if (_index.TryGetValue(token, out var set))
                    set.Add(documentId);
                else

                    _index.Add(token, new HashSet<int>() { documentId });
            }
            _content.Add(text);
        }


        public IEnumerable<int> Search(string word)
        {
            word = word.ToLowerInvariant();
            if (_index.TryGetValue(word, out var set))
                return set;
            return Enumerable.Empty<int>();
        }

        public IEnumerable<string> SearchTest(string word)
        {
            var documentList = Search(word);
            foreach (var docId in documentList)
            {
                foreach (var match in _searcher.Search(word, _content[docId]))
                    yield return match;
            }
        }
    }
}
