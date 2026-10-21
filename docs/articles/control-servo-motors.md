## Control Servo Motors

The digital outputs **DO2** and **DO3** can control positional servo motors that accept a pulse-width signal. For instance, it can be used to move a lick spout in and out of reach, or open and close a door. Refer to the [connections](./connections.md?tabs=servo#connections) article to set up the servo motor on **DO2**, which we will use for the rest of these examples.

This article covers how to configure the servo, enable and disable the pulse-width signal, and adjust the position in Bonsai.

The complete workflow is shown below. Copy and paste it into Bonsai to get started immediately, or build each section by following the step-by-step instructions in the following sections.

:::workflow
![Control Servo Motors](../workflows/controlservomotors-toplevel.bonsai)
:::

### Configure the Servo

To configure the servo, set the period and pulse-width registers for the digital output port (e.g. [`ServoMotor2Period`] and [`ServoMotor2Pulse`] for **DO2**) in microseconds. The period determines the servo update rate (i.e. how often the servo receives a position command), which can affect how smoothly and reliably it responds. The pulse width determines the servo's target angle.

:::workflow
![Configure the Servo](../workflows/controlservomotors-configure.bonsai)
:::

- Insert a [`KeyDown`] operator and set the `Filter` property to `A`.
- Insert a [`CreateMessage`] operator and configure the following properties:
    - `Payload` - Select `ServoMotor2PeriodPayload`.
    - `ServoMotor2Period` - Set the period of the servo control pulse in µs (e.g. `20000` for a 50 Hz servo update rate, typical of positional servos).
- Insert a [`MulticastSubject`] operator named `Behavior Commands`.

In a separate branch:

- Insert a [`KeyDown`] operator and set the `Filter` property to `S`.
- Insert a [`CreateMessage`] operator and configure the following properties:
    - `Payload` - Select `ServoMotor2PulsePayload`.
    - `ServoMotor2Pulse` - Set the pulse width in µs (e.g. `1500` for the center position of a positional servo, the range is typically 1000–2000 µs).
- Insert a [`MulticastSubject`] operator named `Behavior Commands`.

Run the workflow, press <kbd>A</kbd> to set the servo update rate and <kbd>S</kbd> to set the servo starting angle. The two commands are bound to separate keys, so you can reset the angle later without reconfiguring the period. Check your servo's datasheet for its period and pulse width range if you are unsure about which values to use.

### Enable and Disable Servos

Use the [`EnableServos`] and [`DisableServos`] registers to turn on and off the servo:

:::workflow
![Enable and Disable Servos](../workflows/controlservomotors-enabledisable.bonsai)
:::

- Insert a [`KeyDown`] operator and set the `Filter` property to `D`.
- Insert a [`CreateMessage`] operator and configure the following properties:
    - `Payload` - Select `EnableServosPayload`.
    - `EnableServos` - Select the servo output to activate (e.g. `ServoOutput2`).
- Insert a [`MulticastSubject`] operator named `Behavior Commands`.

In a separate branch:

- Insert a [`KeyDown`] operator and set the `Filter` property to `F`.
- Insert a [`CreateMessage`] operator and configure the following properties:
    - `Payload` - Select `DisableServosPayload`.
    - `DisableServos` - Select the servo output to deactivate (e.g. `ServoOutput2`).
- Insert a [`MulticastSubject`] operator named `Behavior Commands`.

Run the workflow, press <kbd>D</kbd> to enable the servo and <kbd>F</kbd> to stop. The servo moves to the starting position which was configured previously.

### Adjust the Position

While the servo is enabled, writing a new pulse width moves it immediately:

:::workflow
![Adjust the Position](../workflows/controlservomotors-position.bonsai)
:::

- Insert a [`KeyDown`] operator and set the `Filter` property to `G`.
- Insert a [`CreateMessage`] operator and configure the following properties:
    - `Payload` - Select `ServoMotor2PulsePayload`.
    - `ServoMotor2Pulse` - Set a new pulse width in µs (e.g. `2000`).
- Insert a [`MulticastSubject`] operator named `Behavior Commands`.

Run the workflow, then press <kbd>G</kbd> to move the servo to a new angle. Press <kbd>S</kbd> to move it back to the starting angle.

[!INCLUDE [](version-footer.md)]

<!--Reference Style Links -->
[`KeyDown`]: xref:Bonsai.Windows.Input.KeyDown
[`CreateMessage`]: xref:Harp.Behavior.CreateMessage
[`MulticastSubject`]: xref:Bonsai.Expressions.MulticastSubject
[`ServoMotor2Period`]: xref:Harp.Behavior.ServoMotor2Period
[`ServoMotor2Pulse`]: xref:Harp.Behavior.ServoMotor2Pulse
[`EnableServos`]: xref:Harp.Behavior.EnableServos
[`DisableServos`]: xref:Harp.Behavior.DisableServos
