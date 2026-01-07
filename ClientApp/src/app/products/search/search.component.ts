import { HttpErrorResponse } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormControl, FormGroup, NgForm, Validators } from '@angular/forms';
import { ProductsService } from 'src/app/products.service';

@Component({
  selector: 'app-search',
  templateUrl: './search.component.html',
  styleUrls: ['./search.component.css']
})
export class SearchComponent implements OnInit {

  search: string = '';
  searchForm: any;
  searchKey: any;
  products: any;
  message: string = '';

  constructor(
    private productsService: ProductsService
  ) { }

  ngOnInit(): void {
  }

  submitSearchForm(searchForm: NgForm) {
    if (searchForm.valid) {
      this.searchKey = searchForm.value;
      
      this.productsService.sendSearchRequest(this.searchKey).subscribe({
        next: (res: any) => {
          // 'res' is already a JavaScript object parsed from JSON
          this.products = res.products;
          // this.message = res.message;
        },
        error: (error: any) => {
          // Access nested error messages if the server returns a JSON error body
          this.message = error.error?.message || 'No record(s) found.';
          setTimeout(() => {
            this.message = '';
            this.products = [];
          }, 3000);

        },
      });
    }
  }

}
