using SEFLink.Model;
using System;

namespace SEFLink.UI.ViewModels.Dashboard
{
    public class DocumentInfoHelper
    {
        #region Helper Methods

        public static DocumentInfoViewModel CreateDocumentInfoViewModel(DocumentInfo documentInfo, DocumentInfoType DocumentInfoType)
        {
            int id = documentInfo.Id;

            string endpoint;

            if (DocumentInfoType == DocumentInfoType.Incoming)                
                endpoint = documentInfo.Sender;            
            else            
                 endpoint = documentInfo.Receiver;


            string number = documentInfo.NumberDisplayMember;
            string type = documentInfo.TypeDisplayMember;
            DocStatus status = documentInfo.Status;

            int year = documentInfo.Date.Year;
            int month = documentInfo.Date.Month;
            int day = documentInfo.Date.Day;

            int hour = DateTime.Now.Hour;
            int minute = DateTime.Now.Minute;
            int second = DateTime.Now.Second;

            DateTime dateTime = new DateTime(year, month, day, hour, minute, second);

            return new DocumentInfoViewModel(id, endpoint, number, type, dateTime, status);
        }


        #endregion
    }

    public enum DocumentInfoType
    {
        Incoming = 1, Outgoing = 2
    }
}
