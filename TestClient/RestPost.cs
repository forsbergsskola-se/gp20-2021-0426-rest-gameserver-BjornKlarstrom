using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace TestClient{
    
    public class RestPost : RestBase{
        
        readonly object obj;

        public RestPost(Uri uri, object obj){
            this.uri = uri;
            this.obj = obj;
        }
        
        public async Task<string> Post() {
            return await Request(uri, HttpMethod.Post, obj);
        }
    }
}