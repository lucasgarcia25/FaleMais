// calculator.service.ts
import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { catchError } from 'rxjs/operators';
import { environment } from 'src/environments/environment';
import { CalculationResponse, ValidationErrorResponse } from '../models/response.models';

@Injectable({
  providedIn: 'root'
})
export class CalculatorService {
  private apiUrl = `${environment.apiBaseUrl}/calculate`; 

  constructor(private http: HttpClient) { }

  calculate(origem: string, destino: string, tempo: number, plano: number): Observable<CalculationResponse | ValidationErrorResponse> {
    return this.http.post<CalculationResponse | ValidationErrorResponse>(this.apiUrl, { origem, destino, tempo, plano })
      .pipe(
        catchError(this.handleError)
      );
  }

  private handleError(error: any): Observable<never> {
    throw new Error(error.message || 'Erro desconhecido');
  }
}
