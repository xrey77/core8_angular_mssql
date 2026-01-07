import { Component, OnInit, inject } from '@angular/core';
import { ProductsService } from '../products.service';
import { DomSanitizer, SafeResourceUrl } from '@angular/platform-browser';

@Component({
  selector: 'app-aboutus',
  templateUrl: './product-report.component.html',
  styleUrls: ['./product-report.component.css']
})
export class ProductReportComponent implements OnInit {

  private productsService = inject(ProductsService);
  private sanitizer = inject(DomSanitizer);

  pdfUrl: SafeResourceUrl | null = null;
  private currentBlobUrl: string | null = null;

  ngOnInit() {
    this.onViewReport();
  }

  onViewReport() {
    this.productsService.showPdfReport().subscribe({
      next: (blob: Blob) => {
        if (this.currentBlobUrl) {
          URL.revokeObjectURL(this.currentBlobUrl);
        }

        this.currentBlobUrl = URL.createObjectURL(blob);
        this.pdfUrl = this.sanitizer.bypassSecurityTrustResourceUrl(this.currentBlobUrl);
      },
      error: (err) => console.error('Error downloading PDF:', err)
    });
  }
}