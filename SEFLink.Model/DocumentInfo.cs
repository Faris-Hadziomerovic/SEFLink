using System;

namespace SEFLink.Model
{
    public class DocumentInfo
    {
        public DocumentInfo() { }

        public DocumentInfo(DocumentInfo x)
        {
            Id = x.Id;
            Sender = x.Sender;
            Receiver = x.Receiver;
            Number = x.Number;
            Date = new DateTime(x.Date.Year, x.Date.Month, x.Date.Day);
            Type = x.Type;
            Status = x.Status;
        }

        public int Id { get; set; }
        public string Sender { get; set; }
        public string Receiver { get; set; }
        public int Number { get; set; }
        public DateTime Date { get; set; }
        public DocType Type { get; set; }
        public DocStatus Status { get; set; }

        public string NumberDisplayMember { get => $"#{Number}/{Date.Year}"; }
        public string DateDisplayMember { get => $"{Date.Day}.{Date.Month}.{Date.Year}."; }
        public string TypeDisplayMember { get => $"{Type}"; }
        public string StatusDisplayMember { get => $"{Status}"; }
    }

    public enum DocStatus
    {
        Saved = 1, Pending, Error, Archived, Deleted
    }

    public enum DocType 
    {
        TIP = 1, KO, KF, KZ, AF
    }
}
