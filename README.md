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

## Session 與對話歷史

這是一個基本的 Function Calling 範例，**目前不包含 Session，也不會保留多輪對話的歷史記錄**。程式只執行一次請求；程式結束後，對話內容也不會由這個範例保存。

如果要讓 Agent 記住同一次程式執行中的對話，必須自行建立 `AgentSession`，並在每次呼叫 `RunAsync` 時傳入相同的 Session。例如，可以將原本的單次呼叫：

```csharp
var result = await agent.RunAsync("請幫我計算 25 加 17");
Console.WriteLine($"AI 回答：{result}");
```

修改為以下多輪對話：

```csharp
AgentSession session = await agent.CreateSessionAsync();

var firstResult = await agent.RunAsync(
	"請幫我計算 25 加 17",
	session);
Console.WriteLine($"AI 回答：{firstResult}");

var secondResult = await agent.RunAsync(
	"再把剛才的結果乘以 2",
	session);
Console.WriteLine($"AI 回答：{secondResult}");
```

這個修改只涵蓋目前程式執行期間的對話歷史。如果需要跨程式重啟、跨使用者或跨裝置保留歷史記錄，還需要自行將 Session 狀態或對話訊息保存到資料庫、檔案或其他持久化儲存服務，並在下一次執行時載入。

## 注意事項

範例使用的 `GetResponsesClient()` 目前在 OpenAI SDK 中標記為 evaluation API，因此程式以 `#pragma warning disable OPENAI001` 局部停用該診斷。這表示相關 API 未來可能變更。