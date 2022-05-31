import { HttpParams } from '@angular/common/http';

export function setQueryString(params: object) {
  let httpParams = new HttpParams();
  if (params) {
    for (const key of Object.keys(params)) {
      if (params[key]) {
        httpParams = httpParams.set(key, params[key]);
      }
    }
  }
  return httpParams;
}
