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
        Error, Pending, Saved, Archived, Deleted
    }

    public enum DocType 
    {
        TIP, KO, KF, KZ, AF
    }

    public class DocDate
    {
        public DocDate(int day, int month, int year)
        {
            Day = day;
            Month = month;
            Year = year;
        }

        public DocDate(DocDate x)
        {
            Day = x.Day;
            Month = x.Month;
            Year = x.Year;
        }

        public override string ToString()
        {
            return $"{Day}.{Month}.{Year}.";
        }
        
        public string ToString(char separator)
        {
            return $"{Day}{separator}{Month}{separator}{Year}{separator}";
        }

        public int Day { get; set; }
        public int Month { get; set; }
        public int Year { get; set; }
    }
}
