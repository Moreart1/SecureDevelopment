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
    internal class Sample02
    {
        static void Main(string[] args)
        {
            var host = Host.CreateDefaultBuilder(args)
                .ConfigureServices(services => {

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

            //var documentsSet = DocumentExtractor.DocumentsSet().Take(10000).ToArray();
            //new SimpleSearcher().Search("Monday", documentsSet);
            //new SimpleSearcherV2().SearchV1("Monday", documentsSet);
            //new SimpleSearcherV2().SearchV2("Monday", documentsSet);

            BenchmarkSwitcher.FromAssembly(typeof(Sample02).Assembly).Run(args, new BenchmarkDotNet.Configs.DebugInProcessConfig());
            BenchmarkRunner.Run<SearchBenchmarkV1>();
        }
    }

    [MemoryDiagnoser]
    [WarmupCount(1)]
    [IterationCount(5)]
    public class SearchBenchmarkV1
    {
        private readonly string[] _documentsSet;

        public SearchBenchmarkV1()
        {
            _documentsSet = DocumentExtractor.DocumentsSet().Take(10000).ToArray();
        }

        [Benchmark]
        public void SimpleSearch()
        {
            new SimpleSearcherV2().SearchV3("Monday", _documentsSet).ToArray();
        }

    }
}

