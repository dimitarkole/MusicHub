import { ReturnStatement } from "@angular/compiler";

export enum PrivacyStatus {
  Public = 0,
  Protected = 1,
  Private = 2,
}

// optional: Record type annotation guaranties that 
// all the values from the enum are presented in the mapping
export const PrivacyStatusMapping: Record<PrivacyStatus, string> = {
  [PrivacyStatus.Public]: "Public",
  [PrivacyStatus.Protected]: "Protected",
  [PrivacyStatus.Private]: "Private",
};

export function CreatePrivacyStatus(visibleStatus: number) {
  if (visibleStatus == 0) {
    return PrivacyStatus.Public;
  }
  else if (visibleStatus == 1) {
    return PrivacyStatus.Protected;
  }

  return PrivacyStatus.Private;
}
