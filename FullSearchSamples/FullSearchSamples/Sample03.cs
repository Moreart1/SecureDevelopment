using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Running;
using FullSearchSamples.Services.Impl;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FullSearchSamples
{
    internal class Sample03
    {
        static void Main(string[] args)
        {
            var host = Host.CreateDefaultBuilder(args)
                .ConfigureServices(services =>
                {

                    #region Configure EF DBContext Service (CardStorageService Database)

                    services.AddDbContext<DocumentDbContext>(options =>
                    {
                        options.UseSqlServer(@"data source=DESKTOP-D46VK67\SQLEXPRESS;initial catalog=DocumentsDatabase;User Id=DocumentsDatabaseUser;Password=12345;MultipleActiveResultSets=True;App=EntityFramework");
                    });

                    #endregion

                    #region Configure Repositories

                    #endregion


                })
                .Build();

            //FullTextIndexV1 fullTextIndexV1 = new FullTextIndexV1(host.Services.GetService<DocumentDbContext>());
            //fullTextIndexV1.BuildIndex();
            BenchmarkSwitcher.FromAssembly(typeof(Sample03).Assembly).Run(args, new BenchmarkDotNet.Configs.DebugInProcessConfig());
            BenchmarkRunner.Run<SearchBenchmarkV2>();
        }
    }

    [MemoryDiagnoser]
    [WarmupCount(1)]
    [IterationCount(5)]
    public class SearchBenchmarkV2
    {

        private readonly FullTextIndexV3 _index;
        private readonly string[] _documentsSet;

        [Params("intercontinental", "monday", "not")]
        public string Query { get; set; }

        public SearchBenchmarkV2()
        {
            _documentsSet = DocumentExtractor.DocumentsSet().Take(10000).ToArray();
            _index = new FullTextIndexV3();
            foreach (var item in _documentsSet)
                _index.AddStringToIndex(item);

        }

        [Benchmark(Baseline = true)]
        public void SimpleSearch()
        {
            new SimpleSearcherV2().SearchV3(Query, _documentsSet).ToArray();
        }

        [Benchmark]
        public void FullTextIndexSearch()
        {
            _index.SearchTest(Query).ToArray();
        }

    }
}

