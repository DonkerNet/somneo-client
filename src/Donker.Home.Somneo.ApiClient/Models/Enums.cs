using System.ComponentModel;
using System.Runtime.Serialization;

namespace Donker.Home.Somneo.ApiClient.Models
{
    /// <summary>
    /// The type of audio device in use by the Somneo's audio player.
    /// </summary>
    public enum PlayerDeviceType
    {
        /// <summary>
        /// The Somneo is not playing anything.
        /// </summary>
        [EnumMember(Value = "off")]
        [Description("None")]
        None,
        /// <summary>
        /// The Somneo is playing the FM radio.
        /// </summary>
        [EnumMember(Value = "fmr")]
        [Description("FM radio")]
        FMRadio,
        /// <summary>
        /// The Somneo is playing music from the auxiliary input.
        /// </summary>
        [EnumMember(Value = "aux")]
        [Description("AUX")]
        AUX
    }

    /// <summary>
    /// The direction in which to seek a station on the radio band.
    /// </summary>
    public enum RadioSeekDirection
    {
        /// <summary>
        /// Seek a radio station on the band in forwards direction.
        /// </summary>
        [EnumMember(Value = "aux")]
        [Description("AUX")]
        Up,
        /// <summary>
        /// Seek a radio station on the band in backwards direction.
        /// </summary>
        [EnumMember(Value = "aux")]
        [Description("AUX")]
        Down
    }
}
