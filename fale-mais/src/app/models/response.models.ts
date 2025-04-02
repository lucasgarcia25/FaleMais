export interface CalculationResponse {
  success: boolean;
  data: {
    costWithPlan: number;
    costWithoutPlan: number;
  };
  errorMessage: string;
}

export interface ValidationErrorResponse {
  type: string;
  title: string;
  status: number;
  errors: {
    [key: string]: string[];
  };
  traceId: string;
}
