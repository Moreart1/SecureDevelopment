using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FullSearchSamples.Services.Impl
{
    public class SimpleSearcherV2
    {
        #region FullTextIndexV3

        // Добавим возможность поиска просто в одном документе
        public IEnumerable<string> Search(string word, string item)
        {
            int pos = 0;
            while (true)
            {
                pos = item.IndexOf(word, pos);
                if (pos >= 0)
                {
                    yield return PrettyMatchV2(item, pos);
                }
                else
                    break;
                pos++;
            }
        }

        #endregion

        #region Search V1

        public void SearchV1(string word, IEnumerable<string> data)
        {
            foreach (var item in data)
            {
                if (item.Contains(word, StringComparison.InvariantCultureIgnoreCase))
                    Console.WriteLine(PrettyMatchV1(word, item));
            }
        }
        private string PrettyMatchV1(string word, string text)
        {
            int pos = text.IndexOf(word);
            var start = Math.Max(0, pos - 50);
            int end = Math.Min(start + 100, text.Length - 1);
            return $"{(start == 0 ? "" : "...")}{text.Substring(start, end - start)}{(end == text.Length - 1 ? "" : "...")}";
        }

        #endregion


        #region Search V2

        public void SearchV2(string word, IEnumerable<string> data)
        {
            foreach (var item in data)
            {
                Console.WriteLine("==============");
                int pos = 0;
                while (true)
                {
                    pos = item.IndexOf(word, pos);
                    if (pos >= 0)
                    {
                        Console.WriteLine(PrettyMatchV2(item, pos));
                    }
                    else
                        break;
                    pos++;
                }
            }

        }

        public IEnumerable<string> SearchV3(string word, IEnumerable<string> data)
        {
            foreach (var item in data)
            {
                int pos = 0;
                while (true)
                {
                    pos = item.IndexOf(word, pos);
                    if (pos >= 0)
                    {
                        yield return PrettyMatchV2(item, pos);
                    }
                    else
                        break;
                    pos++;
                }
            }
        }

        public string PrettyMatchV2(string text, int pos)
        {
            var start = Math.Max(0, pos - 50);
            int end = Math.Min(start + 100, text.Length - 1);
            return $"{(start == 0 ? "" : "...")}{text.Substring(start, end - start)}{(end == text.Length - 1 ? "" : "...")}";
        }



        #endregion
    }
}
