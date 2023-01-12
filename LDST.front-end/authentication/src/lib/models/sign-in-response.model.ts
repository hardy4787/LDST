export interface SignInResponse {
  token: string;
  is2StepVerificationRequired: boolean;
  provider: string | null;
}
