namespace SEFLink.Model
{
    public class UserSettings
    {
        public UserSettings()
        {
            IncomingFilesLocation = null;
            OutgoingFilesLocation = null;
            API_Key = null;

            AutomaticSending = true;
            AutomaticConversion_IncomingFiles = true;
            AutomaticConversion_OutgoingFiles = true;

            CustomFormat_Incoming = "";
            CustomFormat_Outgoing = "";
        }

        public UserSettings(UserSettings original)
        {
            IncomingFilesLocation = original.IncomingFilesLocation;
            OutgoingFilesLocation = original.OutgoingFilesLocation;
            API_Key = original.API_Key;
            AutomaticSending = original.AutomaticSending;
            AutomaticConversion_IncomingFiles = original.AutomaticConversion_IncomingFiles;
            AutomaticConversion_OutgoingFiles = original.AutomaticConversion_OutgoingFiles;
            CustomFormat_Incoming = original.CustomFormat_Incoming;
            CustomFormat_Outgoing = original.CustomFormat_Outgoing;
        }

        public UserSettings(string incomingFilesLocation, string outgoingFilesLocation, string aPI_Key, bool automaticSending, bool automaticConversion_Incoming, bool automaticConversion_Outgoing, string customFormat_Incoming, string customFormat_Outgoing)
        {
            IncomingFilesLocation = incomingFilesLocation;
            OutgoingFilesLocation = outgoingFilesLocation;
            API_Key = aPI_Key;
            AutomaticSending = automaticSending;
            AutomaticConversion_IncomingFiles = automaticConversion_Incoming;
            AutomaticConversion_OutgoingFiles = automaticConversion_Outgoing;
            CustomFormat_Incoming = customFormat_Incoming;
            CustomFormat_Outgoing = customFormat_Outgoing;
        }

        public string IncomingFilesLocation { get; set; }
        public string OutgoingFilesLocation { get; set; }
        public string API_Key { get; set; }

        public bool AutomaticSending { get; set; }
        public bool AutomaticConversion_IncomingFiles { get; set; }
        public bool AutomaticConversion_OutgoingFiles { get; set; }

        public string CustomFormat_Incoming { get; set; }
        public string CustomFormat_Outgoing { get; set; }
    }
}
