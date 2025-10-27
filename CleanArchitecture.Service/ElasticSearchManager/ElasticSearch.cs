using System.Collections.Generic;
using CleanArchitecture.Service.ElasticSearch;
using Nest;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace CleanArchitecture.Service.ElasticSearchManager
{
    public class ElasticSearch<T>(ElasticClient elasticClient) : IElasticSearch<T> where T : class
    {
        private readonly ElasticClient _elasticClient = elasticClient;
        private string _Index = typeof(T).Name.ToLower();
        public async Task<bool> CreateDocument(T t)
        {
            // Test if has inedex
            var existingIndex = await _elasticClient.Indices.ExistsAsync(_Index);
            if (!existingIndex.Exists)
                await _elasticClient.Indices.CreateAsync(_Index, c => c
            .Map<T>(m => m.AutoMap()));

            // Create Document in specify Index

            var response = await _elasticClient.IndexAsync(t, i => i.Index(_Index));
            if (response.IsValid)
                return true;
            return false;
        }

        public async Task<ICollection<T>> Search(string query)
        {
            var response = await _elasticClient.SearchAsync<T>(s => s
            .Index(_Index)
            .Query(q => q
                .Bool(b => b
                    .Should(
                        // 1. بحث دقيق (Exact Match) - أعلى أولوية
                        sh => sh.QueryString(qs => qs
                            .Query($"\"{query}\"")
                            .DefaultOperator(Operator.And)
                            .Boost(3.0)
                        ),

                        // 2. بحث بالأحرف من البداية (Prefix)
                        sh => sh.QueryString(qs => qs
                            .Query($"{query}*")
                            .AnalyzeWildcard(true)
                            .Boost(2.0)
                        ),

                        // 3. بحث بالأحرف في أي مكان (Wildcard)
                        sh => sh.QueryString(qs => qs
                            .Query($"*{query}*")
                            .AnalyzeWildcard(true)
                            .AllowLeadingWildcard(true)
                            .Boost(1.5)
                        ),

                        // 4. بحث عادي مع Fuzziness
                        sh => sh.MultiMatch(mm => mm
                            .Query(query)
                            .Type(TextQueryType.MostFields)
                            .Fuzziness(Fuzziness.Auto)
                            .Boost(1.0)
                        )
                    )
                    .MinimumShouldMatch(1)
                )
            )
            .Size(50));
            if (response.IsValid) {
                return (ICollection<T>)response.Documents;

            }
            return [];
        }

        public async Task<ICollection<T>> GetAll(int size)
        {
            var response = await _elasticClient.SearchAsync<T>(s => s
                          .Index(_Index)
                          .Query(q => q
                          .MatchAll() ).Size(size));
            if(response.IsValid) return (ICollection<T>)response.Documents;
            return [];
        }
       public async Task<bool> Update(T t, int id)
        {
            var response = await _elasticClient.UpdateAsync<T>(id, u => u
                             .Index(_Index)
                                .Doc(t));
            if(response.IsValid) return true;
            return false;
        }

        public async Task<bool> Delete(int id)
        {
            var response = await _elasticClient.DeleteAsync<T>(id, d => d.Index(_Index));
            if(response.IsValid) return true;
            return false;
        }
    }
}
