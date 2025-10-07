using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine.UI;
using UnityEngine;
using SCFrame;

/// <summary>
/// Text拓展 支持游戏启动自翻译
/// </summary>
public class TextSC : Text
{
    private readonly UIVertex[] m_TempVerts = new UIVertex[4];
    protected static readonly Regex NoMeshTextRegex = new Regex("(<b>)|(</b>)|(<i>)|(</i>)|(<size.+?>)|(</size>)|(<color.+?>)|(</color>)|(<u.*?>)|(</u>)|(<a.+?>)|(</a>)|(\n)|( )|(\r)");
    protected static readonly Regex RichTextRegex = new Regex("(<b>)|(</b>)|(<i>)|(</i>)|(<size.+?>)|(</size>)|(<color.+?>)|(</color>)|(<u.*?>)|(</u>)|(<a.+?>)|(</a>)");
    protected static readonly Regex EmptyTextRegex = new Regex("(\n)|( )|(\r)");

    public static Font g_Font;

    //用于存储翻译后的文本，原始key直接存在base.text
    private string _m_sTranslatedText = null;
    //保存的语言
    private ELanguageType _m_eLanguage = ELanguageType.NONE;

    //父节点LayoutGroup
    public List<LayoutGroup> layoutGroupList;

    private Func<string, string> _m_onValueChange;
    public Func<string, string> onValueChange { get { return _m_onValueChange; } set { _m_onValueChange = value; } }

    public override string text
    {
        get
        {
            //Prefab模式，不处理
            if (SCCommon.IsInPrefabStage())
            {
                return base.text;
            }

            //没有运行的时候，不翻译
            if (!Application.isPlaying)
            {
                return base.text;
            }

            //base.text不是需要自动翻译的字符串
            if (string.IsNullOrEmpty(_m_sTranslatedText))
                return base.text;

            if (_m_eLanguage == SCSaveSys.instance.languageType)
            {
                //不需要更新翻译
                return _m_sTranslatedText;
            }
            else
            {
                //需要更新翻译

                //设置语言
                _m_eLanguage = SCSaveSys.instance.languageType;

                if (string.IsNullOrEmpty(base.text))
                    return base.text;

                //开始处理翻译, '#1'
                if (base.text.Length >= 2 && base.text[0].Equals('#'))
                {
                    _m_sTranslatedText = LanguageHelper.instance.GetTextTranslate(base.text);
                }
                else
                {
                    _m_sTranslatedText = base.text;
                }

                return _m_sTranslatedText;
            }
        }
        set
        {
            if (_m_onValueChange != null)
            {
                value = _m_onValueChange(value);
            }

            base.text = value;

            if (null != layoutGroupList && _m_sTranslatedText != value)
            {
                foreach (LayoutGroup layoutGroup in layoutGroupList)
                {
                    if (layoutGroup == null)
                        continue;

                    LayoutRebuilder.ForceRebuildLayoutImmediate((RectTransform)layoutGroup.transform);
                }
            }
            _m_sTranslatedText = value;

        }
    }

    /// <summary>
    ///   <para>Called by the layout system.</para>
    /// </summary>
    public override float preferredWidth
    {
        get
        {
            //Prefab模式，不处理 || 没有运行的时候，不翻译 || base.text不是需要自动翻译的字符串
            if (SCCommon.IsInPrefabStage() || !Application.isPlaying || string.IsNullOrEmpty(_m_sTranslatedText))
            {
                return base.preferredWidth;
            }

            return this.cachedTextGenerator.GetPreferredWidth(this._m_sTranslatedText, this.GetGenerationSettings(Vector2.zero)) / this.pixelsPerUnit;
        }
    }

    /// <summary>
    ///   <para>Called by the layout system.</para>
    /// </summary>
    public override float preferredHeight
    {
        get
        {
            //Prefab模式，不处理 || 没有运行的时候，不翻译 || base.text不是需要自动翻译的字符串
            if (SCCommon.IsInPrefabStage() || !Application.isPlaying || string.IsNullOrEmpty(_m_sTranslatedText))
            {
                return base.preferredHeight;
            }

            return this.cachedTextGenerator.GetPreferredHeight(this._m_sTranslatedText, this.GetGenerationSettings(new Vector2(this.GetPixelAdjustedRect().size.x, 0.0f))) / this.pixelsPerUnit;
        }
    }

    protected override void Awake()
    {
        base.Awake();

        //Prefab模式，不处理
        if (SCCommon.IsInPrefabStage())
        {
            return;
        }

        //没有运行的时候，不翻译
        if (!Application.isPlaying)
        {
            return;
        }
        if (font != g_Font && g_Font != null)
        {
            font = g_Font;
        }

        if (_m_eLanguage != SCSaveSys.instance.languageType)
        {
            //需要更新翻译

            //设置语言
            _m_eLanguage = SCSaveSys.instance.languageType;

            if (string.IsNullOrEmpty(base.text))
                return;

            //开始处理翻译
            if (base.text.Length >= 2 && base.text[0].Equals('#'))
            {
                _m_sTranslatedText = LanguageHelper.instance.GetTextTranslate(base.text);
                this.SetVerticesDirty();
                this.SetLayoutDirty();
            }
        }
    }

    protected override void OnPopulateMesh(VertexHelper toFill)
    {
        if ((UnityEngine.Object)this.font == (UnityEngine.Object)null)
            return;
        this.m_DisableFontTextureRebuiltCallback = true;
        string txt = this.text;

        this.cachedTextGenerator.PopulateWithErrors(txt, this.GetGenerationSettings(this.rectTransform.rect.size), this.gameObject);

        // cachedTextGenerator会判断是否重复，如果重复则不处理。外部没办法判断，所以需要new一个List，不然会二次修改顶点导致
        IList<UIVertex> verts = new List<UIVertex>(this.cachedTextGenerator.verts);

        float num = 1f / this.pixelsPerUnit;

        int count = verts.Count;

        if (count <= 0)
        {
            toFill.Clear();
        }
        else
        {
            Vector2 point = new Vector2(verts[0].position.x, verts[0].position.y) * num;
            Vector2 vector2 = this.PixelAdjustPoint(point) - point;
            toFill.Clear();
            if (vector2 != Vector2.zero)
            {
                for (int index1 = 0; index1 < count; ++index1)
                {
                    int index2 = index1 & 3;
                    this.m_TempVerts[index2] = verts[index1];
                    this.m_TempVerts[index2].position *= num;
                    this.m_TempVerts[index2].position.x += vector2.x;
                    this.m_TempVerts[index2].position.y += vector2.y;
                    if (index2 == 3)
                        toFill.AddUIVertexQuad(this.m_TempVerts);
                }
            }
            else
            {
                for (int index1 = 0; index1 < count; ++index1)
                {
                    int index2 = index1 & 3;
                    this.m_TempVerts[index2] = verts[index1];
                    this.m_TempVerts[index2].position *= num;
                    if (index2 == 3)
                        toFill.AddUIVertexQuad(this.m_TempVerts);
                }
            }
            this.m_DisableFontTextureRebuiltCallback = false;
        }
    }

    #region UnderLine

    /// <summary>
    /// 将行信息中 没有顶点的字符计数去掉，便于便利vertex的信息
    /// </summary>
    /// <param name="_lines"></param>
    /// <returns></returns>
    protected IList<UILineInfo> _getValidCharLineInfos(IList<UILineInfo> _lines)
    {
        string txt = this.text;
        MatchCollection matches = NoMeshTextRegex.Matches(txt);
        IList<UILineInfo> nList = new List<UILineInfo>(_lines);
        for (int k = 0; k < matches.Count; k++)
        {
            var match = matches[k];
            for (int i = 0; i < match.Length; i++)
            {
                for (int j = 0; j < _lines.Count; j++)
                {
                    if (_lines[j].startCharIdx <= match.Index + i)
                        continue;
                    UILineInfo tmp = nList[j];
                    tmp.startCharIdx = tmp.startCharIdx - 1;
                    nList[j] = tmp;
                }
            }
        }
        return nList;
    }

    /// <summary>
    /// 生成下划线的范围信息
    /// </summary>
    /// <param name="_vertexHelper"></param>
    /// <param name="_rects"></param>
    /// <param name="_lines"></param>
    /// <param name="_startCharIndex"></param>
    /// <param name="_endCharIndex"></param>
    protected void _genUnderLineRects(VertexHelper _vertexHelper, ref List<Rect> _rects, IList<UILineInfo> _lines, int _startCharIndex, int _endCharIndex)
    {
        if (_vertexHelper == null || _rects == null)
            return;
        _rects.Clear();
        // 将文本顶点索引坐标加入到包围框
        _vertexHelper.PopulateUIVertex(ref m_TempVerts[0], _startCharIndex * 4);
        var bounds = new Bounds(m_TempVerts[0].position, Vector3.zero);

        int lineIndex = 0;
        int nextLineStartIndex = _endCharIndex;

        // 确认当前字符在哪一行，并取出下一行的首字符作为换行判断
        if (_lines != null && _lines.Count >= 1)
        {
            for (int j = 0; j < _lines.Count; j++)
            {
                if (_startCharIndex < _lines[lineIndex].startCharIdx)
                    break;
                lineIndex++;
                // 如果是超过了最后一行的首字符，说明不会有下一行了，把行判断直接设置最后一个字符
                if (lineIndex >= _lines.Count)
                {
                    nextLineStartIndex = _endCharIndex;
                    break;
                }
                nextLineStartIndex = _lines[lineIndex].startCharIdx;
            }
        }

        int tVertexIndex = _startCharIndex;
        for (int j = _startCharIndex; j < _endCharIndex; j++)
        {
            if (j * 4 + 3 >= _vertexHelper.currentVertCount)
                break;

            _vertexHelper.PopulateUIVertex(ref m_TempVerts[0], j * 4 + 0);
            _vertexHelper.PopulateUIVertex(ref m_TempVerts[1], j * 4 + 1);
            _vertexHelper.PopulateUIVertex(ref m_TempVerts[2], j * 4 + 2);
            _vertexHelper.PopulateUIVertex(ref m_TempVerts[3], j * 4 + 3);

            // 换行重新添加包围框
            if (tVertexIndex >= nextLineStartIndex)
            {
                lineIndex++;
                if (lineIndex >= _lines.Count)
                    nextLineStartIndex = _endCharIndex;
                else
                    nextLineStartIndex = _lines[lineIndex].startCharIdx;

                _rects.Add(new Rect(bounds.min, bounds.size));

                bounds = new Bounds(m_TempVerts[0].position, Vector3.zero);
                bounds.Encapsulate(m_TempVerts[1].position);
                bounds.Encapsulate(m_TempVerts[2].position);
                bounds.Encapsulate(m_TempVerts[3].position);
            }
            else
            {
                // 扩展包围框
                bounds.Encapsulate(m_TempVerts[0].position);
                bounds.Encapsulate(m_TempVerts[1].position);
                bounds.Encapsulate(m_TempVerts[2].position);
                bounds.Encapsulate(m_TempVerts[3].position);
            }

            //解决如果文本超框了，相比没超框的情况，顶点中会存在不显示的顶点，所以需要跳过这些顶点
            float disx = Math.Abs(m_TempVerts[0].position.x - m_TempVerts[1].position.x);
            float disy = Math.Abs(m_TempVerts[0].position.y - m_TempVerts[2].position.y);
            if (disx > 0.0001f && disy > 0.0001f)
                tVertexIndex++;
        }
        _rects.Add(new Rect(bounds.min, bounds.size));

    }


    #endregion

    public void setTextColor(List<Color> _colorList)
    {
        if (_colorList == null || _colorList.Count <= 0)
            return;

        color = _colorList[0];
    }

    public List<Color> getTextColorList()
    {
        List<Color> colorList = new List<Color>();

        colorList.Add(color);

        return colorList;
    }
}