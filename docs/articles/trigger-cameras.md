## Trigger Cameras

The Behavior board can generate TTL signals for cameras that support external triggering on **DO0** and **DO1**. This is useful for synchronizing multiple cameras and capturing camera frame timestamps on the device clock. Refer to the [connections](./connections.md) article to set up a compatible camera on **DO1**, which we will use for the rest of these examples.

This article covers how to configure the trigger frequency, start and stop the cameras, and visualize frame events in Bonsai.

The complete workflow is shown below. Copy and paste it into Bonsai or build each section by following the step-by-step instructions below.

:::workflow
![Trigger Cameras](../workflows/triggercameras-toplevel.bonsai)
:::

> [!TIP]
> Configure your camera to trigger on the rising edge of the pulse, and avoid modes that use the trigger pulse width to control exposure. Check that the voltage level is compatible with the camera's external trigger voltage level. The trigger signal is a 5 V square wave with a 50% duty cycle and the pulse width is not configurable.

### Configure Trigger Frequency

The trigger frequency of each camera is set with the appropriate register for the port, so  [`Camera1Frequency`] for **DO1**:

:::workflow
![Configure Trigger Frequency](../workflows/triggercameras-frequency.bonsai)
:::

- Insert a [`KeyDown`] operator and set the `Filter` property to `A`.
- Insert a [`CreateMessage`] operator and configure the following properties:
    - `Payload` - Select `Camera1FrequencyPayload`.
    - `Camera1Frequency` - Set the trigger frequency in Hz (e.g. 30). Valid values are 2 to 600.
- Insert a [`MulticastSubject`] operator named `Behavior Commands`.

Run the workflow and press <kbd>A</kbd> to set the trigger frequency. Configure the frequency before starting the camera. Once camera triggering starts, the frequency cannot be modified without stopping the camera.

### Start and Stop Cameras

Enable and disable the camera triggering with the [`StartCameras`] and [`StopCameras`] registers:

:::workflow
![Start and Stop Cameras](../workflows/triggercameras-startstop.bonsai)
:::

- Insert a [`KeyDown`] operator and set the `Filter` property to `S`.
- Insert a [`CreateMessage`] operator and configure the following properties:
    - `Payload` - Select `StartCamerasPayload`.
    - `StartCameras` - Select `CameraOutput1` for the camera connected to **DO1**.
- Insert a [`MulticastSubject`] operator named `Behavior Commands`.

In a separate branch:

- Insert a [`KeyDown`] operator and set the `Filter` property to `D`.
- Insert a [`CreateMessage`] operator and configure the following properties:
    - `Payload` - Select `StopCamerasPayload`.
    - `StopCameras` - Select `CameraOutput1` for the camera connected to **DO1**.
- Insert a [`MulticastSubject`] operator named `Behavior Commands`.

Run the workflow, press <kbd>S</kbd> to start the camera trigger on **DO1** and <kbd>D</kbd> to stop it. Check the image stream in either Bonsai with a camera source operator like [`CameraCapture`] or in your camera vendor's acquisition software to confirm that the external trigger is working.

> [!NOTE]
> Camera triggering stops automatically when the Bonsai workflow stops and the device goes into standby mode. This behavior is specific to camera triggering, other digital outputs keep their last state when the workflow stops. Remember to start the camera trigger again when you restart the workflow.

### Visualize Frame Events

Each trigger pulse broadcasts a frame event tied to the port like [`Camera1Frame`]. Use it to verify that the trigger signal is being generated. Each message will also carry the recorded timestamp for the pulse on the device clock:

:::workflow
![Visualize Frame Events](../workflows/triggercameras-visualizeframes.bonsai)
:::

- Insert a [`SubscribeSubject`] operator named `Behavior Events`.
- Insert a [`Parse`] operator and configure the `Register` property to `Timestamped<Camera1Frame>`.
- Insert a [`VisualizerWindow`] operator. This will automatically open a window displaying the parsed events when the workflow starts.

Run the workflow and press <kbd>S</kbd>. The visualizer displays:

```text
FrameAcquired@10.351072
```

The first value confirms the frame trigger and the second is the timestamp on the device clock. Match the [logged](./logging-analysis.md) timestamps against the frames saved by your video capture software to align video with the rest of the data.


[!INCLUDE [](version-footer.md)]

<!--Reference Style Links -->
[`KeyDown`]: xref:Bonsai.Windows.Input.KeyDown
[`CameraCapture`]: xref:Bonsai.Vision.CameraCapture
[`CreateMessage`]: xref:Harp.Behavior.CreateMessage
[`MulticastSubject`]: xref:Bonsai.Expressions.MulticastSubject
[`SubscribeSubject`]: xref:Bonsai.Expressions.SubscribeSubject
[`Parse`]: xref:Harp.Behavior.Parse
[`VisualizerWindow`]: xref:Bonsai.Design.VisualizerWindow
[`Camera1Frequency`]: xref:Harp.Behavior.Camera1Frequency
[`StartCameras`]: xref:Harp.Behavior.StartCameras
[`StopCameras`]: xref:Harp.Behavior.StopCameras
[`Camera1Frame`]: xref:Harp.Behavior.Camera1Frame
