using System;
using System.ComponentModel;
using System.IO;
using System.Reactive.Linq;
using Bonsai;

namespace Harp.Behavior
{
    /// <summary>
    /// Represents an operator that returns the contents of the metadata file
    /// describing the device registers.
    /// </summary>
    [Description("Returns the contents of the metadata file describing the device registers.")]
    public class DeviceMetadata : Source<string>
    {
        static readonly string Metadata = GetDeviceMetadata();

        static string GetDeviceMetadata()
        {
            var metadataType = typeof(DeviceMetadata);
            using var metadataStream = metadataType.Assembly.GetManifestResourceStream($"{metadataType.Namespace}.device.yml");
            using var streamReader = new StreamReader(metadataStream);
            return streamReader.ReadToEnd();
        }

        /// <inheritdoc/>
        public override IObservable<string> Generate()
        {
            return Observable.Return(Metadata);
        }
    }
}
