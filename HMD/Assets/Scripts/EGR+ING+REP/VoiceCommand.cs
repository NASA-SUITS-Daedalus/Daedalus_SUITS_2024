using Microsoft.MixedReality.Toolkit.Input;
using UnityEngine;

public class VoiceCommandHandler : MonoBehaviour, IMixedRealitySpeechHandler
{
    public void OnSpeechKeywordRecognized(SpeechEventData eventData)
    {
        if (eventData.Command.Keyword == "Nova") // Replace "Aria" with your chosen name
        {
            // Code to execute when the voice command is recognized
            Debug.Log("Voice command 'Nova' recognized!");
            // Add your desired functionality here
        }
    }
}