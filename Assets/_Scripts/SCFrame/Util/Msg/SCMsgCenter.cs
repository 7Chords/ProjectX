using System;
using System.Collections.Generic;

namespace SCFrame
{

    //带变长的委托模板
    public delegate void MsgRecAction(params object[] _objs);
    public class SCMsgCenter : Singleton<SCMsgCenter>
    {
        private Dictionary<int, List<MsgRecAction>> _m_broadcastDict;
        private Dictionary<int, List<Action>> _m_broadcastActDict;

        private List<MsgRecAction> _m_cacheBroadcastList;
        private List<Action> _m_cacheBroadcastActList;

        public override void OnInitialize()
        {
            _m_broadcastDict = new Dictionary<int, List<MsgRecAction>>();
            _m_broadcastActDict = new Dictionary<int, List<Action>>();
        }

        public override void OnDiscard()
        {
            if(_m_broadcastDict != null)
            {
                _m_broadcastDict.Clear();
                _m_broadcastDict = null;
            }

            if(_m_broadcastActDict != null)
            {
                _m_broadcastActDict.Clear();
                _m_broadcastActDict = null;
            }
        }

        public static void SendMsgAct(int _msg)
        {
            instance.sendMsgAct(_msg);
        }
        private void sendMsgAct(int _msg)
        {
            List<Action> srcList;
            if (_m_broadcastActDict.TryGetValue(_msg, out srcList) && srcList.Count > 0)
            {
                if (_m_cacheBroadcastActList == null)
                    _m_cacheBroadcastActList = new List<Action>();
                else
                    _m_cacheBroadcastActList.Clear();

                _m_cacheBroadcastActList.AddRange(srcList);
                for (int i = 0; i < _m_cacheBroadcastActList.Count; ++i)
                {
                    _m_cacheBroadcastActList[i]();
                }
            }
        }




        public static void SendMsg(int _msg, params object[] _obj)
        {
            instance.sendMsg(_msg, _obj);
        }

        private void sendMsg(int _msg, params object[] _obj)
        {
            List<MsgRecAction> srcList;
            if (_m_broadcastDict.TryGetValue(_msg, out srcList) && srcList.Count > 0)
            {
                if (_m_cacheBroadcastList == null)
                    _m_cacheBroadcastList = new List<MsgRecAction>();
                else
                    _m_cacheBroadcastList.Clear();

                _m_cacheBroadcastList.AddRange(srcList);
                for (int i = 0; i < _m_cacheBroadcastList.Count; ++i)
                {
                    _m_cacheBroadcastList[i](_obj);
                }
            }
        }





        public static void RegisterMsgAct(int _msg, Action _callback)
        {
            instance.registerMsgAct(_msg, _callback);

        }

        private void registerMsgAct(int _msg, Action _callback)
        {
            List<Action> broadcast;
            if (!_m_broadcastActDict.TryGetValue(_msg, out broadcast))
            {
                broadcast = new List<Action>();
                _m_broadcastActDict[_msg] = broadcast;
            }

            if (!broadcast.Contains(_callback))
            {
                broadcast.Add(_callback);
            }
        }





        public static void RegisterMsg(int _msg, MsgRecAction _callback)
        {
            instance.registerMsg(_msg, _callback);
        }

        private void registerMsg(int _msg, MsgRecAction _callback)
        {
            List<MsgRecAction> broadcast;
            if (!_m_broadcastDict.TryGetValue(_msg, out broadcast))
            {
                broadcast = new List<MsgRecAction>();
                _m_broadcastDict[_msg] = broadcast;
            }

            if (!broadcast.Contains(_callback))
            {
                broadcast.Add(_callback);
            }
        }





        public static void UnregisterMsg(int _msg, MsgRecAction _callback)
        {
            instance.unregisterMsg(_msg, _callback);
        }

        private void unregisterMsg(int _msg, MsgRecAction _callback)
        {
            List<MsgRecAction> broadcast;
            if (!_m_broadcastDict.TryGetValue(_msg, out broadcast))
            {
                return;
            }

            broadcast.Remove(_callback);

        }




        public static void UnregisterMsgAct(int _msg, Action _callback)
        {
            instance.unregisterMsgAct(_msg, _callback);
        }

        private void unregisterMsgAct(int _msg, Action _callback)
        {
            List<Action> broadcast;
            if (!_m_broadcastActDict.TryGetValue(_msg, out broadcast))
            {
                return;
            }

            broadcast.Remove(_callback);
        }
    }
}