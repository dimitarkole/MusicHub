export interface SignInModel {
  userId: string,
  requiresTwoFactor: boolean,
  token: string,
}
