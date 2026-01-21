using GameCore;
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

        public EGameStateType _m_gameStateType;
        public EGameStateType gameStateType
        {
            get { return _m_gameStateType; }
            set { _m_gameStateType = value;}
        }
        public override void OnInitialize()
        {
            _m_tbsModel = new TBSModel();
            _m_owModel = new OWModel();
        }

    }
}
