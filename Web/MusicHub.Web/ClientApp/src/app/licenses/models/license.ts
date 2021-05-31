import { OrderMethod } from '../../shared/models/orderMethod';
import User from '../../shared/models/user';
import LicenseFile from './licenseFile';
import { LicenseStatus } from './LicenseStatus';

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
