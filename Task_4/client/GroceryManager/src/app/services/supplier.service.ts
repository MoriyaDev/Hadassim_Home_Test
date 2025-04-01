import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Router } from '@angular/router';
import { Observable } from 'rxjs';
import { Supplier } from '../model/supplier.model';

@Injectable({
  providedIn: 'root'
})
export class SupplierService {
  private apiUrl ='https://localhost:7145/api/Supplier'

  constructor(
    private _http: HttpClient,
    private _router: Router,

  ) { }

  login(companyName :string,password :string):Observable<Supplier>
  {
    return this._http.post<Supplier>(`${this.apiUrl}/login`,{companyName,password});
  }
  register(supplier: Supplier): Observable<Supplier> {
    return this._http.post<Supplier>(`${this.apiUrl}/register`, supplier);
  }
  
}
