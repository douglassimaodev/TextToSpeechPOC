namespace TextToSpeechPOC.Services
{
    public interface ITextToSpeechService
    {
        Task<byte[]> GetByteAudioFromText(string text, string voice);
        Task<Stream> GetStreamAudioFromText(string text, string voice);
    }
}
