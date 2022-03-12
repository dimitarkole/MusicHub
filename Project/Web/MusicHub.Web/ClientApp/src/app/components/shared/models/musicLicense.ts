import { Song } from './song';
import License from './license';

export default interface MusicLicense {
  id: string,
  songId: string,
  licenseId: string,
  song: Song,
  license: License
}
