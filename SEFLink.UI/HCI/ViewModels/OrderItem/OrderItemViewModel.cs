using System;
using SEFLink.Model;
using SEFLink.UI.Events;

namespace SEFLink.UI.HCI.ViewModels
{
    public class OrderItemViewModel : Observable
    {
        #region Fields

        private int _id;

        private bool _isSelected;

        private string _name;
        private string _price;
        private string _type;

        private DocStatus _status;

        private string _date;
        private string _tooltipDateTime;
        private string _tooltipText;

        #endregion



        #region Constructor

        public OrderItemViewModel(int id, string endpoint, string number, string type, DateTime dateTime, DocStatus status)
        {
            Id = id;
            IsSelected = false;

            Endpoint = endpoint;
            Number = number;
            Type = type;

            Date = dateTime.ToString("dd.MM.yyyy");
            TooltipDateTime = dateTime.ToString("dd/MM/yyyy   H:mm");

            Status = status;

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
            get => _name;
            set => _name = value;
        }

        public string Number
        {
            get => _price;
            set => _price = value;
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
