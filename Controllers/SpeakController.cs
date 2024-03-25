using System;
using System.Net.WebSockets;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CognitiveServices.Speech;
using DotNetEnv;

namespace dnetspeech.Controllers;

[Route("api/[controller]")]
[ApiController]
public class SpeakController : ControllerBase
{
    public SpeakController()
    {
        var envPath = Path.Combine(Directory.GetCurrentDirectory(), ".env");
        Env.Load(envPath);
    }
    [HttpGet]
    public async Task Get(string text)
    {
        var context = ControllerContext.HttpContext;

        var subscriptionKey = Environment.GetEnvironmentVariable("SPEECH_KEY");
        if (string.IsNullOrEmpty(subscriptionKey))
        {
            throw new Exception("Speech key is missing");
        }
        var region = Environment.GetEnvironmentVariable("SPEECH_REGION");
        if (string.IsNullOrEmpty(region))
        {
            throw new Exception("Speech region is missing");
        }
        var speechConfig = SpeechConfig.FromSubscription(subscriptionKey, region);
        speechConfig.SpeechSynthesisVoiceName = "zh-CN-XiaoxiaoNeural";
        speechConfig.SetSpeechSynthesisOutputFormat(SpeechSynthesisOutputFormat.Audio16Khz32KBitRateMonoMp3);
        using var speechSynthesizer = new SpeechSynthesizer(speechConfig, null);

        var result = await speechSynthesizer.SpeakTextAsync(text);
        using var stream = AudioDataStream.FromResult(result);

        byte[] buffer = new byte[1024];
        uint bytesRead;
        while ((bytesRead = stream.ReadData(buffer)) > 0)
        {
            await context.Response.Body.WriteAsync(buffer, 0, (int)bytesRead);
        }
    }

    [HttpGet("health")]
    public IActionResult Health()
    {
        return Ok("Healthy");
    }
    
}