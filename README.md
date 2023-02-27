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
        - [プラグイン プロジェクトの作成](#プラグイン-プロジェクトの作成)
      - [プラグインをデバッグする](#プラグインをデバッグする)
      - [プラグインを更新する](#プラグインを更新する)

<!-- /TOC -->

---

<br>

## 事前準備
<a id="markdown-%E4%BA%8B%E5%89%8D%E6%BA%96%E5%82%99" name="%E4%BA%8B%E5%89%8D%E6%BA%96%E5%82%99"></a>

- [Microsoft Power Platform CLI](https://learn.microsoft.com/ja-jp/power-platform/developer/cli/introduction)
  - 新規インストール
    - [Power Platform Tools for Visual Studio Code](https://aka.ms/ppcvscode)
    - [Power Platform CLI for Windows](https://aka.ms/PowerAppsCLI)
  - 既存を更新 `pac install latest` （Power Platform CLI for Windowsのみ）

- [Dataverse 開発ツール](https://learn.microsoft.com/ja-jp/power-apps/developer/data-platform/download-tools-nuget)

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

<br>

## プラグイン概要
<a id="markdown-%E3%83%97%E3%83%A9%E3%82%B0%E3%82%A4%E3%83%B3%E6%A6%82%E8%A6%81" name="%E3%83%97%E3%83%A9%E3%82%B0%E3%82%A4%E3%83%B3%E6%A6%82%E8%A6%81"></a>

- [ビジネス プロセスを拡張するためのプラグインの使用](https://learn.microsoft.com/ja-jp/power-apps/developer/data-platform/plug-ins)

### チュートリアル
<a id="markdown-%E3%83%81%E3%83%A5%E3%83%BC%E3%83%88%E3%83%AA%E3%82%A2%E3%83%AB" name="%E3%83%81%E3%83%A5%E3%83%BC%E3%83%88%E3%83%AA%E3%82%A2%E3%83%AB"></a>

#### [プラグインを書き込み登録する](https://learn.microsoft.com/ja-jp/power-apps/developer/data-platform/tutorial-write-plug-in)
<a id="markdown-%E3%83%97%E3%83%A9%E3%82%B0%E3%82%A4%E3%83%B3%E3%82%92%E6%9B%B8%E3%81%8D%E8%BE%BC%E3%81%BF%E7%99%BB%E9%8C%B2%E3%81%99%E3%82%8B" name="%E3%83%97%E3%83%A9%E3%82%B0%E3%82%A4%E3%83%B3%E3%82%92%E6%9B%B8%E3%81%8D%E8%BE%BC%E3%81%BF%E7%99%BB%E9%8C%B2%E3%81%99%E3%82%8B"></a>

`取引先企業テーブルの作成メッセージで登録された非同期プラグインを作成します。プラグインは、取引先企業の作成者に 1 週間後のフォローアップを通知するタスク活動を作成します。`

##### [プラグイン プロジェクトの作成](https://learn.microsoft.com/ja-jp/power-apps/developer/data-platform/tutorial-write-plug-in#%E3%83%97%E3%83%A9%E3%82%B0%E3%82%A4%E3%83%B3-%E3%83%97%E3%83%AD%E3%82%B8%E3%82%A7%E3%82%AF%E3%83%88%E3%81%AE%E4%BD%9C%E6%88%90)
<a id="markdown-%E3%83%97%E3%83%A9%E3%82%B0%E3%82%A4%E3%83%B3-%E3%83%97%E3%83%AD%E3%82%B8%E3%82%A7%E3%82%AF%E3%83%88%E3%81%AE%E4%BD%9C%E6%88%90" name="%E3%83%97%E3%83%A9%E3%82%B0%E3%82%A4%E3%83%B3-%E3%83%97%E3%83%AD%E3%82%B8%E3%82%A7%E3%82%AF%E3%83%88%E3%81%AE%E4%BD%9C%E6%88%90"></a>

- NET Framework 4.6.2を使用する新しいClass Library (.NET Framework 4.6.2) プロジェクトを作成。プロジェクト名は `BasicPlugin` 。

- NuGet パッケージ「 Microsoft.CrmSdk.CoreAssemblies 」をインストール。

#### [プラグインをデバッグする](https://learn.microsoft.com/ja-jp/power-apps/developer/data-platform/tutorial-debug-plug-in)
<a id="markdown-%E3%83%97%E3%83%A9%E3%82%B0%E3%82%A4%E3%83%B3%E3%82%92%E3%83%87%E3%83%90%E3%83%83%E3%82%B0%E3%81%99%E3%82%8B" name="%E3%83%97%E3%83%A9%E3%82%B0%E3%82%A4%E3%83%B3%E3%82%92%E3%83%87%E3%83%90%E3%83%83%E3%82%B0%E3%81%99%E3%82%8B"></a>


#### [プラグインを更新する](https://learn.microsoft.com/ja-jp/power-apps/developer/data-platform/tutorial-update-plug-in)
<a id="markdown-%E3%83%97%E3%83%A9%E3%82%B0%E3%82%A4%E3%83%B3%E3%82%92%E6%9B%B4%E6%96%B0%E3%81%99%E3%82%8B" name="%E3%83%97%E3%83%A9%E3%82%B0%E3%82%A4%E3%83%B3%E3%82%92%E6%9B%B4%E6%96%B0%E3%81%99%E3%82%8B"></a>



---

Copyright (c) 2022 YA-androidapp(https://github.com/YA-androidapp) All rights reserved.
