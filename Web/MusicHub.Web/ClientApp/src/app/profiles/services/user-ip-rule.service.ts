import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';

import { UserIpRulePageModel } from '../models/userIpRulePageModel';
import { UserIpRule } from '../models/userIpRule';
import { UserIpRuleFilter } from '../models/userIpRuleFilter';
import { setQueryString } from '../../common/QueryString';

@Injectable({
  providedIn: 'root'
})
export class UserIpRuleService {

  private route: string = 'UserIpRule';

  constructor(private http: HttpClient) { }

  Filter(filter: UserIpRuleFilter, page: number, entitesPerPage: number) {
    return this.http.get<UserIpRulePageModel>(`${this.route}?page=${page}&entitesPerPage=${entitesPerPage}`, {
      params: setQueryString(filter)
    });
  }

  GetById(id: string) {
    return this.http.get<UserIpRule>(`${this.route}/${id}`);
  }

  Create(model: UserIpRule) {
    return this.http.post(`${this.route}`, model);
  }

  Edit(id: string, model: UserIpRule) {
    return this.http.put(`${this.route}/${id}`, model);
  }

  Delete(id: string) {
    return this.http.delete(`${this.route}/${id}`);
  }
}
