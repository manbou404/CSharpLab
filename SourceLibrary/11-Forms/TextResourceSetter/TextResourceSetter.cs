/*  [TextResourceSetter]
 *
 *  Copyright (C) 2015 JFactory(manbou404)
 *
 *  This software is released under the MIT License.
 *  http://opensource.org/licenses/mit-license.php
 */

using System.Linq;
using System.Reflection;
using System.Windows.Forms;
using JFactory.CsSrcLib.ExceptionHelper;
using JFactory.CsSrcLib.Library;
using Microsoft.CSharp.RuntimeBinder;

namespace JFactory.CsSrcLib.Forms
{
    /// <summary>Textプロパティを持つコントロールに、リソース文字列を自動で割り当てる</summary>
    public static class TextResourceSetter
    {
        public static ILogger Logger { get; set; }

        /// <summary>リソース文字列を割り当てる</summary>
        /// <param name="target">対象となるコンテナ(FormやUserControlを想定)</param>
        /// <param name="propName">隠れオプション</param>
        public static void Execute(Control target, string propName = "Text")
        {
            Check.ArgumentNull(target, nameof(target));

            // 書き込み可能なTextプロパティを持つオブジェクトを羅列する
            var controls = target.GetType()
                            .GetFields(BindingFlags.NonPublic | BindingFlags.Instance)
                            .Where(x => x.FieldType.GetProperty(propName) != null
                                        && x.FieldType.GetProperty(propName).CanWrite == true)
                            .Select(x => x.GetValue(target))
                            .Where(x => x != null)
                            .Cast<dynamic>();

            foreach (var x in controls)
            {
                try
                {
                    // リソースキーの作成(ターゲット名_コントロール名_Text 
                    // ex. Form1_Label1_Text
                    var key = target.Name + "_" + (string)x.Name + "_" + propName;
                    Logger?.Trace(string.Format("key=[{0}]", key));

                    // リソース文字列を取り出し、コントロールにセットする
                    var value = Properties.Resources.ResourceManager.GetString(key);
                    Logger?.Trace(string.Format("Val=[{0}]", value ?? "(null)"));
                    if (value != null)
                    {
                        x.Text = value;
                        Logger?.Info(string.Format("[{0}] = [{1}]", key, value));
                    }
                }
                catch (RuntimeBinderException ex)
                {
                    Logger?.Info(ex.Message);
                }
            }
        }
    }
}
