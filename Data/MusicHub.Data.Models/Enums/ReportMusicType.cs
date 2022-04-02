using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace MusicHub.Data.Models.Enums
{
    public enum ReportMusicType
    {
        SexualContent = 0,
        ViolentOrRepulsiveContent = 1,
        InstillingHatredOrOffensiveContent = 2,
        HarassmentOrAbuseContent = 3,
        InjuryOrDangerousActions = 4,
        ChildAabuse = 5,
        PromotesTerrorism = 6,
        SpamOrMisleadingContent = 7,
        ItViolatesMyRights = 8,
        ProblemWithCaptions = 9,
    }
}
