import { ReturnStatement } from "@angular/compiler";

export enum ReportMusicType {
  NoType = 0,
  SexualContent = 1,
  ViolentOrRepulsiveContent = 2,
  InstillingHatredOrOffensiveContent = 3,
  HarassmentOrAbuseContent = 4,
  InjuryOrDangerousActions = 5,
  ChildAbuse = 6,
  PromotesTerrorism = 7,
  SpamOrMisleadingContent = 8,
  ItViolatesMyRights = 9,
  ProblemWithCaptions = 10,
}

// optional: Record type annotation guaranties that 
// all the values from the enum are presented in the mapping
export const ReportMusicTypeMapping: Record<ReportMusicType, string> = {
  [ReportMusicType.NoType]: "No type",
  [ReportMusicType.SexualContent]: "Sexual content",
  [ReportMusicType.ViolentOrRepulsiveContent]: "Violent or repulsive content",
  [ReportMusicType.InstillingHatredOrOffensiveContent]: "Instilling hatred or offensive content",
  [ReportMusicType.HarassmentOrAbuseContent]: "Harassment or abuse content",
  [ReportMusicType.InjuryOrDangerousActions]: "Injury or dangerous actions",
  [ReportMusicType.ChildAbuse]: "Child abuse",
  [ReportMusicType.PromotesTerrorism]: "Promotes terrorism",
  [ReportMusicType.SpamOrMisleadingContent]: "Spam or misleading content",
  [ReportMusicType.ItViolatesMyRights]: "It violates my rights",
  [ReportMusicType.ProblemWithCaptions]: "Problem with captions",
};

export function CreateReportMusicType(reportMusicType: number) {
  alert(reportMusicType);
  if (reportMusicType == 0) return ReportMusicType.NoType;
  else if (reportMusicType == 1) return ReportMusicType.SexualContent;
  else if (reportMusicType == 2) return ReportMusicType.ViolentOrRepulsiveContent;
  else if (reportMusicType == 3) return ReportMusicType.InstillingHatredOrOffensiveContent;
  else if (reportMusicType == 4) return ReportMusicType.HarassmentOrAbuseContent;
  else if (reportMusicType == 5) return ReportMusicType.InjuryOrDangerousActions;
  else if (reportMusicType == 6) return ReportMusicType.ChildAbuse;
  else if (reportMusicType == 7) return ReportMusicType.PromotesTerrorism;
  else if (reportMusicType == 8) return ReportMusicType.SpamOrMisleadingContent;
  else if (reportMusicType == 9) return ReportMusicType.ItViolatesMyRights;
  else if (reportMusicType == 10) return ReportMusicType.ProblemWithCaptions;

  return ReportMusicType.NoType;
}
