namespace Domain.Maps;

using System.Collections.Generic;
using Domain.Enums;

public static class SkillToGroupMap
{
    public static readonly Dictionary<SkillType, SkillGroupType> skillGroupMap = new Dictionary<
        SkillType,
        SkillGroupType
    >
    {
        { SkillType.Editor, SkillGroupType.Filmmaking },
        { SkillType.Director, SkillGroupType.Filmmaking },
        { SkillType.Producer, SkillGroupType.Filmmaking },
        { SkillType.Screenwriter, SkillGroupType.Filmmaking },
        { SkillType.Cinematographer, SkillGroupType.Filmmaking },
        { SkillType.SoundDesigner, SkillGroupType.Filmmaking },
        { SkillType.ProductionDesigner, SkillGroupType.Filmmaking },
        { SkillType.SpecialEffects, SkillGroupType.Filmmaking },
        { SkillType.ScriptSupervisor, SkillGroupType.Filmmaking },
        { SkillType.CastingDirector, SkillGroupType.Filmmaking },
        { SkillType.MakeupArtist, SkillGroupType.Filmmaking },
        { SkillType.CostumeDesigner, SkillGroupType.Filmmaking },
        { SkillType.BoomOperator, SkillGroupType.Filmmaking },
        { SkillType.PropsManager, SkillGroupType.Filmmaking },
        { SkillType.SoundMixer, SkillGroupType.Filmmaking },
        { SkillType.Runner, SkillGroupType.Filmmaking },
        { SkillType.ProductionAssistant, SkillGroupType.Filmmaking },
        { SkillType.Chreographer, SkillGroupType.Filmmaking },
        { SkillType.AssistantDirector, SkillGroupType.Filmmaking },
        { SkillType.CoProducer, SkillGroupType.Filmmaking },
        { SkillType.HairStylist, SkillGroupType.Filmmaking },
        { SkillType.Grip, SkillGroupType.Filmmaking },
        { SkillType.LocationManager, SkillGroupType.Filmmaking },
        { SkillType.SetDresser, SkillGroupType.Filmmaking },
        { SkillType.StoryBoardArtist, SkillGroupType.Filmmaking },
        { SkillType.Animator, SkillGroupType.Filmmaking },
        { SkillType.LeadActorScreen, SkillGroupType.Acting },
        { SkillType.LeasAcressScreen, SkillGroupType.Acting },
        { SkillType.Extra, SkillGroupType.Acting },
        { SkillType.SupportingActorScreen, SkillGroupType.Acting },
        { SkillType.SupportingActressScreen, SkillGroupType.Acting },
        { SkillType.BackgroundActorActress, SkillGroupType.Acting },
        { SkillType.VoiceActorActress, SkillGroupType.Acting },
        { SkillType.StandIn, SkillGroupType.Acting },
        { SkillType.Dancer, SkillGroupType.Acting },
        { SkillType.LeadActorTheatre, SkillGroupType.Acting },
        { SkillType.LeadActressTheatre, SkillGroupType.Acting },
        { SkillType.MusicalTheatrePerformer, SkillGroupType.Acting },
        { SkillType.SupportingActorTheatre, SkillGroupType.Acting },
        { SkillType.SupportingActressTheatre, SkillGroupType.Acting },
        { SkillType.ActorModel, SkillGroupType.Acting },
    };
}
