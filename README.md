# QRCodeLibVB
QRCodeLibVBは、Visual Basicで書かれたQRコード生成ライブラリです。  
JIS X 0510に基づくモデル２コードシンボルを生成します。

## 特徴
- 数字・英数字・8ビットバイト・漢字モードに対応しています
- 分割QRコードを作成可能です
- 1bppまたは24bpp BMPファイル(DIB)へ保存可能です
- SVG形式で保存可能です
- 1bppまたは24bpp Imageオブジェクトとして取得可能です 
- 画像の配色(前景色・背景色)を指定可能です
- 8ビットバイトモードでの文字コードを指定可能です

## クイックスタート
QRCodeLibプロジェクト、またはビルドした QRCodeLib.dll を参照設定してください。

## 使用方法
### 例１．単一シンボルで構成される(分割QRコードではない)QRコードの、最小限のコードを示します。
```vbnet
Imports Ys.QRCode
Imports System.Drawing

Public Sub Example()
    Dim symbols As Symbols = New Symbols()
    symbols.AppendText("012345abcdefg")

    Dim image As Image = symbols(0).GetImage()

End Sub
```

### 例２．誤り訂正レベルを指定する
Symbolsクラスのコンストラクタ引数に、ErrorCorrectionLevel列挙型の値を設定します。
```vbnet
Dim symbols As Symbols = New Symbols(ErrorCorrectionLevel.H)
```

### 例３．型番の上限を指定する
Symbolsクラスのコンストラクタで設定します。
```vbnet
Dim symbols As Symbols = New Symbols(maxVersion:=10)
```

### 例４．8ビットバイトモードで使用する文字コードを指定する
Symbolsクラスのコンストラクタで設定します。
```vbnet
Dim symbols As Symbols = New Symbols(byteModeEncoding:="utf-8")
```

### 例５．分割QRコードを作成する
Symbolsクラスのコンストラクタで設定します。型番の上限を指定しない場合は、型番40を上限として分割されます。

```vbnet
Dim symbols As Symbols = New Symbols(allowStructuredAppend:=True)
```

型番1を超える場合に分割し、各QRコードのImageオブジェクトを取得する例を示します。
```vbnet
Dim symbols As Symbols = New Symbols(maxVersion:=1, allowStructuredAppend:=True)
symbols.AppendText("abcdefghijklmnopqrstuvwxyz")

For Each symbol As Symbol In symbols
    Dim image As Image = symbol.GetImage()
Next
```

### 例６．BMPファイルへ保存する
SymbolクラスのSaveBitmapメソッドを使用します。
```vbnet
Dim symbols As Symbols = New Symbols()
symbols.AppendText("012345abcdefg")

' 24bpp DIB
symbols(0).SaveBitmap("qrcode.bmp")

' 1bpp DIB
symbols(0).SaveBitmap("qrcode.bmp", monochrome:=True)

' 10 pixels per module
symbols(0).SaveBitmap("qrcode.bmp", moduleSize:=10)

' Specify foreground and background colors.
symbols(0).SaveBitmap("qrcode.bmp", foreRgb:="#0000FF", backRgb:="#FFFF00")    
```

### 例７．SVGファイルへ保存する
SymbolクラスのSaveSvgメソッドを使用します。
```vbnet
Dim symbols As Symbols = New Symbols()
symbols.AppendText("012345abcdefg")

symbols(0).SaveSvg("qrcode.svg")
```

### 例８．様々な画像形式で保存する
ImageオブジェクトのSaveメソッドを使用します。

```vbnet
Imports System.Drawing
Imports System.Drawing.Imaging

Dim symbols As Symbols = New Symbols()
symbols.AppendText("012345")

Dim image As Image = symbols(0).GetImage()
' PNG
image.Save("qrcode.png", ImageFormat.Png)
' GIF
image.Save("qrcode.gif", ImageFormat.Gif)
' JPEG
image.Save("qrcode.jpg", ImageFormat.Jpeg)
```

### 例９．base64エンコードされた画像データを取得する
SymbolオブジェクトのGetBitmapBase64メソッドを使用します。
```vbnet
Dim symbols As Symbols = New Symbols()
symbols.AppendText("012345abcdefg")

Dim data As String = symbols(0).GetBitmapBase64()
Dim imgTag As String = "<img src=""data:image/bmp;base64," & data & """ />"
```

### 例１０．SVGデータを取得する
SymbolオブジェクトのGetSvgメソッドを使用します。
```vbnet
Dim symbols As Symbols = New Symbols()
symbols.AppendText("012345abcdefg")

Dim svg As String = symbols(0).GetSvg()
```