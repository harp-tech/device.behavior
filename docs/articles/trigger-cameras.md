## Trigger Cameras

The Behavior board generates camera trigger pulses on **DO0** and **DO1**, so video frames are captured and timestamped on the device clock. The examples below use camera 0 on **DO0**.

This article covers how to configure the trigger frequency, start and stop the cameras, and visualize frame events in Bonsai.

The complete workflow is shown below:

:::workflow
![Trigger Cameras](../workflows/triggercameras-toplevel.bonsai)
:::

### Configure Trigger Frequency

The trigger frequency of each camera is set with the [`Camera0Frequency`] register:

:::workflow
![Configure Trigger Frequency](../workflows/triggercameras-frequency.bonsai)
:::

- Insert a [`KeyDown`] operator and set the `Filter` property to `A`.
- Insert a [`CreateMessage`] operator and configure the following properties:
    - `Payload` - Select `Camera0FrequencyPayload`.
    - `Camera0Frequency` - Set the trigger frequency in Hz (e.g. 30). Valid values are 2 to 600.
- Insert a [`MulticastSubject`] operator named `Behavior Commands`.

Run the workflow and press <kbd>A</kbd> to set the trigger frequency. Configure the frequency before starting the camera; the value is read when triggering starts.

### Start and Stop Cameras

Triggering is switched with the [`StartCameras`] and [`StopCameras`] registers:

:::workflow
![Start and Stop Cameras](../workflows/triggercameras-startstop.bonsai)
:::

- Insert a [`KeyDown`] operator and set the `Filter` property to `S`.
- Insert a [`CreateMessage`] operator and configure the following properties:
    - `Payload` - Select `StartCamerasPayload`.
    - `StartCameras` - Select `CameraOutput0`.
- Insert a [`MulticastSubject`] operator named `Behavior Commands`.

In a separate branch:

- Insert a [`KeyDown`] operator and set the `Filter` property to `D`.
- Insert a [`CreateMessage`] operator and configure the following properties:
    - `Payload` - Select `StopCamerasPayload`.
    - `StopCameras` - Select `CameraOutput0`.
- Insert a [`MulticastSubject`] operator named `Behavior Commands`.

Run the workflow, press <kbd>S</kbd> to start the trigger train on **DO0** and <kbd>D</kbd> to stop it. The stop command lets the current trigger pulse finish cleanly: the device broadcasts a [`StopCameras`] event when the last pulse has actually been emitted, so no truncated frame reaches the camera. Triggering also stops automatically when the device leaves active mode.

### Visualize Frame Events

Each trigger pulse broadcasts a [`Camera0Frame`] event, giving the device timestamp of every captured frame:

:::workflow
![Visualize Frame Events](../workflows/triggercameras-visualizeframes.bonsai)
:::

- Insert a [`SubscribeSubject`] operator named `Behavior Events`.
- Insert a [`Parse`] operator and configure the `Register` property to `Timestamped<Camera0Frame>`.
- Insert a [`VisualizerWindow`] operator. This will automatically open a window displaying the parsed events when the workflow starts.

Run the workflow and press <kbd>S</kbd>. The visualizer displays:

```text
FrameAcquired@10.351072
```

The first value confirms the frame trigger and the second is the timestamp on the device clock. Match these timestamps against the frames saved by your video capture software to align video with the rest of the data.

> [!WARNING]
> Each output has a single hardware timer, shared between its camera trigger and [PWM](generate-pwm.md) functions. Don't run PWM and a camera trigger on the same output at the same time. Combining them across outputs (e.g. a camera trigger on **DO0** and PWM on **DO1**) is fine.

[!INCLUDE [](version-footer.md)]

<!--Reference Style Links -->
[`KeyDown`]: xref:Bonsai.Windows.Input.KeyDown
[`CreateMessage`]: xref:Harp.Behavior.CreateMessage
[`MulticastSubject`]: xref:Bonsai.Expressions.MulticastSubject
[`SubscribeSubject`]: xref:Bonsai.Expressions.SubscribeSubject
[`Parse`]: xref:Harp.Behavior.Parse
[`VisualizerWindow`]: xref:Bonsai.Design.VisualizerWindow
[`Camera0Frequency`]: xref:Harp.Behavior.Camera0Frequency
[`StartCameras`]: xref:Harp.Behavior.StartCameras
[`StopCameras`]: xref:Harp.Behavior.StopCameras
[`Camera0Frame`]: xref:Harp.Behavior.Camera0Frame
