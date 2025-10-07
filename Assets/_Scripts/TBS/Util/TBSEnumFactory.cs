using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameCore.TBS
{
    public static class TBSEnumFactory
    {

        /// <summary>
        /// 根据职业类型创建对应的Actor
        /// </summary>
        /// <param name="_professionTyep"></param>
        /// <param name="_mono"></param>
        /// <returns></returns>
        public static TBSActorBase CreateTBSActorByProfession(EProfessionType _professionTyep,TBSActorMonoBase _mono)
        {
            TBSActorBase res = null;
            switch (_professionTyep)
            {
                case EProfessionType.WARRIOR:
                    res = new TBSWarriorActor(_mono);
                    break;
                case EProfessionType.MAGE:
                    res = new TBSMageActor(_mono);
                    break;
                case EProfessionType.TROLL:
                    res = new TBSTrollActor(_mono);
                    break;
                case EProfessionType.MONSTER:
                    res = new TBSMonsterActor(_mono);
                    break;
                default:
                    res = null;
                    break;
            }
            return res;
        }
    }
}
