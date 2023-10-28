using System;
using System.Windows.Input;

namespace InvoiceApp
{
    public class MainWindowViewModel : ViewModel
    {
        private DocumentService _documentService;
        public InvoiceModel Invoice { get; set; }

        private bool _hasError;
        public bool HasError 
        { 
            get
            {
                return _hasError;
            } 
            set
            {
                _hasError = value;
                OnPropertyChanged(nameof(HasError));
            } 
        }

        public ICommand GenerateDocumentCommand { get; }

        public MainWindowViewModel()
        {
            _documentService = new DocumentService();
            Invoice = new InvoiceModel();
            GenerateDocumentCommand = new RelayCommand(GenerateDocument);
        }

        public void GenerateDocument()
        {
            HasError = !_documentService.TryGenerateInvoice(Invoice);
        }
    }

    public class InvoiceModel
    {
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public DateTime Date { get; set; } = DateTime.Today;
        public decimal Cost { get; set; }

        private decimal _price => Cost - _gst;
        public string Price => _price.ToString("C");

        private decimal _gst => Cost * 0.11m;
        public string Gst => _gst.ToString("C");

        public string Total => Cost.ToString("C");
    }
}
