## Errors

This article covers how to resolve common connection errors on the Behavior board.

### COM Port Errors

**Q: In Bonsai, running the workflow throws an error "The port `ComX` does not exist."**

A: Either the wrong communications port in the `PortName` property in [`Device`] was selected, or the [USB](connections.md) cable is not properly connected. Try selecting a different communications port and checking the connection.

**Q: In Bonsai, running the workflow throws an error "Access to the port `ComX` is denied"**

A: Only one interface connection to the Behavior board can be opened at one time. Check that multiple instances of Bonsai are not running, and that the [Behavior GUI](behavior-gui.md) is closed. Sometimes, the port can also be locked by a program that did not terminate correctly; restarting the computer fixes it.

### Device Errors

**Q: Poke events stopped arriving from Port 2.**

A: Port 2 repurposes its input lines while the [quadrature encoder](track-rotary-encoder.md) is enabled. Disable the encoder to restore poke detection.

[!INCLUDE [](version-footer.md)]

<!--Reference Style Links -->
[`Device`]: xref:Harp.Behavior.Device
