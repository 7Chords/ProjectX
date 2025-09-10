using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using UnityEngine;

namespace SCFrame
{
    public abstract class SCRefDataCore
    {
        private string _m_assetPath;//资产路径
        private string _m_excelName;//表名

        public SCRefDataCore(string _assetPath, string _objName)
        {
            _m_assetPath = _assetPath;
            _m_excelName = _objName;
        }

        /** 获取加载资源对象的路径 */
        protected abstract string _assetPath { get; }
        protected abstract string _objName { get; }

        private Dictionary<string, string> _m_keyValueMap = new Dictionary<string, string>();
        protected void parseFromTxt(string _string)
        {
            if (string.IsNullOrEmpty(_string))
            {
                Debug.LogError("txt为空");
                return;
            }

            _m_keyValueMap.Clear();
            string[] lineArray = _string.Split('\n');
            for (int i = 0; i < lineArray.Length; i++)
            {
                string line = lineArray[i];
                if (string.IsNullOrEmpty(line))
                {
                    continue;
                }
                string[] lineSplit = line.Split('\t');
                if (lineSplit.Length < 2)
                {
                    Debug.LogError($"general表格式错误：表格{_m_excelName}，第{i}行：\n{line}");
                    continue;
                }

                _m_keyValueMap.Add(lineSplit[0].Trim(), lineSplit[1].Trim());
            }

            try
            {
                _parseFromString();
            }
            catch (Exception e)
            {
                Debug.LogError($"SCRefDataCore:{e}");
                return;
            }
        }

        protected abstract void _parseFromString();

        #region getXXX

        protected string getString(string _key)
        {
            string result;
            if (!_m_keyValueMap.TryGetValue(_key, out result))
            {
                Debug.LogError($"_key不存在{_key}");
                return null;
            }
            return result;
        }

        protected int getInt(string _name)
        {
            string tempValue = getString(_name);
            if (string.IsNullOrEmpty(tempValue))
            {
                Debug.LogError($"{_m_assetPath},{_m_excelName}的字段{_name}为空");
                return 0;
            }
            if (!int.TryParse(tempValue, out int result))
            {
                Debug.LogError($"表\"{_m_assetPath},{_m_excelName}\"\t数据填写错误: {_name},填的{tempValue}不是int");
            }
            return result;
        }

        protected long getLong(string _name, bool _canNull = true)
        {

            string tempValue = getString(_name);
            if (string.IsNullOrEmpty(tempValue))
            {
                if (!_canNull)
                    Debug.LogError($"{_m_assetPath},{_m_excelName}的字段{_name}为空");
                return 0;
            }
            if (!long.TryParse(tempValue, out long result))
            {
                Debug.LogError($"表\"{_m_assetPath},{_m_excelName}\"\t数据填写错误: {_name},填的{tempValue}不是long");
            }
            return result;
        }

        protected bool getBool(string _name, bool _canNull = true)
        {

            string tempValue = getString(_name);
            if (string.IsNullOrEmpty(tempValue))
            {
                if (!_canNull)
                    Debug.LogError($"{_m_assetPath},{_m_excelName}的字段{_name}为空");
                return false;
            }
            if (!bool.TryParse(tempValue, out bool result))
            {
                Debug.LogError($"表\"{_m_assetPath},{_m_excelName}\"\t数据填写错误: {_name},填的{tempValue}不是bool");
            }
            return result;
        }

        protected float getFloat(string _name, bool _canNull = true)
        {

            string tempValue = getString(_name);
            if (string.IsNullOrEmpty(tempValue))
            {
                if (!_canNull)
                    Debug.LogError($"{_m_assetPath},{_m_excelName}的字段{_name}为空");
                return 0;
            }
            if (!float.TryParse(tempValue, NumberStyles.Float, CultureInfo.InvariantCulture, out float result))
            {
                Debug.LogError($"表\"{_m_assetPath},{_m_excelName}\"\t数据填写错误: {_name},填的{tempValue}不是float");
            }
            return result;
        }

        protected Vector2 getVector2(string _name, bool _canNull = true)
        {
            Vector2 v2 = new Vector2();
            string tempValue = getString(_name);
            if (string.IsNullOrEmpty(tempValue))
            {
                if (!_canNull)
                    Debug.LogError($"{_m_assetPath},{_m_excelName}的字段{_name}为空");
                return v2;
            }

            string[] strs = tempValue.Split(new char[] { ';', ':' });
            if (strs.Length < 2)
            {
                Debug.LogError($"表\"{_m_assetPath},{_m_excelName}\"\t数据填写错误: {_name},填的{tempValue}不是Vector2");
                return v2;
            }
            v2.Set(SCCommon.ParseFloat(strs[0]), SCCommon.ParseFloat(strs[1]));
            return v2;
        }

        protected Vector3 getVector3(string _name, bool _canNull = true)
        {
            Vector3 v3 = new Vector3();
            string tempValue = getString(_name);
            if (string.IsNullOrEmpty(tempValue))
            {
                if (!_canNull)
                    Debug.LogError($"{_m_assetPath},{_m_excelName}的字段{_name}为空");
                return v3;
            }

            string[] strs = tempValue.Split(new char[] { ';', ':' });
            if (strs.Length < 3)
            {
                Debug.LogError($"表\"{_m_assetPath},{_m_excelName}\"\t数据填写错误: {_name},填的{tempValue}不是Vector3");
                return v3;
            }

            v3.Set(SCCommon.ParseFloat(strs[0]), SCCommon.ParseFloat(strs[1]), SCCommon.ParseFloat(strs[2]));
            return v3;
        }

        protected Rect getRect(string _name, bool _canNull = true)
        {
            Rect rect = new Rect();
            string tempValue = getString(_name);
            if (string.IsNullOrEmpty(tempValue))
            {
                if (!_canNull)
                    Debug.LogError($"{_m_assetPath},{_m_excelName}的字段{_name}为空");
                return rect;
            }

            string[] strs = tempValue.Split(new char[] { ';', ':' });
            if (strs.Length < 4)
            {
                Debug.LogError($"表\"{_m_assetPath},{_m_excelName}\"\t数据填写错误: {_name},填的{tempValue}不是rect");
                return rect;
            }

            rect.Set(SCCommon.ParseFloat(strs[0]), SCCommon.ParseFloat(strs[1]), SCCommon.ParseFloat(strs[2]), SCCommon.ParseFloat(strs[3]));
            return rect;
        }

        protected List<T> getList<T>(string _name, bool _canNull = true)
        {
            List<T> list = new List<T>();

            string tempValue = getString(_name);
            if (string.IsNullOrEmpty(tempValue))
            {
                if (!_canNull)
                    Debug.LogError($"{_m_assetPath},{_m_excelName}的字段{_name}为空");
                return list;
            }
            string[] strs = tempValue.Split(new char[] { ';', ':' });
            for (var i = 0; i < strs.Length; i++)
            {
                string tempStr = strs[i];
                object value = ParseValue(tempStr, typeof(T));
                if (value == null)
                {
                    Debug.LogError($"表\"{_m_assetPath},{_m_excelName}\"\t数据填写错误: {_name},填的{tempStr}解析失败，完整数据：{value}");
                }
                else
                {
                    list.Add((T)value);
                }
            }


            return list;
        }

        protected List<Vector2> getVector2List(string _name, bool _canNull = true)
        {
            List<Vector2> vector2List = new List<Vector2>();

            string tempValue = getString(_name);
            if (string.IsNullOrEmpty(tempValue))
            {
                if (!_canNull)
                    Debug.LogError($"{_m_assetPath},{_m_excelName}的字段{_name}为空");
                return vector2List;
            }
            string[] strs = tempValue.Split(new string[] { ";" }, StringSplitOptions.RemoveEmptyEntries);
            for (var i = 0; i < strs.Length; i++)
            {
                string[] valueStrs = strs[i].Split(new string[] { ":" }, StringSplitOptions.RemoveEmptyEntries);
                if (valueStrs == null || valueStrs.Length != 2)
                {
                    Debug.LogError($"表\"{_m_assetPath},{_m_excelName}\"\t数据填写错误: {_name},解析失败，完整数据：{valueStrs}");
                }
                else
                {
                    float x = SCCommon.ParseFloat(valueStrs[0]);
                    float y = SCCommon.ParseFloat(valueStrs[1]);
                    vector2List.Add(new Vector2(x, y));
                }
            }
            return vector2List;
        }

        protected List<Vector2Int> getVector2IntList(string _name, bool _canNull = true)
        {
            List<Vector2Int> Vector2IntList = new List<Vector2Int>();

            string tempValue = getString(_name);
            if (string.IsNullOrEmpty(tempValue))
            {
                if (!_canNull)
                    Debug.LogError($"{_m_assetPath},{_m_excelName}的字段{_name}为空");
                return Vector2IntList;
            }
            string[] strs = tempValue.Split(new string[] { ";" }, StringSplitOptions.RemoveEmptyEntries);
            for (var i = 0; i < strs.Length; i++)
            {
                string[] valueStrs = strs[i].Split(new string[] { ":" }, StringSplitOptions.RemoveEmptyEntries);
                if (valueStrs == null || valueStrs.Length != 2)
                {
                    Debug.LogError($"表\"{_m_assetPath},{_m_excelName}\"\t数据填写错误: {_name},解析失败，完整数据：{valueStrs}");
                }
                else
                {
                    int x = SCCommon.ParseInt(valueStrs[0]);
                    int y = SCCommon.ParseInt(valueStrs[1]);
                    Vector2IntList.Add(new Vector2Int(x, y));
                }
            }
            return Vector2IntList;
        }

        protected List<Vector3> getVector3List(string _name, bool _canNull = true)
        {
            List<Vector3> vector3List = new List<Vector3>();

            string tempValue = getString(_name);
            if (string.IsNullOrEmpty(tempValue))
            {
                if (!_canNull)
                    Debug.LogError($"{_m_assetPath},{_m_excelName}的字段{_name}为空");
                return vector3List;
            }
            string[] strs = tempValue.Split(new string[] { ";" }, StringSplitOptions.RemoveEmptyEntries);
            for (var i = 0; i < strs.Length; i++)
            {
                string[] valueStrs = strs[i].Split(new string[] { ":" }, StringSplitOptions.RemoveEmptyEntries);
                if (valueStrs == null || valueStrs.Length != 3)
                {
                    Debug.LogError($"表\"{_m_assetPath},{_m_excelName}\"\t数据填写错误: {_name},解析失败，完整数据：{valueStrs}");
                }
                else
                {
                    float x = SCCommon.ParseFloat(valueStrs[0]);
                    float y = SCCommon.ParseFloat(valueStrs[1]);
                    float z = SCCommon.ParseFloat(valueStrs[2]);
                    vector3List.Add(new Vector3(x, y, z));
                }
            }
            return vector3List;
        }

        // 解析字段值
        protected static object ParseValue(string _value, Type _type)
        {
            try
            {
                if (_value.Equals(string.Empty))
                {
                    if (_type == typeof(string))
                    {
                        return "";
                    }
                    return Activator.CreateInstance(_type, true);
                }
                else
                {
                    _value = _value.Trim();

                    // 枚举 暂不支持
                    if (_type.IsEnum)
                    {
                        Debug.LogError("热更工程里不能直接解析枚举");
                        return null;
                    }

                    // 字符串
                    else if (_type == typeof(string))
                    {
                        return _value;
                    }

                    // 浮点型
                    else if (_type == typeof(float))
                    {
                        if (_value == "0" || _value == "" || _value == string.Empty)
                            return 0f;

                        return float.Parse(_value, CultureInfo.InvariantCulture);
                    }

                    // 整形
                    else if (_type == typeof(int))
                    {
                        if (_value == "")
                            return 0;

                        return int.Parse(_value);
                    }

                    else if (_type == typeof(bool))
                    {
                        return bool.Parse(_value);
                    }

                    else if (_type == typeof(long))
                    {
                        return long.Parse(_value);
                    }
                }
            }
            catch (Exception ex)
            {
                Debug.LogError($"ParseValue type:{_type.ToString()}, value:{_value}, failed: {ex}");
            }
            return null;
        }

        #endregion

    }
}
