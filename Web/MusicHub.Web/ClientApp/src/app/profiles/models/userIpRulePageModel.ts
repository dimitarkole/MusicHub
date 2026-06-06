import { UserIpRule } from "./userIpRule";

export interface UserIpRulePageModel {
  userIpRules: UserIpRule[];
  currentPage: number;
  numberOfPages: number;
}
