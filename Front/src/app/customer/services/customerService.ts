import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Customer } from '../models/customer.model';

@Injectable({
  providedIn: 'root'
})
export class CustomerService {
  private apiUrl: string ='http://localhost:5010/api/customers';

  constructor(private http: HttpClient) { }

  getCustomers(): Observable<Customer[]> {
    return this.http.get<Customer[]>(this.apiUrl);
  }

  getCustomerById(id: object): Observable<Customer> {
    return this.http.get<Customer>(`${this.apiUrl}/${id}`);
  }

  addCustomer(customer: Customer): Observable<Customer> {
    console.log("add");
    return this.http.post<Customer>(this.apiUrl, customer);
  }

  updateCustomer(id: object, customer: Customer): Observable<void> {
    console.log("edit"); return this.http.put<void>(`${this.apiUrl}/${id}`, customer);
  }

  deleteCustomer(id: object): Observable<void> {
    return this.http.delete<void>(`${this.apiUrl}/${id}`);
  }
}