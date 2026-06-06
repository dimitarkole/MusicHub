namespace MusicHub.Data.Models.Enums
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Text;

    public enum ReportMusicType
    {
        NoType = 0,
        SexualContent = 1,
        ViolentOrRepulsiveContent = 2,
        InstillingHatredOrOffensiveContent = 3,
        HarassmentOrAbuseContent = 4,
        InjuryOrDangerousActions = 5,
        ChildAabuse = 6,
        PromotesTerrorism = 7,
        SpamOrMisleadingContent = 8,
        ItViolatesMyRights = 9,
        ProblemWithCaptions = 10,
    }
}
