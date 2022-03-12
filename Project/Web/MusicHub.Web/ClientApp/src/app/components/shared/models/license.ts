import LicenseFile from './licenseFile';
import { LicenseStatus } from './LicenseStatus';
import { OrderMethod } from './OrderMethod';
import User from './user';

export default interface License {
  id: string,
  name: string,
  status: LicenseStatus,
  songLicensesCount: number,
  licenseFilesCount: number,
  CreatedOn: Date,
  user: User,
  licenseFiles: Array<LicenseFile>,
  orderMethod: OrderMethod,
  uploadfiles: Array<File>,
}
