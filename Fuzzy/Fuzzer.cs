using System.Net;
using System.Threading.Tasks.Sources;
using Fuzzy.Content;
    using Fuzzy.Output;
    
    namespace Fuzzy;
    
    internal class Fuzzer : IDisposable
    {
        private readonly IOutputProvider outputProvider;
        private readonly IContentProvider contentProvider;
        
        private readonly HttpClient httpClient;
        private readonly CmdArgs args;
        
        public Fuzzer(
            CmdArgs args,
            IContentProvider contentProvider,
            IOutputProvider outputProvider)
        {
            this.args = args;
            this.contentProvider = contentProvider;
            this.outputProvider = outputProvider;
            this.httpClient = new HttpClient();
        }
    
        public async Task FuzzAsync(CancellationToken cancellationToken)
        {
            Uri baseUrl = new Uri(args.BaseUrl);
            await contentProvider.LoadContentAsync(cancellationToken);
            await FuzzInternalAsync(baseUrl, cancellationToken);
        }

        private async Task FuzzInternalAsync(Uri baseUrl, CancellationToken cancellationToken)
        {
            foreach (string part in contentProvider.GetPathParts())
            {
                Uri url = new Uri(baseUrl, part);
                var response = await httpClient.GetAsync(url, cancellationToken);
                
                    switch (response.StatusCode)
                    {
                        case HttpStatusCode.NotFound:
                            // TODO: Maybe some logging for later 
                            break;
                        default:
                            outputProvider.Output($"{(int)response.StatusCode} - {response.StatusCode} ({response.RequestMessage.RequestUri})");
                            await FuzzInternalAsync(response.RequestMessage.RequestUri, cancellationToken);
                            break;
                    }
                
            }
            
        }
    
        public void Dispose()
        {
            outputProvider.Dispose();
            this.httpClient.Dispose();
        }
    }