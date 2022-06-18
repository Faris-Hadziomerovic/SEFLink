using System;
using SEFLink.Model;
using SEFLink.UI.Events;

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
            get => _id;
            private set => _id = value;
        }

        public bool IsSelected
        {
            get => _isSelected;
            set
            {
                _isSelected = value;
                OnPropertyChanged();
            }
        }

        public DocStatus Status
        {
            get => _status;
            set
            {
                _status = value;
                OnPropertyChanged();
            }
        }

        public string Endpoint
        {
            get => _endpoint;
            set => _endpoint = value;
        }

        public string Number
        {
            get => _number;
            set => _number = value;
        }

        public string Type
        {
            get => _type;
            set => _type = value;
        }        

        public string Date
        {
            get => _date;
            set => _date = value;
        }

        public string TooltipDateTime
        {
            get => _tooltipDateTime;
            set => _tooltipDateTime = value;
        }

        public string TooltipText
        {
            get => _tooltipText;
            set => _tooltipText = value;
        }

        #endregion
    }
}
