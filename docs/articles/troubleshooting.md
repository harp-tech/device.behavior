## Errors

This article covers how to resolve common connection errors on the Behavior board.

### COM Port Errors

**Q: In Bonsai, running the workflow throws an error "The port `ComX` does not exist."**

A: Either the wrong communications port in the `PortName` property in [`Device`] was selected, or the [USB](connections.md) cable is not properly connected. Try selecting a different communications port and checking the connection.

**Q: In Bonsai, running the workflow throws an error "Access to the port `ComX` is denied"**

A: Only one interface connection to the Behavior board can be opened at one time. Check that multiple instances of Bonsai are not running, and that the [Behavior GUI](behavior-gui.md) is closed. Sometimes, the port can also be locked by a program that did not terminate correctly; restarting the computer fixes it.

### Device Errors

**Q: Poke events stopped arriving from P2.**

A: One possible reason is that the [quadrature encoder](track-rotary-encoder.md) is enabled, as **P2** repurposes its input lines for the quadrature counter. Disable the encoder to restore poke detection.

**Q: An external device does not detect the low state of a digital output. The line reads 5 V when the output is set, but does not reach 0 V when it is cleared.**

A: The input line on the external device might have low impedance. The **DO0**–**DO3** outputs are designed for high-impedance inputs and return to 0 V through a passive pull-down resistor. Try using the peripheral port digital outputs with the [Breakout](./peripherals/peripherals-portbreakout.md) board, as those pins have an actively driven low state.

Also check that the ground connection is shared between the devices, as improper grounding can produce offset voltage levels in the low state.

**Q: How do I use the DIO pins on the peripheral ports as digital outputs?**

A: The **DIO** line on ports **P0**–**P2** work as a digital input in the current firmware (v3.3), as the functionality to switch the pins from digital input to digital output have not been implemented yet. Refer to the [Read Digital Inputs](./read-digital-inputs.md) article to read those pins.

[!INCLUDE [](version-footer.md)]

<!--Reference Style Links -->
[`Device`]: xref:Harp.Behavior.Device
