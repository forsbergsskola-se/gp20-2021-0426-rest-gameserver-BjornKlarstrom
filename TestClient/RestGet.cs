using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace TestClient{
    
    public class RestGet : RestBase{

        public RestGet(Uri uri){
            this.uri = uri;
        }
        
        public async Task<string> Get(){
            return await Request(uri, HttpMethod.Get,null);
        }
    }
}