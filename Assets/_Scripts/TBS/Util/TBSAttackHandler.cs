namespace GameCore.TBS
{
    public static class TBSAttackHandler
    {

        public static TBSGameAttackInfo CreateTBSAttackInfo()
        {
            return new TBSGameAttackInfo();
        }
        public static void DealAttack(TBSGameAttackInfo _attackInfo)
        {
            if (_attackInfo == null)
                return;
            if (_attackInfo.srcActorList == null)
                return;
            for(int i =0;i<_attackInfo.srcActorList.Count;i++)
            {
                _attackInfo.srcActorList[i].TakeDamage(_attackInfo.srcUseHpList[i]);
                _attackInfo.srcActorList[i].TakeMagic(_attackInfo.srcUseMpList[i]);
            }
            int damage = 0;
            foreach (var actor in _attackInfo.srcActorList)
            {

            }
        }

    }
}
