> [!WARNING]
> [`OutputPulseEnable`](xref:Harp.Behavior.OutputPulseEnable) is a selection mask over all digital outputs, any output not selected in the payload has its pulse mode disabled. Make sure to list every output that should keep pulse mode (e.g. `DO0`, `SupplyPort0`).
