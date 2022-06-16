using SEFLink.Model;
using SEFLink.UI.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SEFLink.UI.ViewModels.Dashboard
{
    public class DocumentInfoViewModel : Observable
    {
        #region Fields

        private int _id;
        
        private bool _isSelected;

        private string _endpoint;
        private string _number;
        private string _type;

        private DocStatus _status;

        private string _date;
        private string _tooltipDateTime;
        private string _tooltipText;

        #endregion



        #region Constructor

        public DocumentInfoViewModel(int Id, string Endpoint, string Number, string Type, DateTime DateTime, DocStatus Status)
        {
            this.Id = Id;
            this.IsSelected = false;

            this.Endpoint = Endpoint;
            this.Number = Number;
            this.Type = Type;

            this.Date = DateTime.ToString("dd.MM.yyyy");
            this.TooltipDateTime = DateTime.ToString("dd/MM/yyyy   H:mm");

            this.Status = Status;

            switch (Status)
            {
                case DocStatus.Saved: 
                    TooltipText = "Poslano na SEF.";
                    break;

                case DocStatus.Pending:
                    TooltipText = "Slanje na SEF u toku.";
                    break;

                case DocStatus.Error:
                    TooltipText = "Greška! Nije poslano na SEF.";
                    break;

                case DocStatus.Archived:
                    TooltipText = "Arhivirano.";
                    break;

                default:
                    TooltipText = "Error";
                    break;
            }
            
        }

        #endregion



        #region Properties

        public int Id
        {
            get { return _id; }
            set { _id = value; OnPropertyChanged(); }
        }

        public bool IsSelected
        {
            get { return _isSelected; }
            set { _isSelected = value; OnPropertyChanged(); }
        }

        public string Endpoint
        {
            get { return _endpoint; }
            set { _endpoint = value; OnPropertyChanged(); }
        }

        public string Number
        {
            get { return _number; }
            set { _number = value; OnPropertyChanged(); }
        }

        public string Type
        {
            get { return _type; }
            set { _type = value; OnPropertyChanged(); }
        }

        public DocStatus Status
        {
            get { return _status; }
            set { _status = value; OnPropertyChanged(); }
        }

        public string Date
        {
            get { return _date; }
            set { _date = value; OnPropertyChanged(); }
        }

        public string TooltipDateTime
        {
            get { return _tooltipDateTime; }
            set
            {
                _tooltipDateTime = value;
                OnPropertyChanged();
            }
        }

        public string TooltipText
        {
            get { return _tooltipText; }
            set { _tooltipText = value; OnPropertyChanged(); }
        }

        #endregion
    }
}
