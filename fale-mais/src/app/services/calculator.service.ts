import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { environment } from 'src/environments/environment';

@Injectable({
  providedIn: 'root'
})
export class CalculatorService {
  private apiUrl = `${environment.apiBaseUrl}/calculate`;
  
  constructor(private http: HttpClient) { }

  calculate(origem: string, destino: string, tempo: number, plano: number): Observable<any> {
    const requestBody = {
      Origin: origem,
      Destination: destino,
      Duration: tempo,
      PlanType: plano
    };
    return this.http.post<any>(this.apiUrl, requestBody);
  }
}