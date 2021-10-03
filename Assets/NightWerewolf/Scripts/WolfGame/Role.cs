#region Author
// Copyright (c) 2015 Multiverse
// 
// Namespace:   $NAMESPACE$
// Class:       $CLASS$
// Author:      ZengWeihuan
// CreateTime:  2021-04-18-11:31
#endregion

namespace NightWerewolf
{
    public enum Role {
        InitRole               = 0,
        Villager               ,
        Villager2              ,
        Villager3              ,
        WereWolf               ,
        WereWolf2              ,
        Tanner                 ,
        Hunter                 ,
        Insomanic              ,
        BodyGuard              ,
        DreamWolf              ,
        Sentinel               ,
        DoppelGanger           ,
        AlphaWolf              ,
        MysticWolf             ,
        Minion                 ,
        Mason                  ,
        Seer                   ,
        ApprenticeSeer         ,
        ParanormalInvestigator ,
        Robber                 ,
        Witch                  ,
        TroubleMaker           ,
        VillageIdiot           ,
        Drunk                  ,
        Revealer               ,
        Curator                ,
    }
    
    public enum RoomPlayerState {
        NotReady = 0,
        Ready    = 1,
    }
    
    public static class RoleEx
    {
        public static string ToFriendlyString(this Role _self)
        {
            switch (_self)
            {
                case Role.InitRole:
                    return "初始";
                case Role.Villager:
                case Role.Villager2:
                case Role.Villager3:
                    return "村民";
                case Role.WereWolf:
                case Role.WereWolf2:
                    return "狼人";
                case Role.Tanner:
                    return "皮匠";
                case Role.Hunter:
                    return "猎人";
                case Role.Insomanic:
                    return "失眠者";
                case Role.BodyGuard:
                    return "保镖";
                case Role.DreamWolf:
                    return "贪睡狼";
                case Role.Sentinel:
                    return "守卫";
                case Role.DoppelGanger:
                    return "幽灵";
                case Role.AlphaWolf:
                    return "阿尔法狼";
                case Role.MysticWolf:
                    return "预言狼";
                case Role.Minion:
                    return "爪牙";
                case Role.Seer:
                    return "预言家";
                case Role.ApprenticeSeer:
                    return "见习预言家";
                case Role.ParanormalInvestigator:
                    return "研究员";
                case Role.Robber:
                    return "强盗";
                case Role.Witch:
                    return "女巫";
                case Role.TroubleMaker:
                    return "捣蛋鬼";
                case Role.VillageIdiot:
                    return "白痴";
                case Role.Drunk:
                    return "酒鬼";
                case Role.Revealer:
                    return "揭露者";
                case Role.Curator:
                    return "馆长";
                case Role.Mason: 
                    return "兄弟会";
                default:
                    return "NO!!!!";
            }
        }

        public static string GetDescString(this Role _self)
        {
            switch (_self)
            {
                case Role.InitRole:
                    return "初始";
                case Role.Villager:
                case Role.Villager2:
                case Role.Villager3:
                    return "平民阵营。参与发言和投票，无特殊技能。杀死狼人即可获胜，如果无狼人阵营，则平民互指一人，确保平均每人一票无人死亡方可获胜。";
                case Role.WereWolf:
                case Role.WereWolf2:
                    return "狼人阵营。确认狼同伴，并在投票环节存活下来才可获胜。如果只有一个狼人，狼人可以看到一张场下牌。狼人可以利用这张卡牌的身份来隐藏自己。";
                case Role.Tanner:
                    return "皮匠";
                case Role.Hunter:
                    return "猎人";
                case Role.Insomanic:
                    return "平民阵营。失眠者醒来后，可以看看卡片是否有变化。只有当强盗和捣蛋鬼在游戏中时才使用失眠者。";
                case Role.BodyGuard:
                    return "保镖";
                case Role.DreamWolf:
                    return "贪睡狼";
                case Role.Sentinel:
                    return "守卫";
                case Role.DoppelGanger:
                    return "幽灵";
                case Role.AlphaWolf:
                    return "阿尔法狼";
                case Role.MysticWolf:
                    return "预言狼";
                case Role.Minion:
                    return "狼人阵营。单向知道狼玩家的身份并确保他们都存活即可获胜，自己本身的存亡并不重要。如果场上没有狼玩家，确保自己活下来。";
                case Role.Seer:
                    return "平民阵营。夜晚看另一个玩家的牌或两张场下牌，但不会移动它们。";
                case Role.ApprenticeSeer:
                    return "见习预言家";
                case Role.ParanormalInvestigator:
                    return "研究员";
                case Role.Robber:
                    return "强盗";
                case Role.Witch:
                    return "女巫";
                case Role.TroubleMaker:
                    return "平民阵营。游戏中守夜人牌至少有2张，夜晚可以相互确认身份。";
                case Role.VillageIdiot:
                    return "白痴";
                case Role.Drunk:
                    return "平民阵营或新牌阵营。夜晚酒鬼必须把他的牌与场下任意一张牌进行交换，但不能查看新卡身份。";
                case Role.Revealer:
                    return "揭露者";
                case Role.Curator:
                    return "馆长";
                case Role.Mason: 
                    return "兄弟会";
                default:
                    return "NO!!!!";
            }
        }
        
        public static string GetActHintString(this Role _self)
        {
            switch (_self)
            {
                case Role.InitRole:
                    return "初始";
                case Role.Villager:
                case Role.Villager2:
                case Role.Villager3:
                    return "平民阵营。参与发言和投票，无特殊技能。杀死狼人即可获胜，如果无狼人阵营，则平民互指一人，确保平均每人一票无人死亡方可获胜。";
                case Role.WereWolf:
                case Role.WereWolf2:
                    return "独狼:勾选1名场外并确认";
                case Role.Tanner:
                    return "皮匠";
                case Role.Hunter:
                    return "猎人";
                case Role.Insomanic:
                    return "平民阵营。失眠者醒来后，可以看看卡片是否有变化。只有当强盗和捣蛋鬼在游戏中时才使用失眠者。";
                case Role.BodyGuard:
                    return "保镖";
                case Role.DreamWolf:
                    return "贪睡狼";
                case Role.Sentinel:
                    return "守卫";
                case Role.DoppelGanger:
                    return "幽灵";
                case Role.AlphaWolf:
                    return "阿尔法狼";
                case Role.MysticWolf:
                    return "预言狼";
                case Role.Minion:
                    return "狼人阵营。单向知道狼玩家的身份并确保他们都存活即可获胜，自己本身的存亡并不重要。如果场上没有狼玩家，确保自己活下来。";
                case Role.Seer:
                    return "预言家:勾选1名玩家或者2名场外并确认";
                case Role.ApprenticeSeer:
                    return "见习预言家";
                case Role.ParanormalInvestigator:
                    return "研究员";
                case Role.Robber:
                    return "强盗";
                case Role.Witch:
                    return "女巫:勾选自己以外任意1名玩家和1名场外并确认";
                case Role.TroubleMaker:
                    return "捣蛋鬼:勾选自己以外任意两名玩家并确认";
                case Role.VillageIdiot:
                    return "白痴";
                case Role.Drunk:
                    return "酒鬼:勾选1名场外并确认";
                case Role.Revealer:
                    return "揭露者";
                case Role.Curator:
                    return "馆长";
                case Role.Mason: 
                    return "兄弟会";
                default:
                    return "NO!!!!";
            }
        }
        
        public static bool IsWolf(this Role _self)
        {
            switch (_self)
            {
                case Role.WereWolf:
                case Role.WereWolf2:
                case Role.AlphaWolf:
                case Role.MysticWolf:
                case Role.DreamWolf:
                    return true;
                default:
                    return false;
            }
        }
    }
}