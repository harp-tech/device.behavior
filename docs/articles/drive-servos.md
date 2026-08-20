## Drive Servos

The outputs **DO2** and **DO3** can drive standard hobby servo motors — for instance to move a lick spout or a door in and out of reach. Each servo has a period register ([`ServoMotor2Period`]) and a pulse-width register ([`ServoMotor2Pulse`]), both in microseconds, and the [`EnableServos`] and [`DisableServos`] registers switch the pulse train. The examples below use the servo on **DO2**.

The complete workflow is shown below:

:::workflow
![Drive Servos](../workflows/driveservos-toplevel.bonsai)
:::

### Configure the Servo

:::workflow
![Configure the Servo](../workflows/driveservos-configure.bonsai)
:::

- Insert a [`KeyDown`] operator and set the `Filter` property to `A`.
- Insert a [`CreateMessage`] operator and configure the following properties:
    - `Payload` - Select `ServoMotor2PeriodPayload`.
    - `ServoMotor2Period` - Set the period of the servo pulse train in µs (e.g. 20000 for the standard 50 Hz servo frame).
- Insert a [`MulticastSubject`] operator named `Behavior Commands`.
- Insert a [`CreateMessage`] operator and configure the following properties:
    - `Payload` - Select `ServoMotor2PulsePayload`.
    - `ServoMotor2Pulse` - Set the pulse width in µs (e.g. 1500 for the center position of a standard servo).
- Insert a [`MulticastSubject`] operator named `Behavior Commands`.

Run the workflow and press <kbd>A</kbd> to load the servo configuration. Check your servo's datasheet for its period and pulse-width range.

### Enable and Disable Servos

:::workflow
![Enable and Disable Servos](../workflows/driveservos-enabledisable.bonsai)
:::

- Insert a [`KeyDown`] operator and set the `Filter` property to `S`.
- Insert a [`CreateMessage`] operator and configure the following properties:
    - `Payload` - Select `EnableServosPayload`.
    - `EnableServos` - Select `ServoOutput2`.
- Insert a [`MulticastSubject`] operator named `Behavior Commands`.

In a separate branch:

- Insert a [`KeyDown`] operator and set the `Filter` property to `D`.
- Insert a [`CreateMessage`] operator and configure the following properties:
    - `Payload` - Select `DisableServosPayload`.
    - `DisableServos` - Select `ServoOutput2`.
- Insert a [`MulticastSubject`] operator named `Behavior Commands`.

Run the workflow, press <kbd>S</kbd> to start driving the servo and <kbd>D</kbd> to stop. The servo moves to the position set by the pulse width and holds it while enabled.

### Adjust the Position

While the servo is enabled, writing a new pulse width moves it immediately:

:::workflow
![Adjust the Position](../workflows/driveservos-position.bonsai)
:::

- Insert a [`KeyDown`] operator and set the `Filter` property to `F`.
- Insert a [`CreateMessage`] operator and configure the following properties:
    - `Payload` - Select `ServoMotor2PulsePayload`.
    - `ServoMotor2Pulse` - Set a new pulse width in µs (e.g. 2000).
- Insert a [`MulticastSubject`] operator named `Behavior Commands`.

Run the workflow, enable the servo with <kbd>S</kbd>, then press <kbd>F</kbd> — the servo moves to the new position.

> [!WARNING]
> **DO2** and **DO3** share their hardware timers with the [PWM generator](generate-pwm.md). Don't run PWM and a servo on the same output at the same time.

[!INCLUDE [](version-footer.md)]

<!--Reference Style Links -->
[`KeyDown`]: xref:Bonsai.Windows.Input.KeyDown
[`CreateMessage`]: xref:Harp.Behavior.CreateMessage
[`MulticastSubject`]: xref:Bonsai.Expressions.MulticastSubject
[`ServoMotor2Period`]: xref:Harp.Behavior.ServoMotor2Period
[`ServoMotor2Pulse`]: xref:Harp.Behavior.ServoMotor2Pulse
[`EnableServos`]: xref:Harp.Behavior.EnableServos
[`DisableServos`]: xref:Harp.Behavior.DisableServos
