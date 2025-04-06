import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Order, OrderNew } from '../model/Order.model';
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

  getOrders(): Observable<Order[]> {
    return this.http.get<Order[]>(`${this.baseUrl}`);
  }

  approveOrder(orderId: number): Observable<any> {
    return this.http.post(`${this.baseUrl}/${orderId}/approve`, {});
  }

  completeOrder(orderId: number): Observable<any> {
    return this.http.post(`${this.baseUrl}/${orderId}/complete`, {});
  }

  createOrder(order :OrderNew): Observable<any> {
    return this.http.post(`${this.baseUrl}/by-name`, order);
  }
  
  

}
