using System;
using System.Windows.Input;

namespace InvoiceApp
{
    public class MainWindowViewModel : ViewModel
    {
        public InvoiceModel Invoice { get; set; }

        public ICommand GenerateDocumentCommand { get; }

        public MainWindowViewModel()
        {
            Invoice = new InvoiceModel();
            GenerateDocumentCommand = new RelayCommand(GenerateDocument);
        }

        public void GenerateDocument(object _)
        {
            Console.WriteLine(Invoice.Name);
        }
    }

    public class InvoiceModel
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime Date { get; set; } = DateTime.Today;
        public decimal Cost { get; set; }

        private decimal _price => Cost - _gst;
        public string Price => _price.ToString("C");

        private decimal _gst => Cost * 0.11m;
        public string Gst => _gst.ToString("C");

        public string Total => Cost.ToString("C");
    }
}
