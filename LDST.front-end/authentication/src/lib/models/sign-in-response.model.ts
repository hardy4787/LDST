export interface SignInResponse {
  token: string;
  userName: string;
  is2StepVerificationRequired: boolean;
  provider: string | null;
}
