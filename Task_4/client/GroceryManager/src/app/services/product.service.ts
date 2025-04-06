import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class ProductService {
  private baseUrl = ' https://localhost:7145/api/Product/by-supplier';

  constructor(private http: HttpClient) {}

  getProductsBySupplier(supplierId: number): Observable<any> {
    return this.http.get(`${this.baseUrl}/${supplierId}`, {
    });
  }
  
}
