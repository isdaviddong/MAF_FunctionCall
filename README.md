# MAF Function Calling 基本範例

這是一個使用 C#、Microsoft Agent Framework 與 OpenAI Responses API 的基本範例程式。

程式會建立一個數學助理 Agent，註冊自訂的 `Add` 函式，並要求模型在計算加法時呼叫這個函式。

## 執行需求

- .NET 10 SDK
- OpenAI API Key

## 設定 API Key

請先設定 `OPENAI_API_KEY` 環境變數。

Linux/macOS：

```bash
export OPENAI_API_KEY="你的 OpenAI API Key"
```

Windows PowerShell：

```powershell
$env:OPENAI_API_KEY = "你的 OpenAI API Key"
```

## 執行程式

在專案目錄執行：

```bash
dotnet run
```

預期會看到類似以下輸出：

```text
[Function Call] Add(25, 17)
AI 回答：25 加 17 等於 42。
```

## 程式流程

1. 從 `OPENAI_API_KEY` 環境變數讀取 API Key。
2. 建立 `OpenAIClient` 與 AI Agent。
3. 將自訂的 `Add` C# 函式註冊為 Agent 可使用的工具。
4. 傳送自然語言問題，讓模型決定呼叫 `Add`。
5. 顯示函式呼叫紀錄與模型回答。

## 注意事項

範例使用的 `GetResponsesClient()` 目前在 OpenAI SDK 中標記為 evaluation API，因此程式以 `#pragma warning disable OPENAI001` 局部停用該診斷。這表示相關 API 未來可能變更。