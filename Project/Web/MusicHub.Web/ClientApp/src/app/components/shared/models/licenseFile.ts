import { LicenseStatus } from './LicenseStatus';
import User from './user';

export default interface LicenseFile {
  id: string,
  path: string,
  licenseId: string,
}
