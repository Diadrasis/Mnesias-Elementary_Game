using UnityEngine;

[CreateAssetMenu (fileName="New Instrument", menuName="Instrument")]
public class Instrument : ScriptableObject
{
    [Tooltip("Name of the instrument")]
    public string nameTitle;

    [Tooltip("Coordinates to use it for creating our markers on our map")]
    public Vector2 cordinates;

    [Tooltip("Assign the Image of the instrument for the marker, needs .texture in code")]
    public Sprite imgMarker;

    [Tooltip("Assign the Image of the instrument, needs .texture in code")]
    public Sprite mainImage;

    [Tooltip("Audio clip of our instrument")]
    public AudioClip clip;

    [Tooltip("Main text of our instrument with extra information")]
    public string infoText;
}
