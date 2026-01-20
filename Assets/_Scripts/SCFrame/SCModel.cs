using GameCore.OW;
using GameCore.TBS;

namespace SCFrame
{
    public class SCModel : Singleton<SCModel>
    {
        private TBSModel _m_tbsModel;
        public TBSModel tbsModel { get { return _m_tbsModel; } }

        private OWModel _m_owModel;
        public OWModel owModel { get { return _m_owModel; } }
        public override void OnInitialize()
        {
            _m_tbsModel = new TBSModel();
            _m_owModel = new OWModel();
        }

    }
}
