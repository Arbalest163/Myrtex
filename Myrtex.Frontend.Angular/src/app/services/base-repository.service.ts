import { HttpClient, HttpErrorResponse } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { ErrorService } from './error.service';
import { throwError } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class BaseRepositoryService {

  constructor(protected http: HttpClient, protected errorService: ErrorService) { }

  protected errorHandler(error: HttpErrorResponse){
    let errorMessage = error.message
    if(error.status === 0){
      errorMessage = "Ошибка соединения с сервером. Повторите запрос позже."
    }
    this.errorService.handle(errorMessage)
    return throwError(() => errorMessage)
  }
}
