## Generate PWM

The four general-purpose outputs **DO0** – **DO3** can generate hardware-timed pulse-width modulation (PWM) signals. The PWM signal can be tested with either a [speaker](./peripherals/peripherals-speaker.md) (where the frequency controls the pitch) or an LED (where the duty cycle controls the brightness). Refer to the [connections](./connections.md) article to set up either accessory on **DO0**, which we will use for the rest of these examples.

This article covers how to configure the frequency and duty cycle, as well as how to generate and stop the PWM in Bonsai.

The complete workflow is shown below. Copy and paste it into Bonsai or build each section by following the step-by-step instructions below.

:::workflow
![Generate PWM](../workflows/generatepwm-toplevel.bonsai)
:::

### Configure PWM

Each output has its own [`PwmFrequencyDO0`] and [`PwmDutyCycleDO0`] registers.

:::workflow
![Configure PWM](../workflows/generatepwm-configure.bonsai)
:::

- Insert a [`KeyDown`] operator and set the `Filter` property to `A`.
- Insert a [`CreateMessage`] operator and configure the following properties:
    - `Payload` - Select `PwmFrequencyDO0Payload`.
    - `PwmFrequencyDO0` - Set the PWM frequency in Hz (e.g. 1000). Valid values are 1 to 10000.
- Insert a [`MulticastSubject`] operator named `Behavior Commands`.
- Insert a [`CreateMessage`] operator and configure the following properties:
    - `Payload` - Select `PwmDutyCycleDO0Payload`.
    - `PwmDutyCycleDO0` - Set the duty cycle in percent (e.g. 50). Valid values are 1 to 99.
- Insert a [`MulticastSubject`] operator named `Behavior Commands`.

Run the workflow and press <kbd>A</kbd> to configure a 1 kHz, 50% duty cycle waveform on **DO0**. Frequency and duty cycle can also be modified while the PWM signal is running.

### Start and Stop PWM

The [`PwmStart`] and [`PwmStop`] registers start and stop the waveform on any combination of outputs.

:::workflow
![Start and Stop PWM](../workflows/generatepwm-startstop.bonsai)
:::

- Insert a [`KeyDown`] operator and set the `Filter` property to `S`.
- Insert a [`CreateMessage`] operator and configure the following properties:
    - `Payload` - Select `PwmStartPayload`.
    - `PwmStart` - Select `PwmDO0`.
- Insert a [`MulticastSubject`] operator named `Behavior Commands`.

In a separate branch:

- Insert a [`KeyDown`] operator and set the `Filter` property to `D`.
- Insert a [`CreateMessage`] operator and configure the following properties:
    - `Payload` - Select `PwmStopPayload`.
    - `PwmStop` - Select `PwmDO0`.
- Insert a [`MulticastSubject`] operator named `Behavior Commands`.

Run the workflow, then press <kbd>S</kbd> to start and <kbd>D</kbd> to stop the waveform on **DO0**.

### Generate PWM Bursts

To generate a PWM train of fixed duration, enable the [pulse function](control-digital-outputs.md#pulse-outputs) on the output with the [`OutputPulseEnable`] register and configure the duration in the [`PulseDO0`] register.

:::workflow
![Generate PWM Bursts](../workflows/generatepwm-pulseenable.bonsai)
:::

- Insert a [`KeyDown`] operator and set the `Filter` property to `F`.
- Insert a [`CreateMessage`] operator and configure the following properties:
    - `Payload` - Select `OutputPulseEnablePayload`.
    - `OutputPulseEnable` - Select `DO0`.
- Insert a [`MulticastSubject`] operator named `Behavior Commands`.
- Insert a [`CreateMessage`] operator and configure the following properties:
    - `Payload` - Select `PulseDO0Payload`.
    - `PulseDO0` - Set the burst duration to 100 ms.
- Insert a [`MulticastSubject`] operator named `Behavior Commands`.

Run the workflow, press <kbd>F</kbd> once to configure the pulse, then press <kbd>S</kbd>. The PWM runs for 100 ms and stops on its own, which translates to either a short beep with a speaker or a brief flash with an LED.

[!INCLUDE [](outputpulseenable-warning.md)]

[!INCLUDE [](version-footer.md)]

<!--Reference Style Links -->
[`KeyDown`]: xref:Bonsai.Windows.Input.KeyDown
[`CreateMessage`]: xref:Harp.Behavior.CreateMessage
[`MulticastSubject`]: xref:Bonsai.Expressions.MulticastSubject
[`PwmFrequencyDO0`]: xref:Harp.Behavior.PwmFrequencyDO0
[`PwmDutyCycleDO0`]: xref:Harp.Behavior.PwmDutyCycleDO0
[`PwmStart`]: xref:Harp.Behavior.PwmStart
[`PwmStop`]: xref:Harp.Behavior.PwmStop
[`OutputPulseEnable`]: xref:Harp.Behavior.OutputPulseEnable
[`PulseDO0`]: xref:Harp.Behavior.PulseDO0
