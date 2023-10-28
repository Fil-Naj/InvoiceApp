using GemBox.Document;
using System;
using System.IO;

namespace InvoiceApp
{
    public class DocumentService
    {
        public DocumentService() 
        {
            ComponentInfo.SetLicense("FREE-LIMITED-KEY");
        }

        public bool TryGenerateInvoice(InvoiceModel model)
        {
            try
            {
                // Load template document.
                var path = Path.Combine(Environment.CurrentDirectory, "InvoiceTemplate.docx");
                var document = DocumentModel.Load(path);

                // Execute find and replace operations.
                document.Content.Replace("{{Date}}", model.Date.ToString("dd/MM/yyyy"));
                document.Content.Replace("{{Name}}", model.Name);
                document.Content.Replace("{{Description}}", model.Description);
                document.Content.Replace("{{Total}}", model.Total);
                document.Content.Replace("{{Price}}", model.Price);
                document.Content.Replace("{{Gst}}", model.Gst);

                // Save document in specified file format.
                var stream = new MemoryStream();
                document.Save(stream, SaveOptions.DocxDefault);

                // Save document to desktop
                var savePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), $"{model.Name}_{model.Date:yyyy_MM_dd}.docx");
                var file = File.Create(savePath);

                stream.WriteTo(file);
                file.Close();

                return true;
            }
            catch (FreeLimitReachedException)
            {
                return false;
            }
        }
    }
}
