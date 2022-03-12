import { Song } from './song';

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
