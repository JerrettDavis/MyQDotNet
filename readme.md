# MyQDotNet

MyQDotNet is an unofficial C# wrapper for the [MyQ Api](https://www.myq.com/).

*Warning: MyQ does not have any public API. This API is provided as-is, with no warranty or guarantees. 
Use at your own risk.*

## Usage

### Basic Usage
```csharp
var api = new MyQ(new HttpClient());
await api.Authenticate("username", "password");

var garageDoor = api.Devices.Where(d => d.DeviceFamily == "garagedoor")
                            .Cast<GarageDoor>()
                            .First();

await garageDoor.Open();
Thread.Sleep(15000);
await garageDoor.Close();
```

## Contributing
Pull requests are welcome. For major changes, please open an issue first to discuss what you would like to change.

Please make sure to update tests as appropriate.

## License
[MIT](https://choosealicense.com/licenses/mit/)