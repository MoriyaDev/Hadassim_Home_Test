import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { Inventory } from '../model/Inventory.model';

@Injectable({
  providedIn: 'root'
})
export class InventoryService {
  private baseUrl = 'https://localhost:7145/api/Inventory';


  constructor(private http: HttpClient) {}

  getAll(): Observable<Inventory[]> {
    return this.http.get<Inventory[]>(`${this.baseUrl}`);
  }

}
