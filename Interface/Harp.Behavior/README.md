## About

`Harp.Behavior` provides an asynchronous API and reactive operators for data acquisition and control of [Harp Behavior](https://harp-tech.org/api/Harp.Behavior.html) devices.

## How to Use

To use `Harp.Behavior` for visual reactive programming, please install this package using the [Bonsai package manager](https://bonsai-rx.org/docs/articles/packages.html).

The package can also be used from any .NET application:
```c#
using Harp.Behavior;

using var device = await Device.CreateAsync("COM3");
var whoAmI = await device.ReadWhoAmIAsync();
var deviceName = await device.ReadDeviceNameAsync();
var timestamp = await device.ReadTimestampSecondsAsync();
Console.WriteLine($"{deviceName} WhoAmI: {whoAmI} Timestamp (s): {timestamp}");
```

## Additional Documentation

For additional documentation and examples, refer to the [official Harp documentation](https://harp-tech.org/api/Harp.Behavior.html).

## Feedback & Contributing

`Harp.Behavior` is released as open-source under the [MIT license](https://licenses.nuget.org/MIT). Bug reports and contributions are welcome at [the GitHub repository](https://github.com/harp-tech/device.behavior).