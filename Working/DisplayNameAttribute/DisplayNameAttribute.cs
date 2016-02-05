/*  [Module name]
 *
 *  Copyright (C) 2015 JFactory(manbou404)
 *
 *  This software is released under the MIT License.
 *  http://opensource.org/licenses/mit-license.php
 */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace JFactory.CsSrcLib.Library
{
    //--- ターゲットをすべてに拡張
    [AttributeUsageAttribute(AttributeTargets.All, AllowMultiple = false)]
    public class DisplayNameAttribute : System.ComponentModel.DisplayNameAttribute
    {
        public DisplayNameAttribute(string name)
            : base(name)
        { }

        //--- ほとんどコレでカバー
        public static string Get(ICustomAttributeProvider provider)
        {
            if (provider == null)
                throw new ArgumentNullException("provider");
            var attributes = provider.GetCustomAttributes(typeof(JFactory.CsSrcLib.Library.DisplayNameAttribute), false) as JFactory.CsSrcLib.Library.DisplayNameAttribute[];
            return attributes != null && attributes.Any()
                ? attributes[0].DisplayName
                : null;
        }

        //--- enumは特別
        public static string Get(Enum value)
        {
            var type = value.GetType();
            var name = Enum.GetName(type, value);
            var info = type.GetField(name);
            return JFactory.CsSrcLib.Library.DisplayNameAttribute.Get(info);
        }

        //--- フィールド/プロパティ/戻り値のあるメソッド用
        public static string Get<T>(Expression<Func<T>> expression)
        {
            return JFactory.CsSrcLib.Library.DisplayNameAttribute.Get((LambdaExpression)expression)
                ?? JFactory.CsSrcLib.Library.DisplayNameAttribute.Get(typeof(T));  //--- エラーのときは型情報でリトライ
        }

        //--- 戻り値のないメソッド/イベント用
        public static string Get(Expression<Action> expression)
        {
            return JFactory.CsSrcLib.Library.DisplayNameAttribute.Get((LambdaExpression)expression);
        }

        //--- まとめて処理しちゃえ作戦
        private static string Get(LambdaExpression expression)
        {
            if (expression == null)
                throw new ArgumentNullException("expression");

            var body = expression.Body;
            MemberInfo info = body is MemberExpression ? (body as MemberExpression).Member
                            : body is MethodCallExpression ? (body as MethodCallExpression).Method
                            : null;
            return info == null ? null : JFactory.CsSrcLib.Library.DisplayNameAttribute.Get(info);
        }
    }

}
