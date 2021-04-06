import { ReturnStatement } from "@angular/compiler";

export enum VisibleStatus {
  Public = 0,
  Hidden = 1,
  OnlyMe = 2,
}

// optional: Record type annotation guaranties that 
// all the values from the enum are presented in the mapping
export const VisibleStatusMapping: Record<VisibleStatus, string> = {
  [VisibleStatus.Public]: "Public",
  [VisibleStatus.Hidden]: "Hidden",
  [VisibleStatus.OnlyMe]: "Only me",
};

export function CreateVisibleStatus(visibleStatus: number) {
  if (visibleStatus == 0) {
    return VisibleStatus.Public;
  }
  else if (visibleStatus == 1) {
    return VisibleStatus.Hidden;
  }

  return  VisibleStatus.OnlyMe;
}
