import { Injectable } from '@angular/core';
import { MatSnackBar } from '@angular/material/snack-bar';
import { CalculationResponse, ValidationErrorResponse } from '../models/response.models';

@Injectable({
  providedIn: 'root'
})
export class CalculationHandlerService {
  constructor(private snackBar: MatSnackBar) {}

  processResult(
    response: CalculationResponse | ValidationErrorResponse,
    updateResultData: (costWithPlan: number, costWithoutPlan: number) => void,
    resetForm: () => void,
    handleValidationError: (response: ValidationErrorResponse) => void
  ): void {
    if ('success' in response && response.success) {
      updateResultData(response.data.costWithPlan, response.data.costWithoutPlan);
      resetForm();
    } else {
      if ('errors' in response) {
        handleValidationError(response);
      }
    }
  }

  handleValidationError(response: ValidationErrorResponse): void {
    const errorMessages = Object.values(response.errors).flat().join(' ');
    this.showError(errorMessages);
  }

  handleError(error: any): void {
    const errorMessages = error?.error?.errors ? Object.values(error.error.errors).flat().join(' ') : error.message;
    this.showError('Erro ao calcular: ' + errorMessages);
  }

  showError(message: string): void {
    this.snackBar.open(message, 'Fechar', {
      duration: 5000,
      panelClass: ['error-snackbar']
    });
  }

  getPlanValue(plan: 'FaleMais30' | 'FaleMais60' | 'FaleMais120' | ''): number {
    const planMap: { [key in 'FaleMais30' | 'FaleMais60' | 'FaleMais120']: number } = {
      'FaleMais30': 30,
      'FaleMais60': 60,
      'FaleMais120': 120
    };
    return plan ? planMap[plan] : 0;
  }
}
