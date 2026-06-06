import { IpRuleType } from './ipRuleType';

export interface UserIpRuleFilter {
  pattern?: string;
  ruleType?: IpRuleType;
}
