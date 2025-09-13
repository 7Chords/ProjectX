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

        /// <summary>
        /// 新游戏初始化数据
        /// </summary>
        public void InitNewData()
        {
            _m_tbsModel.InitNewData();
        }

        /// <summary>
        /// 新游戏测试临时数据
        /// </summary>
        public void InitTempData()
        {
            _m_tbsModel.InitTempData();
        }
        public void LoadData()
        {

        }
    }
}
