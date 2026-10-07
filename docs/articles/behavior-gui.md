## Behavior GUI

The Behavior GUI is a standalone visual interface for configuring and testing the device without using Bonsai.

Before beginning, [install the GUI](installation.md#software-packages), follow the hardware setup [guide](connections.md), and connect the USB cable to the computer. Launch "Harp.Behavior.App" from the Windows Start menu.

![Behavior GUI](../images/behavior-gui.png)

1. Select the port for the device, and press "Connect". The device details will display on the right side if it is successfully connected.
2. Select the tab for the register group that you want to control.
3. Change the values to test outputs, LEDs, cameras, and servos, and observe the output on the connected peripherals.

> [!WARNING]
> Only one program can access the device's COM port at a time. Close the GUI before starting a Bonsai workflow that uses the device.

[!INCLUDE [](version-footer.md)]
