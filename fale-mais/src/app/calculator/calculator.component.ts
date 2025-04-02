import { Component, ViewChild } from '@angular/core';
import { MatSnackBar } from '@angular/material/snack-bar';
import { NgForm } from '@angular/forms';
import { CalculatorService } from '../services/calculator.service';
import { CalculationHandlerService } from '../services/calculation-handler.service';
import { CalculationResponse, ValidationErrorResponse } from '../models/response.models';
import { MatTableDataSource } from '@angular/material/table';

interface TariffData {
  origin: string;
  destination: string;
  time: number;
  plan: 'FaleMais30' | 'FaleMais60' | 'FaleMais120' | '';
  withPlan: number;
  withoutPlan: number;
}

@Component({
  selector: 'app-calculator',
  templateUrl: './calculator.component.html',
  styleUrls: ['./calculator.component.css']
})
export class CalculatorComponent {
  @ViewChild('calcForm') calcForm!: NgForm;

  origin: string;
  destination: string;
  time: number;
  plan: 'FaleMais30' | 'FaleMais60' | 'FaleMais120' | '';
  resultWithPlan: number;
  resultWithoutPlan: number;
  resultData: MatTableDataSource<TariffData>;
  displayedColumns: string[];
  tariffs: { [key: string]: string[] };
  availableDestinations: string[];

  constructor(
    private calculatorService: CalculatorService,
    private calculationHandlerService: CalculationHandlerService,
    private snackBar: MatSnackBar
  ) {
    this.origin = '';
    this.destination = '';
    this.time = 0;
    this.plan = '';
    this.resultWithPlan = 0;
    this.resultWithoutPlan = 0;
    this.resultData = new MatTableDataSource<TariffData>([]);
    this.displayedColumns = ['origin', 'destination', 'time', 'plan', 'withPlan', 'withoutPlan'];
    this.tariffs = {
      '011': ['016', '017', '018'],
      '016': ['011'],
      '017': ['011'],
      '018': ['011']
    };
    this.availableDestinations = [];
  }

  onOriginChange(): void {
    this.availableDestinations = this.tariffs[this.origin] || [];
    if (!this.availableDestinations.includes(this.destination)) {
      this.destination = '';
    }
  }

  onDestinationFocus(): void {
    if (!this.origin) {
      this.calculationHandlerService.showError('Por favor, selecione a origem primeiro!');
    }
  }

  async calculate(): Promise<void> {
    const planValue = this.calculationHandlerService.getPlanValue(this.plan);

    this.calculatorService.calculate(this.origin, this.destination, this.time, planValue).subscribe({
      next: (response: CalculationResponse | ValidationErrorResponse) => 
        this.calculationHandlerService.processResult(
          response,
          this.updateResultData.bind(this),
          this.resetForm.bind(this),
          this.calculationHandlerService.handleValidationError.bind(this.calculationHandlerService)
        ),
      error: (error) => this.calculationHandlerService.handleError(error)
    });
  }

  private updateResultData(costWithPlan: number, costWithoutPlan: number): void {
    this.resultWithPlan = costWithPlan;
    this.resultWithoutPlan = costWithoutPlan;
    this.addResultData();
  }

  private addResultData(): void {
    this.resultData.data.unshift({
      origin: this.origin,
      destination: this.destination,
      time: this.time,
      plan: this.plan,
      withPlan: this.resultWithPlan,
      withoutPlan: this.resultWithoutPlan
    });
    this.resultData._updateChangeSubscription();
  }

  private resetForm(): void {
    this.origin = '';
    this.destination = '';
    this.time = 0;
    this.plan = '';
    this.calcForm.resetForm();
  }
}
