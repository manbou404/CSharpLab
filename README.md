CSharpLab (C#の俺々実験室)
==============================

* CSharpLab
  * SourceLibrary
    * 00-TemplateCS
    * 10-CSharp
      * BasicLibrary (例外チェック,など  
        As(T), True/False, Clamp(T), IsRange(T), ForEach(T), Indexed(T)
      * BoxArray (行/列の挿入/削除をサポートする2次元配列
    * 11-Forms
      * GroupEnabledCheckBox (有効/無効CheckBox付きのGroupBox
      * PropertyGridHelper (Toolbarの取得/Controlセレクタ付きPropertyGrid
      * TextResourceSetter (Controlと同名の文字列リソースを自動で設定する
    * 12-WPF
      * nothng
    * 13-Win32
      *  ConsoleWindow
    * 90-MISC
      * nothing
* Working (ビルドできないかも

BasicLibrary
============

### string.IsEmpty()
string.IsNullOrWhiteSpaceのラップ
```
    var text = "abc";
    //if (string.IsNullOrWhiteSpace(text)) ...
    if (text.IsEmpty()) ...
```

### bool.True(Action) / bool.False(Action)
bool型のチェックとコードブロックを１行で記述
(コードブロックが複数行になる時は、通常の記述を推奨)
```
  var flag = true;
  //if (flag) 
  //{
  //    ...
  //}
  flag.True(() => ...);
```

### object.As<Type>(Action)
キャストのチェックとコードブロックを１行で記述
(コードブロックが複数行になる時は、通常の記述を推奨)
```
  private void Button1_Click(object sender, EventArgs e)
  {
    sender<Button>(() => xxx);
```

### Type Clamp(min,max) / IsRange(min,max)
min <= value <= max
```
  if (value.IsRange(0, 10)) ...
  
  value = value.Clamp(0, 10);
```

### (Linq extension) FOrEach, Indexed, 

### Indexer<T>, Indexer2<T>
クラス内に、インデックス付きプロパティを作成
* setterを指定しなければ読み込み専用となる
* 列挙もサポートしているが、yeildでひとつづつ返すため遅い
```
  public Indexer<int> Prop1 { get; }
  prop1 = new Indexer<int>(Action count, getter, setter)
  public Indexer2<int> Prop2 { get; }
  Prop2 = new Indexer2<int>(Action width, height, getter, setter);
```

### Check.Do<>, Check.ArgumentXXX

```
  //if (obj == null)
  //{
  //  throw new ArgumentNullException(nameof(obj));
  //}
  Check.ArgumentNull(obj, nameof(obj));

  //if (value < 0 || 10 < value)
  //{
  //  throw new ArgumentOutOfRangeException(nameof(PropName));
  //}
  Check.ArgumentOutOfRange(value.IsRange(0, 10), nameof(PropName));

  //if (xxxx)
  //{
  //  throw new InvalidOperationException("not supported.")
  //}
  Check.Do<InvalidOperationException>(xxxx, "not supported.");
```


