using System.ComponentModel;
using System.Runtime.Serialization;

namespace Donker.Home.Somneo.ApiClient.Models
{
    /// <summary>
    /// The type of audio device in use by the Somneo's audio player.
    /// </summary>
    public enum AudioPlayerDeviceType
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
}
