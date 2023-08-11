using Microsoft.CognitiveServices.Speech;
using Microsoft.Extensions.Options;
using TextToSpeechPOC.Models;

namespace TextToSpeechPOC.Services
{
    public class AzureService : ITextToSpeechService
    {
        private readonly SpeechConfig _config;
        public AzureService(IOptions<AzureCloudCredentials> azureCloudCredentials)
        {
            var credentials = azureCloudCredentials.Value;

            _config = SpeechConfig.FromSubscription(credentials.SubscriptionKey, credentials.Region);
            _config.SetSpeechSynthesisOutputFormat(SpeechSynthesisOutputFormat.Audio16Khz32KBitRateMonoMp3);
        }

        public async Task<byte[]> GetByteAudioFromText(string text, string voice = "pt-BR-NicolauNeural")
        {
            _config.SpeechSynthesisVoiceName = voice;
            using (var synthesizer = new SpeechSynthesizer(_config))
            {
                using (var result = await synthesizer.SpeakTextAsync(text))
                {
                    if (result.Reason == ResultReason.SynthesizingAudioCompleted)
                    {
                        Console.WriteLine($"Speech synthesized for [{text}]");
                        var stream = AudioDataStream.FromResult(result);  // to return in Memory  

                        //Save Path
                        //await stream.SaveToWaveFileAsync(@"C:\Users\{UserName}\Documents\TestAudio_" + DateTime.Now.Hour.ToString() + "_" + DateTime.Now.Minute.ToString() + ".mp3");

                        var buffer2 = result.AudioData;
                        return buffer2;
                    }
                    else if (result.Reason == ResultReason.Canceled)
                    {
                        var cancellation = SpeechSynthesisCancellationDetails.FromResult(result);
                        Console.WriteLine($"CANCELED: Reason={cancellation.Reason}");

                        if (cancellation.Reason == CancellationReason.Error)
                        {
                            Console.WriteLine($"CANCELED: ErrorCode={cancellation.ErrorCode}");
                            Console.WriteLine($"CANCELED: ErrorDetails=[{cancellation.ErrorDetails}]");
                        }
                    }

                    //Return an empty object like return new byte[];
                    return default!;
                }
            }
        }

        public async Task<Stream> GetStreamAudioFromText(string text, string voice = "pt-BR-NicolauNeural")
        {
            _config.SpeechSynthesisVoiceName = voice;
            using (var synthesizer = new SpeechSynthesizer(_config))
            {
                using (var result = await synthesizer.SpeakTextAsync(text))
                {
                    if (result.Reason == ResultReason.SynthesizingAudioCompleted)
                    {
                        Console.WriteLine($"Speech synthesized for [{text}]");
                        var stream = AudioDataStream.FromResult(result);  // to return in Memory  

                        //Save Path
                        //await stream.SaveToWaveFileAsync(@"C:\Users\{UserName}\Documents\TestAudio_" + DateTime.Now.Hour.ToString() + "_" + DateTime.Now.Minute.ToString() + ".mp3");

                        return new MemoryStream(result.AudioData);
                    }
                    else if (result.Reason == ResultReason.Canceled)
                    {
                        var cancellation = SpeechSynthesisCancellationDetails.FromResult(result);
                        Console.WriteLine($"CANCELED: Reason={cancellation.Reason}");

                        if (cancellation.Reason == CancellationReason.Error)
                        {
                            Console.WriteLine($"CANCELED: ErrorCode={cancellation.ErrorCode}");
                            Console.WriteLine($"CANCELED: ErrorDetails=[{cancellation.ErrorDetails}]");
                        }
                    }

                    //Return an empty object like return new byte[];
                    return default!;
                }
            }
        }
    }
}
