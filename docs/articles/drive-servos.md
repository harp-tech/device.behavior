## Drive Servos

The digital outputs **DO2** and **DO3** can drive standard servo motors, which can be used, for instance, to move a lick spout or a door in and out of reach. Refer to the [connections](./connections.md) article to set up the servo motor on **DO2**, which we will use for the rest of these examples.

This article covers how to configure the servo, enable and disable the pulse train, and adjust the position in Bonsai.

The complete workflow is shown below:

:::workflow
![Drive Servos](../workflows/driveservos-toplevel.bonsai)
:::

### Configure the Servo

To configure the servo, set the period and pulse-width registers for the digital output ([`ServoMotor2Period`] and [`ServoMotor2Pulse`] for **DO2**) in microseconds. The period sets the refresh rate for the servo update and the pulse width sets the servo angle.

:::workflow
![Configure the Servo](../workflows/driveservos-configure.bonsai)
:::

- Insert a [`KeyDown`] operator and set the `Filter` property to `A`.
- Insert a [`CreateMessage`] operator and configure the following properties:
    - `Payload` - Select `ServoMotor2PeriodPayload` to configure .
    - `ServoMotor2Period` - Set the period of the servo pulse train in µs (e.g. 20000 for a 50 Hz servo update).
- Insert a [`MulticastSubject`] operator named `Behavior Commands`.

In a separate branch:

- Insert a [`KeyDown`] operator and set the `Filter` property to `S`.
- Insert a [`CreateMessage`] operator and configure the following properties:
    - `Payload` - Select `ServoMotor2PulsePayload`.
    - `ServoMotor2Pulse` - Set the pulse width in µs (e.g. 1500 for the center position of a standard servo, the range is typically 1000 – 2000 µs).
- Insert a [`MulticastSubject`] operator named `Behavior Commands`.

Run the workflow, press <kbd>A</kbd> to set the servo period and <kbd>S</kbd> to move the servo to the starting angle. The two registers are bound to separate keys, so you can reset the angle later without reconfiguring the period. Check your servo's datasheet for its period and pulse-width range if you are unsure about which values to use.

### Enable and Disable Servos

The [`EnableServos`] and [`DisableServos`] registers switch the servo pulse train:

:::workflow
![Enable and Disable Servos](../workflows/driveservos-enabledisable.bonsai)
:::

- Insert a [`KeyDown`] operator and set the `Filter` property to `D`.
- Insert a [`CreateMessage`] operator and configure the following properties:
    - `Payload` - Select `EnableServosPayload`.
    - `EnableServos` - Select `ServoOutput2`.
- Insert a [`MulticastSubject`] operator named `Behavior Commands`.

In a separate branch:

- Insert a [`KeyDown`] operator and set the `Filter` property to `F`.
- Insert a [`CreateMessage`] operator and configure the following properties:
    - `Payload` - Select `DisableServosPayload`.
    - `DisableServos` - Select `ServoOutput2`.
- Insert a [`MulticastSubject`] operator named `Behavior Commands`.

Run the workflow, press <kbd>D</kbd> to enable the servo and <kbd>F</kbd> to stop. The servo moves to the position set by the pulse width and holds it while enabled.

### Adjust the Position

While the servo is enabled, writing a new pulse width moves it immediately:

:::workflow
![Adjust the Position](../workflows/driveservos-position.bonsai)
:::

- Insert a [`KeyDown`] operator and set the `Filter` property to `G`.
- Insert a [`CreateMessage`] operator and configure the following properties:
    - `Payload` - Select `ServoMotor2PulsePayload`.
    - `ServoMotor2Pulse` - Set a new pulse width in µs (e.g. 2000).
- Insert a [`MulticastSubject`] operator named `Behavior Commands`.

Run the workflow, then press <kbd>G</kbd> to move the servo to a new position. Press <kbd>S</kbd> to move it back to the starting angle.

[!INCLUDE [](version-footer.md)]

<!--Reference Style Links -->
[`KeyDown`]: xref:Bonsai.Windows.Input.KeyDown
[`CreateMessage`]: xref:Harp.Behavior.CreateMessage
[`MulticastSubject`]: xref:Bonsai.Expressions.MulticastSubject
[`ServoMotor2Period`]: xref:Harp.Behavior.ServoMotor2Period
[`ServoMotor2Pulse`]: xref:Harp.Behavior.ServoMotor2Pulse
[`EnableServos`]: xref:Harp.Behavior.EnableServos
[`DisableServos`]: xref:Harp.Behavior.DisableServos
