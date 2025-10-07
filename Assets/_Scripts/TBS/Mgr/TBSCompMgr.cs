using SCFrame;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameCore.TBS
{
    public class TBSCompMgr : TBSSubMgrBase
    {
        public override ETBSSubMgrType tbsSubMgrType => ETBSSubMgrType.COMP;

        private Dictionary<ETBSCompType, TBSCompBase> _m_tbsCompDict;

        public override void OnInitialize()
        {
            _m_tbsCompDict = new Dictionary<ETBSCompType, TBSCompBase>();
            initAllCompModule();
        }

        public override void OnDiscard()
        {
            discardAllComp();
            _m_tbsCompDict.Clear();
            _m_tbsCompDict = null;
        }
        public override void OnResume()
        {
            resumeAllComp();
        }


        public override void OnSuspend() 
        {
            suspendAllComp();
        }


        private void initAllCompModule()
        {
            TBSAttackComp attackComp = new TBSAttackComp();
            attackComp.Initialize();
            _m_tbsCompDict.Add(ETBSCompType.NORMAL_ATTACK, attackComp);

            TBSSkillComp skillComp = new TBSSkillComp();
            skillComp.Initialize();
            _m_tbsCompDict.Add(ETBSCompType.SKILL, skillComp);

            TBSDefendComp defendComp = new TBSDefendComp();
            defendComp.Initialize();
            _m_tbsCompDict.Add(ETBSCompType.DEFEND, defendComp);

            TBSItemComp itemComp = new TBSItemComp();
            itemComp.Initialize();
            _m_tbsCompDict.Add(ETBSCompType.ITEM, itemComp);

            TBSNavigateComp navigateComp = new TBSNavigateComp();
            navigateComp.Initialize();
            _m_tbsCompDict.Add(ETBSCompType.NAVIGATE, navigateComp);
        }

        private void discardAllComp()
        {
            TBSCompBase compBase = null;
            foreach(var pair in _m_tbsCompDict)
            {
                compBase = pair.Value;
                if (compBase == null)
                    continue;
                compBase.Discard();
            }
        }

        private void suspendAllComp()
        {
            TBSCompBase compBase = null;
            foreach (var pair in _m_tbsCompDict)
            {
                compBase = pair.Value;
                if (compBase == null)
                    continue;
                compBase.Suspend();
            }
        }

        private void resumeAllComp()
        {
            TBSCompBase compBase = null;
            foreach (var pair in _m_tbsCompDict)
            {
                compBase = pair.Value;
                if (compBase == null)
                    continue;
                compBase.Resume();
            }
        }

    }
}
