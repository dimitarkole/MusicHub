export enum MusicLicenseType {
  AllRightsReserved = 0,
  CreativeCommons = 1,
  /*CC = 0,
  CCBY = 1,
  CCBYSA = 2,
  CCBYNDSA = 3,*/
}

export function CreateMusicLicenseType(musicLicenseType: number) {
  if (musicLicenseType == 0) {
    return MusicLicenseType.AllRightsReserved;
  }
  else if (musicLicenseType == 1) {
    return MusicLicenseType.CreativeCommons;
  }

  return MusicLicenseType.AllRightsReserved;
}

// optional: Record type annotation guaranties that 
// all the values from the enum are presented in the mapping
/*export const MusicLicenseTypeMapping: Record<MusicLicenseType, string> = {
  [MusicLicenseType.CC]: "CC",
  [MusicLicenseType.CCBY]: "CC-BY",
  [MusicLicenseType.CCBYSA]: "CC-BY-SA",
  [MusicLicenseType.CCBYNDSA]: "CC-BY-ND-SA",
};

export function CreateMusicLicenseType(musicLicenseType: number) {
  if (musicLicenseType == 0) {
    return MusicLicenseType.CC;
  }
  else if (musicLicenseType == 1) {
    return MusicLicenseType.CCBY;
  }
  else if (musicLicenseType == 2) {
    return MusicLicenseType.CCBYSA;
  }

  return MusicLicenseType.CCBYNDSA;
}*/
