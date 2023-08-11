using Microsoft.CognitiveServices.Speech;
using Microsoft.CognitiveServices.Speech.Translation;

namespace TextToSpeechPOConsole
{
    internal class Program
    {
        static string speechKey = "";
        static string speechRegion = "";

        static async Task Main(string[] args)
        {
            await AzureSpeech();
        }

        private static async Task AzureSpeech()
        {
            var config = SpeechConfig.FromSubscription(speechKey, speechRegion);

            // Specify the desired language and voice
            config.SpeechSynthesisVoiceName = "pt-BR-NicolauNeural";

            config.SetSpeechSynthesisOutputFormat(SpeechSynthesisOutputFormat.Audio16Khz32KBitRateMonoMp3);

            // Create a speech synthesizer using the default speaker as audio output.
            //using (var synthesizer = new SpeechSynthesizer(config))
            //{
            //    // Receive a text from the user
            //    Console.WriteLine("Enter the text you want to synthesize:");
            //    string text = "Ola, Please harvard, esse é  meu primeiro exemplo de inteligencia artificial da StarLink e Please SpaceX is the best para falar por texto";// Console.ReadLine();

            //    using (var result = await synthesizer.SpeakTextAsync(text))
            //    {
            //        if (result.Reason == ResultReason.SynthesizingAudioCompleted)
            //        {
            //            Console.WriteLine($"Speech synthesized for [{text}]");
            //        }
            //        else if (result.Reason == ResultReason.Canceled)
            //        {
            //            var cancellation = SpeechSynthesisCancellationDetails.FromResult(result);
            //            Console.WriteLine($"CANCELED: Reason={cancellation.Reason}");

            //            if (cancellation.Reason == CancellationReason.Error)
            //            {
            //                Console.WriteLine($"CANCELED: ErrorCode={cancellation.ErrorCode}");
            //                Console.WriteLine($"CANCELED: ErrorDetails=[{cancellation.ErrorDetails}]");
            //            }
            //        }
            //    }
            //}


            // Your SSML content
            string ssml = @"<speak version=""1.0"" xmlns=""http://www.w3.org/2001/10/synthesis"" xml:lang=""en-US"">
                            <voice name=""en-US-RyanMultilingualNeural"">
                               <lang xml:lang=""pt-BR"">Olá, agora vamos testar essa merda! já disse que temos que ser mais </lang> 
                               <lang xml:lang=""en-US"">J. Robert Oppenheimer is more Fluently in English!</lang> <lang xml:lang=""pt-BR""> <prosody pitch=""high"" > vocês são todos burros!</prosody> </lang> 
                               
                            </voice>
                        </speak>";

            using (var synthesizer = new SpeechSynthesizer(config))
            {
                var result = await synthesizer.SpeakSsmlAsync(ssml);

                if (result.Reason == ResultReason.SynthesizingAudioCompleted)
                {
                    AudioDataStream stream = AudioDataStream.FromResult(result);  // to return in Memory  
                    await stream.SaveToWaveFileAsync(@"C:\Users\dougl\Documents\TestAudio_" + DateTime.Now.Hour.ToString() + "_" + DateTime.Now.Minute.ToString() + ".mp3");

                    //_logger.LogInformation("AzureSynthesisController_AzureSynthesisToBytesAsync", new Dictionary<string, string> { { "Id", Id }, { "ResultReasonMessage", "SynthesizingAudioCompleted" }, { "OriginalText", audioText } });  


                    var buffer2 = result.AudioData;
                    Stream stream2 = new MemoryStream(buffer2);

                    //return result.AudioData;

                    Console.WriteLine("Speech synthesized successfully.");
                }
                else if (result.Reason == ResultReason.Canceled)
                {
                    var cancellation = SpeechSynthesisCancellationDetails.FromResult(result);
                    Console.WriteLine($"CANCELED: Reason={cancellation.Reason}");
                    Console.WriteLine($"CANCELED: ErrorCode={cancellation.ErrorCode}");
                    Console.WriteLine($"CANCELED: ErrorDetails=[{cancellation.ErrorDetails}]");
                }
            }
        }

        static async Task GoogleSpeech()
        {
            
        }
    }
}