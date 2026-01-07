import { Component, OnInit, inject } from '@angular/core';
import { ProductsService } from '../products.service';
import { DomSanitizer, SafeResourceUrl } from '@angular/platform-browser';
// declare var $:any;

@Component({
  selector: 'app-aboutus',
  templateUrl: './sales-chart.component.html',
  styleUrls: ['./sales-chart.component.css']
})

export class SalesChartComponent implements OnInit {

  private productsService = inject(ProductsService);
  private sanitizer = inject(DomSanitizer);

  pdfUrl: SafeResourceUrl | null = null;
  private currentBlobUrl: string | null = null;

  ngOnInit() {
    this.onViewReport();
  }

  onViewReport() {
    this.productsService.showSalesGraph().subscribe({
      next: (blob: Blob) => {

        // setTimeout(() => {          
        //   const url = URL.createObjectURL(blob);
        //   this.pdfUrl = this.sanitizer.bypassSecurityTrustResourceUrl(url);
        // }, 3000);

        const url = window.URL.createObjectURL(blob);        
        window.open(url, '_blank');
        setTimeout(() => window.URL.revokeObjectURL(url), 10000);

        // const pdfBlob = new Blob([blob], { type: 'application/pdf' });      
        // if (this.currentBlobUrl) {
        //   URL.revokeObjectURL(this.currentBlobUrl);
        // }
        // this.currentBlobUrl = URL.createObjectURL(pdfBlob);
        // this.pdfUrl = this.sanitizer.bypassSecurityTrustResourceUrl(this.currentBlobUrl);        

        // if (this.currentBlobUrl) {
        //   URL.revokeObjectURL(this.currentBlobUrl);
        // }

        // this.currentBlobUrl = URL.createObjectURL(blob);
        // $("#graph").attr('src',this.currentBlobUrl);
        // this.pdfUrl = this.sanitizer.bypassSecurityTrustResourceUrl(this.currentBlobUrl);
      },
      error: (err) => console.error('Error downloading PDF:', err)
    });
  }
}

