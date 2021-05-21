using System;
using TinyBrowser.Interfaces;

namespace TinyBrowser.Browsers{
    public struct BrowserSetup{
        
        public const int options = 3;
        public static IBrowser GetBrowser(int playerInput){
            return playerInput switch{
                1 => new TcpClientBrowser(),
                2 => new LocalFileBrowser(),
                3 => new HttpRequestBrowser(),
                _ => throw new ArgumentException("Input out of range...")
            };
        }
    }
}