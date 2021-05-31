import License from './license';

export interface LicensePagination {
  licenses: Array<License>,
  numberOfPages: number,
  currentPage: number,
}
