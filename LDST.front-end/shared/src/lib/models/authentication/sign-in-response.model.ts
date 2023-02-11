export interface SignInResponse {
  token: string;
  userName: string;
  userId: string;
  is2StepVerificationRequired: boolean;
  provider: string | null;
}
