import { Injectable } from '@angular/core';
import { environment } from '../../environments/environment.development';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { IDepartmentView } from '../models/models';

const url = `${environment.baseUrl}/departments`

@Injectable({
  providedIn: 'root'
})
export class DepartmentsService {

  constructor(private http: HttpClient) { }

  getDepartments() : Observable<IDepartmentView>{
    return this.http.get<IDepartmentView>(url)
  }
}
