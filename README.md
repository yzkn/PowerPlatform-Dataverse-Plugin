# PowerPlatform-Dataverse-Plugin
<a id="markdown-powerplatform-dataverse-plugin" name="powerplatform-dataverse-plugin"></a>

Dataverseプラグイン

---

<!-- TOC -->

- [PowerPlatform-Dataverse-Plugin](#powerplatform-dataverse-plugin)
  - [事前準備](#事前準備)
  - [プラグイン概要](#プラグイン概要)
    - [チュートリアル](#チュートリアル)
      - [プラグインを書き込み登録する](#プラグインを書き込み登録する)
      - [プラグインをデバッグする](#プラグインをデバッグする)
      - [プラグインを更新する](#プラグインを更新する)
    - [クイックスタート](#クイックスタート)
      - [Power Platform ツール プロジェクトを作成する](#power-platform-ツール-プロジェクトを作成する)
      - [Power Platform Tools を使用してプラグインを作成する](#power-platform-tools-を使用してプラグインを作成する)

<!-- /TOC -->

---

<br>

## 事前準備
<a id="markdown-%E4%BA%8B%E5%89%8D%E6%BA%96%E5%82%99" name="%E4%BA%8B%E5%89%8D%E6%BA%96%E5%82%99"></a>

- [Microsoft Power Platform CLI](https://learn.microsoft.com/ja-jp/power-platform/developer/cli/introduction)
  - インストール
    - 新規インストール
      - [Power Platform Tools for Visual Studio Code](https://aka.ms/ppcvscode)
      - [Power Platform CLI for Windows](https://aka.ms/PowerAppsCLI)
    - 既存を更新 `pac install latest` （Power Platform CLI for Windowsのみ）
  - [Dataverse 開発ツール](https://learn.microsoft.com/ja-jp/power-apps/developer/data-platform/download-tools-nuget)

<br>

```powershell
# 更新の場合は --update オプションを追加

# Configuration Migration Tool (CMT)
pac tool cmt

# Package Deployer (PD)
pac tool pd

# Plug-in Registration tool (PRT)
pac tool prt

# SolutionPackager ツール (SP)
# コード生成ツール (CG)

# インストール済一覧
pac tool list

```

- Visual Studio インストーラー
  - Visual Studio 最新版
  - .NET Framework 4.6.2
  - Windows Workflow Foundation

- Visual Studio 上での、拡張機能 > 拡張機能の管理
  - Power Platform Tools ( for VS 2022 )
    1. Power Platform Tools > General
    2. Use nuget package for deploying Plugins to Dataverse


<br>

## プラグイン概要
<a id="markdown-%E3%83%97%E3%83%A9%E3%82%B0%E3%82%A4%E3%83%B3%E6%A6%82%E8%A6%81" name="%E3%83%97%E3%83%A9%E3%82%B0%E3%82%A4%E3%83%B3%E6%A6%82%E8%A6%81"></a>

- [ビジネス プロセスを拡張するためのプラグインの使用](https://learn.microsoft.com/ja-jp/power-apps/developer/data-platform/plug-ins)

### チュートリアル
<a id="markdown-%E3%83%81%E3%83%A5%E3%83%BC%E3%83%88%E3%83%AA%E3%82%A2%E3%83%AB" name="%E3%83%81%E3%83%A5%E3%83%BC%E3%83%88%E3%83%AA%E3%82%A2%E3%83%AB"></a>

#### [プラグインを書き込み登録する](https://learn.microsoft.com/ja-jp/power-apps/developer/data-platform/tutorial-write-plug-in)
<a id="markdown-%E3%83%97%E3%83%A9%E3%82%B0%E3%82%A4%E3%83%B3%E3%82%92%E6%9B%B8%E3%81%8D%E8%BE%BC%E3%81%BF%E7%99%BB%E9%8C%B2%E3%81%99%E3%82%8B" name="%E3%83%97%E3%83%A9%E3%82%B0%E3%82%A4%E3%83%B3%E3%82%92%E6%9B%B8%E3%81%8D%E8%BE%BC%E3%81%BF%E7%99%BB%E9%8C%B2%E3%81%99%E3%82%8B"></a>

`取引先企業テーブルの作成メッセージで登録された非同期プラグインを作成します。プラグインは、取引先企業の作成者に 1 週間後のフォローアップを通知するタスク活動を作成します。`

1. 新しい `クラスライブラリ (.NET Framework 4.6.2)` プロジェクトを作成。プロジェクト名は `BasicPlugin`
2. NuGet パッケージ `Microsoft.CrmSdk.CoreAssemblies` をインストール
3. `Class1.cs` を `FollowupPlugin.cs` にリネーム
4. [FollowupPlugin.cs](https://raw.githubusercontent.com/microsoft/PowerApps-Samples/master/dataverse/orgsvc/C%23/FollowupPlugin/FollowupPlugin/FollowupPlugin.cs) で上書き。 `namespace BasicPlugin` はそのまま
5. ソリューションをビルド
6. 署名設定
   1. BasicPlugin プロジェクトのプロパティで署名タブの `アセンブリに署名する` を有効化
   2. 新規作成
   3. ファイル名・パスワードを指定
7. `\bin\Debug\BasicPlugin.dll` が生成されていることを確認

8. プラグイン登録ツールを起動
9. Microsoft 365 アカウントで認証
10. 環境を選択
11. 「登録」ドロップダウンリストで「新しいアセンブリ」を選択
12. 選択したプラグインの登録 をクリックしてダイアログでも OK をクリック

13. `(Assembly) BasicPlugin` を展開すると、 `(Plugin) BasicPlugin.FollowUpPlugin` プラグインが表示される。 `(Plugin) BasicPlugin.FollowUpPlugin` を右クリックし、新しいステップの登録を選択
14. ダイアログで以下を設定

| 設定                               | 値            |
| ---------------------------------- | ------------- |
| メッセージ                         | Create        |
| 主エンティティ                     | account       |
| 実行のイベントパイプラインステージ | PostOperation |
| 実行モード                         | 非同期        |

15. 新しいステップの登録をクリックして登録を完了

16. 取引先企業テーブルとタスクテーブルを使用するモデル駆動型アプリ（ [ソリューションパッケージ](./Solutions/AccountTask_1_0_0_0.zip) ）を作成し、取引先企業テーブルにレコードを新規作成
17. 作成した取引先企業レコードの活動にタスクが追加されていることを確認
18. Power Platform 管理センターの環境の設定画面から「すべてのレガシ設定」から Dynamics 365 の設定画面を開き、設定からシステムジョブを確認（ [システム ジョブの表示](https://learn.microsoft.com/ja-jp/power-apps/developer/data-platform/tutorial-write-plug-in#%E3%82%B7%E3%82%B9%E3%83%86%E3%83%A0-%E3%82%B8%E3%83%A7%E3%83%96%E3%81%AE%E8%A1%A8%E7%A4%BA) ）

#### [プラグインをデバッグする](https://learn.microsoft.com/ja-jp/power-apps/developer/data-platform/tutorial-debug-plug-in)
<a id="markdown-%E3%83%97%E3%83%A9%E3%82%B0%E3%82%A4%E3%83%B3%E3%82%92%E3%83%87%E3%83%90%E3%83%83%E3%82%B0%E3%81%99%E3%82%8B" name="%E3%83%97%E3%83%A9%E3%82%B0%E3%82%A4%E3%83%B3%E3%82%92%E3%83%87%E3%83%90%E3%83%83%E3%82%B0%E3%81%99%E3%82%8B"></a>

1. プラグイン登録ツールで、 `プロファイラーのインストール` をクリック
   1. エラーが出る場合は、プラグイン登録ツールと同じフォルダにあるソリューションパッケージを手動でインストール
2. `(Step) BasicPlugin.FollowupPlugin: Create of account` を選択し、 `プロファイリングを開始` をクリック
3. プロファイラー設定ダイアログが表示されるので、既定のまま `OK` をクリック
4. プラグイン登録ツールで、 `デバッグ` をクリック
5. `プラグイン実行の再生` ダイアログの `セットアップ` タブで、 `プロファイルの場所` の右の `↓` をクリック
6. `CRM からプロファイルを選択` ダイアログで、プロファイルを選択
7. `アセンブリの場所` で `BasicPlugin.dll` の場所を指定
8. Visual Studio 上で
   1. Visual Studio で、プラグイン クラスにブレーク ポイントを設定
   2. Visual Studio プロジェクトで、 `デバッグ` > `プロセスにアタッチ` を選択
   3. `利用できるプロセス（Available processes）` の一覧から `PluginRegistration.exe` でフィルタリングしてプロセスを選択
   4. `追加（Attach）` をクリック
9. プラグイン登録ツールで、 `実行の開始` をクリック

10. デバッグする

11. プラグイン実行の再生ダイアログダイアログを閉じる
12. プラグイン登録ツールで、 `プロファイリングの停止` をクリック

#### [プラグインを更新する](https://learn.microsoft.com/ja-jp/power-apps/developer/data-platform/tutorial-update-plug-in)
<a id="markdown-%E3%83%97%E3%83%A9%E3%82%B0%E3%82%A4%E3%83%B3%E3%82%92%E6%9B%B4%E6%96%B0%E3%81%99%E3%82%8B" name="%E3%83%97%E3%83%A9%E3%82%B0%E3%82%A4%E3%83%B3%E3%82%92%E6%9B%B4%E6%96%B0%E3%81%99%E3%82%8B"></a>

<br>

### クイックスタート
<a id="markdown-%E3%82%AF%E3%82%A4%E3%83%83%E3%82%AF%E3%82%B9%E3%82%BF%E3%83%BC%E3%83%88" name="%E3%82%AF%E3%82%A4%E3%83%83%E3%82%AF%E3%82%B9%E3%82%BF%E3%83%BC%E3%83%88"></a>

#### [Power Platform ツール プロジェクトを作成する](https://learn.microsoft.com/ja-jp/power-apps/developer/data-platform/tools/devtools-create-project)
<a id="markdown-power-platform-%E3%83%84%E3%83%BC%E3%83%AB-%E3%83%97%E3%83%AD%E3%82%B8%E3%82%A7%E3%82%AF%E3%83%88%E3%82%92%E4%BD%9C%E6%88%90%E3%81%99%E3%82%8B" name="power-platform-%E3%83%84%E3%83%BC%E3%83%AB-%E3%83%97%E3%83%AD%E3%82%B8%E3%82%A7%E3%82%AF%E3%83%88%E3%82%92%E4%BD%9C%E6%88%90%E3%81%99%E3%82%8B"></a>

- [Power Platform ソリューションのテンプレートを使用する](https://learn.microsoft.com/ja-jp/power-apps/developer/data-platform/tools/devtools-create-project#power-platform-%E3%82%BD%E3%83%AA%E3%83%A5%E3%83%BC%E3%82%B7%E3%83%A7%E3%83%B3%E3%81%AE%E3%83%86%E3%83%B3%E3%83%97%E3%83%AC%E3%83%BC%E3%83%88%E3%82%92%E4%BD%BF%E7%94%A8%E3%81%99%E3%82%8B)

1. Power Platform ソリューションテンプレートから新規プロジェクトを作成
2. .NET Framework 4.6.2 を指定
3. Dataverse の認証ダイアログ
4. 既存の Dataverse のソリューションを使用するか、新しいソリューションを作成するかを選択
5. 新しいソリューションに関する情報を入力
6. Power Platform のプロジェクトテンプレートを使用して新規プロジェクトを作成
7. ダイアログで、利用可能な各プロジェクトを 1 つだけ選択してソリューションに追加
8. プロジェクトの名前を入力し、完了を選択
9. Visual Studio ソリューション エクスプローラーでプロジェクト名として表示する名前を指定
10. ソリューション ファイルを保存するかどうかの確認で保存を選択

#### [Power Platform Tools を使用してプラグインを作成する](https://learn.microsoft.com/ja-jp/power-apps/developer/data-platform/tools/devtools-create-plugin)
<a id="markdown-power-platform-tools-%E3%82%92%E4%BD%BF%E7%94%A8%E3%81%97%E3%81%A6%E3%83%97%E3%83%A9%E3%82%B0%E3%82%A4%E3%83%B3%E3%82%92%E4%BD%9C%E6%88%90%E3%81%99%E3%82%8B" name="power-platform-tools-%E3%82%92%E4%BD%BF%E7%94%A8%E3%81%97%E3%81%A6%E3%83%97%E3%83%A9%E3%82%B0%E3%82%A4%E3%83%B3%E3%82%92%E4%BD%9C%E6%88%90%E3%81%99%E3%82%8B"></a>

1. Visual Studio でプラグインライブラリを持つ Dataverse ソリューションを開く
2. Visual Studio で、 `ツール` メニューで `Dataverse に接続` を選択
3. ダイアログで認証
4. 既存の Dataverse ソリューション、または既定のソリューションを選択
5. `ビュー` > `Power Platform Explorer` を選択し、 `環境` ノードと `テーブル` サブノードを展開
6. ステップを登録するテーブルを右クリックし、プラグインの作成を選択
7.

---

Copyright (c) 2023 YA-androidapp(https://github.com/YA-androidapp) All rights reserved.
