import { HttpClient, HttpErrorResponse, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable, catchError, throwError } from 'rxjs';
import { IEditEmployee, IEmployee, IGetEmployeesQuery, IPageResponse } from '../models/models';
import { environment } from '../../environments/environment.development';
import { ErrorService } from './error.service';
import { BaseRepositoryService } from './base-repository.service';

const url = `${environment.baseUrl}/employees`

@Injectable({
  providedIn: 'root'
})
export class EmployeesService extends BaseRepositoryService{

  constructor(
    http: HttpClient,
    errorService: ErrorService
    ) {
    super(http, errorService);
  }

  getEmployees(query: IGetEmployeesQuery): Observable<IPageResponse<IEmployee>>{
    return this.http
      .post<IPageResponse<IEmployee>>(url, query)
      .pipe(
        catchError(this.errorHandler.bind(this))
      )
  }

  getEmployeeToEdit(id: number) : Observable<IEditEmployee>{
    return this.http
      .get<IEditEmployee>(`${url}/${id}`)
      .pipe(
        catchError(this.errorHandler.bind(this))
      )
  }

  editEmployee(editEmployee: IEditEmployee){
    return this.http
      .put(`${url}/edit`, editEmployee)
      .pipe(
        catchError(this.errorHandler.bind(this))
      )
  }

  addEmployee(addEmployee: IEditEmployee) : Observable<number>{
    return this.http
      .post<number>(`${url}/create`, addEmployee)
      .pipe(
        catchError(this.errorHandler.bind(this))
      )
  }

  deleteEmployee(id: number) : Observable<any>{
    return this.http
      .delete(`${url}/delete/${id}`)
      .pipe(
        catchError(this.errorHandler.bind(this))
      )
  }
}
