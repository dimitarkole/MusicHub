import { Song } from './song';

export enum MusicLicenseType {
  CC = 0,
  CCBY = 1,
  CCBYSA = 2,
  CCBYNDSA = 3,
}

// optional: Record type annotation guaranties that 
// all the values from the enum are presented in the mapping
export const MusicLicenseTypeMapping: Record<MusicLicenseType, string> = {
  [MusicLicenseType.CC]: "CC",
  [MusicLicenseType.CCBY]: "CC-BY",
  [MusicLicenseType.CCBYSA]: "CC-BY-SA",
  [MusicLicenseType.CCBYNDSA]: "CC-BY-ND-SA",
};
