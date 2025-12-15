using SCFrame;
using UnityEditor;
using UnityEngine;

namespace GameCore.Editor
{
    public class GameCheatEditor : EditorWindow
    {
        [MenuItem("MBC编辑器/开发者作弊面板 %#k")]
        public static void ShowWindow()
        {
            GetWindow<GameCheatEditor>("开发者作弊面板");
        }

        // 折叠状态变量
        private bool _m_showItemCheats = false;


        #region 道具相关
        private string _m_getItemIDStr;
        private string _m_getItemAmountStr;
        private string _m_deleteItemIDStr;
        private string _m_deleteItemAmountStr;
        private string _m_getAllItemAmountStr;
        #endregion


        private void OnGUI()
        {
            _m_showItemCheats = EditorGUILayout.Foldout(_m_showItemCheats, "道具作弊相关", true);

            if(_m_showItemCheats)
            {
                EditorGUI.indentLevel++;


                //--------------获得道具--------------
                EditorGUILayout.BeginHorizontal();
                if (GUILayout.Button("获得道具"))
                {
                    SCDataMgr.instance.GetItem(SCCommon.ParseLong(_m_getItemIDStr), SCCommon.ParseLong(_m_getItemAmountStr));
                }

                // 简单文本输入框
                _m_getItemIDStr = EditorGUILayout.TextField("道具ID", _m_getItemIDStr);
                _m_getItemAmountStr = EditorGUILayout.TextField("获得道具数量", _m_getItemAmountStr);
                EditorGUILayout.EndHorizontal();

                //--------------删除道具--------------
                EditorGUILayout.BeginHorizontal();
                if (GUILayout.Button("删除道具"))
                {
                    SCDataMgr.instance.DeleteItem(SCCommon.ParseLong(_m_getItemIDStr), SCCommon.ParseLong(_m_getItemAmountStr));
                }

                // 简单文本输入框
                _m_deleteItemIDStr = EditorGUILayout.TextField("道具ID", _m_deleteItemIDStr);
                _m_deleteItemAmountStr = EditorGUILayout.TextField("删除道具数量", _m_deleteItemAmountStr);
                EditorGUILayout.EndHorizontal();
            }


            EditorGUILayout.HelpBox("这些作弊功能只在游戏运行时有效", MessageType.Info);
        }
    }
}
