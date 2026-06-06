import { IpRuleType } from './ipRuleType';

export interface UserIpRule{
  id: string;
  pattern: string;
  ruleType: IpRuleType;
  description: string;
  isEnabled: boolean;
}
