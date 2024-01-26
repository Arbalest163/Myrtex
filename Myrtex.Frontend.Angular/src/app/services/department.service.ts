import { Injectable } from '@angular/core';
import { environment } from '../../environments/environment.development';
import { HttpClient } from '@angular/common/http';
import { Observable, catchError } from 'rxjs';
import { IDepartmentView } from '../models/models';
import { BaseRepositoryService } from './base-repository.service';
import { ErrorService } from './error.service';

const url = `${environment.baseUrl}/departments`

@Injectable({
  providedIn: 'root'
})
export class DepartmentsService extends BaseRepositoryService {

  constructor(
    http: HttpClient, 
    errorService: ErrorService
    ) {
    super(http, errorService);
  }

  getDepartments() : Observable<IDepartmentView>{
    return this.http
    .get<IDepartmentView>(url)
    .pipe(
      catchError(this.errorHandler.bind(this))
    )
  }
}
