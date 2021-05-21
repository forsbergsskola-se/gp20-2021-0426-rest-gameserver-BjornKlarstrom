using System.Collections.Generic;

namespace TinyBrowser.Interfaces{
    public interface IPage : ILink{
        public List<ILink> HyperLinks{ get; }
        Dictionary<string, Pages> SubPageDictionary{ get;}
    }
}