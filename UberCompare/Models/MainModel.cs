namespace UberCompare.Models
{
    class MainModel
    {
        public string LeftFileName;
        public string RightFileName;
        public string LogText;
        public bool CharConversion;
        public bool FileExport;
        public FileHandler LeftFileHandler = new FileHandler();
        public FileHandler RightFileHandler = new FileHandler();
        public MainModel()
        {
            LogText = "No files selected...\r\n";
            CharConversion = false;
            FileExport = false;
            LeftFileHandler = new FileHandler();
            RightFileHandler = new FileHandler();
            RightFileName = "";
            LeftFileName = "";
        }
    }
}
