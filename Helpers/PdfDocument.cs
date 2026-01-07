using Microsoft.AspNetCore.Hosting;
using core8_angular_mssql.Entities;
using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;

namespace core8_angular_mssql.Helpers
{
    public class ProductReportDocument : IDocument
    {
        private readonly IWebHostEnvironment _env;
        private readonly IEnumerable<Product> _products;

        public ProductReportDocument(
            IWebHostEnvironment env,
            IEnumerable<Product> products)
        {
            _env = env;
            _products = products;
        }

        public DocumentMetadata GetMetadata() => DocumentMetadata.Default;
        public DocumentSettings GetSettings() => DocumentSettings.Default;

        public void Compose(IDocumentContainer container)
        {
            var imagePath = Path.Combine(_env.WebRootPath, "images", "logo.png");
            container.Page(page =>
            {
                page.Size(PageSizes.A4);
                page.Margin(1, Unit.Centimetre);
                page.PageColor(Colors.White);
                page.Header().Column(column =>
                {
                    if (File.Exists(imagePath))
                    {
                        byte[] imageData = File.ReadAllBytes(imagePath);
                        column.Item().Width(150).Image(imageData);                        
                    }

                    column.Item().Text("Product List Report")
                        .FontSize(20)
                        .SemiBold()
                        .FontColor(Colors.Blue.Medium);

                    column.Item().Text($"As of {DateTime.Now:MMMM dd, yyyy}")
                        .FontSize(12) 
                        .Italic()
                        .FontColor(Colors.Grey.Medium);
                    
                    column.Item().PaddingVertical(5).LineHorizontal(1).LineColor(Colors.Grey.Lighten2);
                });

                page.Content().PaddingTop(10).Table(table =>
                {
                    table.ColumnsDefinition(columns =>
                    {
                        columns.ConstantColumn(30);
                        columns.RelativeColumn();
                        columns.ConstantColumn(50);
                        columns.ConstantColumn(100);
                    });

                    table.Header(header =>
                    {
                        static IContainer CellStyle(IContainer container)
                        {
                            return container.DefaultTextStyle(x => x.SemiBold())
                                            .PaddingVertical(5)
                                            .BorderBottom(1)
                                            .BorderColor(Colors.Black);
                        }                        

                        header.Cell().Element(CellStyle)
                        .DefaultTextStyle(x => x.FontSize(10))
                        .Text("ID");

                        header.Cell().Element(CellStyle)                        
                        .DefaultTextStyle(x => x.FontSize(10))
                        .Text("Description");

                        header.Cell().Element(CellStyle)
                        .DefaultTextStyle(x => x.FontSize(10))
                        .Text("Stocks");

                        header.Cell().Element(CellStyle)
                        .DefaultTextStyle(x => x.FontSize(10))
                        .Text("Price");
                    });

                    foreach (var product in _products)
                    {
                        static IContainer RowStyle(IContainer container)
                        {
                            return container
                                    .PaddingVertical(5)
                                    // .BorderBottom(1)
                                    .DefaultTextStyle(x => x.FontSize(10).FontColor(Colors.Black));                            
                        }                        
                        table.Cell().Element(RowStyle)
                        .DefaultTextStyle(x => x.FontSize(10))
                        .Text(product.Id.ToString());

                        table.Cell().Element(RowStyle)
                        .DefaultTextStyle(x => x.FontSize(10))
                        .Text(product.Descriptions);

                        table.Cell().Element(RowStyle)                            
                            .DefaultTextStyle(x => x.FontSize(10))
                            .Text(product.Qty.ToString("N0"));                        

                        table.Cell().Element(RowStyle)
                        .DefaultTextStyle(x => x.FontSize(10))
                        .Text($"{product.SellPrice:C}");
                    }
                });

                page.Footer().AlignCenter().Text(text =>
                {
                    text.CurrentPageNumber();
                    text.Span(" of ");
                    text.TotalPages();
                });

            });
        }
    }
}