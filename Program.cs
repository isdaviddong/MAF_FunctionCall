
using System.ComponentModel;
using Microsoft.Agents.AI;
using Microsoft.Extensions.AI;
using OpenAI;
using OpenAI.Responses;

// 1. 建立 OpenAI Client
string apiKey = Environment.GetEnvironmentVariable("OPENAI_API_KEY")
    ?? throw new Exception("請設定 OPENAI_API_KEY");

OpenAIClient client = new(apiKey);

// 加入這個是因為我們正在使用一個尚未穩定的 API，我們需要忽略警告。
#pragma warning disable OPENAI001
// 2. 建立 Agent，註冊自己的 C# Function
AIAgent agent = client.GetResponsesClient().AsAIAgent(
    model: "gpt-4o-mini",
    instructions: "你是數學助理，計算加法時必須使用 Add 函式。",
    tools: [AIFunctionFactory.Create(Add)]
);
#pragma warning restore OPENAI001

// 3. 使用自然語言呼叫 Agent
var result = await agent.RunAsync("請幫我計算 25 加 17");

Console.WriteLine($"AI 回答：{result}");

// 4. 自己撰寫的 C# Function
[Description("計算兩個整數的總和")]
static int Add(
    [Description("第一個整數")] int a,
    [Description("第二個整數")] int b)
{
    Console.WriteLine($"[Function Call] Add({a}, {b})");

    return a + b;
}
