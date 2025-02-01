using System;
using System.ComponentModel;
using System.Linq;
using System.Reactive.Linq;
using Bonsai.Harp;
using Bonsai;

namespace Harp.Behavior
{
    /// <summary>
    /// Represents an operator that convertes raw ADC values to Volts.
    /// </summary>
    [Description("Converts a sequence of raw ADC reads to Volt.")]
    public class AdcToVolt : Transform<AnalogDataPayload, AnalogVoltData>
    {
        /// <summary>
        /// Converts a <see cref="AnalogDataPayload"/> with raw voltage readings to volts.
        /// </summary>
        /// <returns>
        /// A sequence of <see cref="AnalogVoltData"/> objects representing the converted values.
        /// </returns>
        public override IObservable<AnalogVoltData> Process(IObservable<AnalogDataPayload> source)
        {
            return source.Select(
                value => new AnalogVoltData()
                {
                    AnalogInput0 = AdcToVoltConverter(value.AnalogInput0),
                    AnalogInput1 = AdcToVoltConverter(value.AnalogInput1)
                });
        }

        /// <summary>
        /// Converts a sequence of <see cref="Timestamped"/> <see cref="AnalogDataPayload"/> values
        /// with raw voltage readings to volts.
        /// </summary>
        /// <returns>
        /// A sequence of <see cref="Timestamped"/> <see cref="AnalogVoltData"/> objects representing the converted values.
        /// </returns>
        public IObservable<Timestamped<AnalogVoltData>> Process(IObservable<Timestamped<AnalogDataPayload>> source)
        {
            return source.Select(
                value =>
                {
                    var payload = new AnalogVoltData()
                    {
                        AnalogInput0 = AdcToVoltConverter(value.Value.AnalogInput0),
                        AnalogInput1 = AdcToVoltConverter(value.Value.AnalogInput1)
                    };
                    return Timestamped.Create(payload, value.Seconds);
                });
        }

        /// <summary>
        /// Converts a raw ADC value to volts.
        /// </summary>
        /// <param name="adcValue">The raw ADC value to be converted.</param>
        /// <returns>The converted voltage value.</returns>
        private static float AdcToVoltConverter(short adcValue)
        {
            // Full adc scale 4096 -> 3.3/1.6 = 2.0625V
            // In practice, HW supports max 5V which translates
            // to = 15/39 (given by resistor) * 5V = 1.9231 VMax
            // input to the ADC.
            // 1.9231V/2.0625V * 4095 = 3818 @ 5V analog input
            return (float)(5.0 / 3818) * adcValue;
        }
    }

    /// <summary>
    /// Represents the converted values from raw adc units to volts from
    /// a payload of the AnalogData register.
    /// </summary>
    public struct AnalogVoltData
    {
        /// <summary>
        /// The voltage at the output of the ADC channel 0.
        /// </summary>
        public float AnalogInput0;

        /// <summary>
        /// The voltage at the output of the ADC channel 1.
        /// </summary>
        public float AnalogInput1;

        /// <summary>
        /// Returns a <see cref="string"/> that represents the 
        /// converted AnalogData payload.
        /// </summary>
        /// <returns>
        /// A <see cref="string"/> that represents the 
        /// converted AnalogData payload.
        /// </returns>
        public override string ToString()
        {
            return "AnalogVoltData { " +
                "AnalogInput0 = " + AnalogInput0 + ", " +
                "AnalogInput1 = " + AnalogInput1 + " " +
            "}";
        }
    }
}
