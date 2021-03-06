﻿/*
 *Namespace         MisakaTranslator
 *Class             TransLangSettingForm
 *Description       文本翻译选项窗口
 *Author            Hanmin Qi
 *LastModifyTime    2020-03-12
 * ===============================================================
 * 以下是修改记录（任何一次修改都应被记录）
 * 日期   修改内容    作者
 * 2020-03-12       代码注释完成      果冻
 */

using MaterialSkin.Controls;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace MisakaTranslator
{
    public partial class TransLangSettingForm : MaterialForm
    {
        public TransLangSettingForm()
        {
            InitializeComponent();
        }

        private void TransLangSettingForm_Load(object sender, EventArgs e)
        {
            List<KeyValuePair<string, string>> langList = new List<KeyValuePair<string, string>>();

            langList.Add(new KeyValuePair<string, string>("zh","中文"));
            langList.Add(new KeyValuePair<string, string>("en", "英语"));
            langList.Add(new KeyValuePair<string, string>("jp", "日语"));
            langList.Add(new KeyValuePair<string, string>("kr", "韩语"));//百度为kor
            langList.Add(new KeyValuePair<string, string>("ru", "俄语"));
            langList.Add(new KeyValuePair<string, string>("fr", "法语"));//百度为fra

            srcLangCombox.BoxStyle = ComboBoxStyle.DropDownList;
            srcLangCombox.Source = langList;
            dstLangCombox.BoxStyle = ComboBoxStyle.DropDownList;
            dstLangCombox.Source = langList;

            srcLangCombox.SelectedIndex = 2;
            dstLangCombox.SelectedIndex = 0;
        }

        private void ConfirmLangBtn_BtnClick(object sender, EventArgs e)
        {
            if (srcLangCombox.SelectedValue == dstLangCombox.SelectedValue)
            {
                MessageBox.Show("源语言与目标语言不能一致！", "错误");
            }
            else
            {
                IniFileHelper.WriteValue(Environment.CurrentDirectory + "\\GameListInfo.ini", "Game" + Common.GameID,
                    "srcLang", srcLangCombox.SelectedValue);//保存游戏源语言
                IniFileHelper.WriteValue(Environment.CurrentDirectory + "\\GameListInfo.ini", "Game" + Common.GameID,
                    "dstLang", dstLangCombox.SelectedValue);//保存目标翻译语言
                Common.srcLang = srcLangCombox.SelectedValue;
                Common.desLang = dstLangCombox.SelectedValue;

                GameTranslateForm gtlf = new GameTranslateForm();
                if (Common.TransMode != 2) {
                    Common.TextractorHandle.SetGameTransForm(gtlf);
                }

                GameTranslateBackForm.Show(gtlf);
                this.Close();
            }
        }
    }
}
