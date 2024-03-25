# dnetspeech

这是一个使用 .NET 和 Microsoft Cognitive Services Speech SDK 构建的语音合成服务。

## 安装

1. 克隆此仓库到本地。
2. 在项目的根目录下创建一个 `.env` 文件，用于存储环境变量。

## 环境变量

在 `.env` 文件中，需要设置以下两个环境变量：

- `SPEECH_KEY`：您的 Microsoft Cognitive Services Speech 服务的订阅密钥。
- `SPEECH_REGION`：您的 Microsoft Cognitive Services Speech 服务的区域。

例如：
``` ini
SPEECH_KEY=your_speech_key 
SPEECH_REGION=your_speech_region
```

## 运行

在项目的根目录下，使用以下命令启动服务：

```bash
dotnet restore
dotnet run
```
服务启动后，您可以访问 http://localhost:5032/test.html 来测试语音合成功能。

源码位于 [./wwwroot/test.html](/wwwroot/test.html)。

## API
GET /api/speak：将文本转化为语音。需要在请求参数中提供 text 参数。
GET /api/speak/health：检查服务的健康状态。
