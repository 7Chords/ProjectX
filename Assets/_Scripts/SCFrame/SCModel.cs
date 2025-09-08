using GameCore.TBS;

namespace SCFrame
{
    public class SCModel : Singleton<SCModel>
    {
        private TBSModel _m_tbsModel;
        public TBSModel tbsModel { get { return _m_tbsModel; } }

        public override void OnInitialize()
        {
            _m_tbsModel = new TBSModel();
        }
    }
}
