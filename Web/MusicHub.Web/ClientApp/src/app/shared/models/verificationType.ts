export enum VerificationType {
  ConfirmEmail = 0,
  ChangePassword = 1,
}

export function CreateVerificationType(verificationType: number) {
  if (verificationType == 0) {
    return VerificationType.ConfirmEmail;
  }
  else if (verificationType == 1) {
    return VerificationType.ChangePassword;
  }

  return VerificationType.ChangePassword;
}
