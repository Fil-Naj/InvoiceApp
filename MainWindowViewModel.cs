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
        public decimal Price => Math.Round(Cost, 2);
    }
}
