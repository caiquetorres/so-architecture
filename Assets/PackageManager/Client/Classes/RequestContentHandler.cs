namespace PackageManager.Client.Classes
{
    public class RequestContentHandler
    {
        public string name;
        public string version;

        public RequestContentHandler(string name, string version)
        {
            this.name = name;
            this.version = version;
        }
    }
}
