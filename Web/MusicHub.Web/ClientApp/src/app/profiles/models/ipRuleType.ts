export enum IpRuleType {
  All = 0,
  Allow = 1,
  Deny = 2
}

export const IpRuleTypeMapping: Record<IpRuleType, string> = {
  [IpRuleType.All]: "All",
  [IpRuleType.Allow]: "Allow",
  [IpRuleType.Deny]: "Deny",
};

export function CreateIpRuleType(visibleStatus: number) {
  if (visibleStatus == 0) {
    return IpRuleType.All;
  }
  else if (visibleStatus == 1) {
    return IpRuleType.Allow;
  }

  return IpRuleType.Deny;
}
