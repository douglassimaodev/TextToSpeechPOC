using Microsoft.AspNetCore.Mvc;
using TextToSpeechPOC.Services;

namespace TextToSpeechPOC.Controllers
{
    public class TextToSpeechController : Controller
    {
        private readonly ITextToSpeechService _textToSpeechService;

        public TextToSpeechController(ITextToSpeechService textToSpeechService)
        {
            _textToSpeechService = textToSpeechService;
        }

        public IActionResult Azure()
        {
            //Populate DDl with available nationalities like en-US, pt-BR
            //Populate DDl with available languages name (will need to filter per nationalities)
            //Populate DDl with available names genders M,F,G etc where will gilder the language names

            return View();
        }

        //Create connection to azure api text to speech, send the details, and get the audio back to show to the user 
        //[HttpPost]
        //public async Task<IActionResult> Azure(FormDto model)
        //{
        //    var result = _textToSpeechService.GetStreamAudioFromText(model.Text, model.VoiceName)
        //    return View(result);
        //}

        public IActionResult Google()
        {
            return View();
        }


        //Create connection to google api text to speech, send the details, and get the audio back to show to the user 
        //[HttpPost]
        //public IActionResult Google(FormDto model)
        //{
        //    var result = _textToSpeechService.GetStreamAudioFromText(model.Text, model.VoiceName)
        //    return View(result);
        //}

        //public void SomeMethod()
        //{
        //    string serviceAccountJson = JsonSerializer.Serialize(_googleCloudCredentials);

        //    var credentials = GoogleCredential.FromJson(serviceAccountJson)
        //                    .CreateScoped(TextToSpeechClient.DefaultScopes);
        //    var client = TextToSpeechClient.Create(credentials);

        //    // Use the client as needed...
        //}
    }
}
