using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class OrbiterSound : MonoBehaviour
{
    public int sampleRate = 44100; // Sample rate (CD quality)

    public string note = "C"; // Musical note (C, D, E, F, G, A, B)
    public int octave = 4;    // Octave number (e.g., 4 for middle C)

    // Frequency values for the notes in the 4th octave (C4, D4, E4, etc.)
    private Dictionary<string, float> baseFrequencies = new Dictionary<string, float>()
    {
        {"C", 261.63f}, {"D", 293.66f}, {"E", 329.63f},
        {"F", 349.23f}, {"G", 392.00f}, {"A", 440.00f},
        {"B", 493.88f}
    };

    private void PlayTone(string newNote, int newOctave)
    {
        // Update the note and octave
        note = newNote;
        octave = newOctave;

        // Calculate the frequency of the selected note and octave
        if (baseFrequencies.TryGetValue(note, out float baseFrequency))
        {
            float frequency = baseFrequency * Mathf.Pow(2, octave - 4); // Adjust frequency based on octave

            // Create the audio clip with the desired duration and sample rate
            AudioClip clip = AudioClip.Create("SineTone_" + note + octave, (int)(sampleRate * duration), 1, sampleRate, false);

            // Generate the sine tone and fill the audio clip data
            float[] samples = new float[(int)(sampleRate * duration)];
            GenerateSineTone(samples, frequency);

            // Set the samples to the audio clip
            clip.SetData(samples, 0);

            // Stop the current sound if playing
            var source = GetComponent<AudioSource>();
            if (GetComponent<AudioSource>().isPlaying)
            {
                source.Stop();
            }

            // Assign the new audio clip to the AudioSource and play it
            source.clip = clip;
            source.loop = true;
            source.Play();
        }
        else
        {
            Debug.LogError("Invalid note selected: " + note);
        }
    }

    void GenerateSineTone(float[] samples, float frequency)
    {
        int totalSamples = samples.Length;

        for (int i = 0; i < totalSamples; i++)
        {
            float time = (float)i / sampleRate;
            samples[i] = Mathf.Sin(2.0f * Mathf.PI * frequency * time);
        }
    }

    // Example public methods to change note and octave
    public void SetNoteAndOctave(string newNote, int newOctave)
    {
        PlayTone(newNote, newOctave);
    }

    public void SetNote(string newNote)
    {
        PlayTone(newNote, octave); // Keep the current octave
    }

    public void SetOctave(int newOctave)
    {
        PlayTone(note, newOctave); // Keep the current note
    }

    private float duration = 5;

}
