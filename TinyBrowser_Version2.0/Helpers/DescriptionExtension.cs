namespace TinyBrowser.Helpers{
    public static class DescriptionExtension{
        public static string TryShorten(this string description){
            if(description.Length < 15)
                return description;
            var firstWord = description.Substring(0, 7);
            var secondWord = description.Substring(description.Length - 6, 6);
            return $"{firstWord}..{secondWord}";
        }
    }
}