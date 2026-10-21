> [!WARNING]
> [`OutputPulseEnable`](xref:Harp.Behavior.OutputPulseEnable) acts on every output of the Behavior board. Any output not specified in the payload will have its pulse mode disabled. Make sure to list every output that should keep pulse mode (e.g. `DO0, SupplyPort0`).
