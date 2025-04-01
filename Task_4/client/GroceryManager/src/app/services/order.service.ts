import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Order } from '../model/Order.model';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class OrderService {
  private baseUrl = 'https://localhost:7145/api/Order';


  constructor(private http: HttpClient) {}

  getOrdersBySupplier(supplierId: number): Observable<Order[]> {
    return this.http.get<Order[]>(`${this.baseUrl}/by-supplier/${supplierId}`);
  }

  approveOrder(orderId: number): Observable<any> {
    return this.http.post(`${this.baseUrl}/approve/${orderId}`, {});
  }


}
