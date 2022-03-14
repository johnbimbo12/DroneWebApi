# DroneWebApi

DroneWebApi is a REST Web API developed to facilitate client communication with drones, using a single controller (DispatchController).  It is written in C#.

Input/Output data are in JSON format.


## Usage

```json
//registering a drone;

https://localhost:5001/api/Dispatch/RegisterDrone

-POST
{
  "serialNumber": "DR_Auto6",
  "model": "Cruiserweight",
  "weightLimit": 550,
  "batteryCapacity": 100
}
```
```json
//loading a drone with medication items
https://localhost:5001/api/Dispatch/LoadMedications

-POST
[
    {
        "name": "Multivitamins000_A",
        "weight": 50,
        "code": "MD_MTVa",
        "image": null,
        "droneId": 15
    },
    {
        "name": "Iron-3",
        "weight": 100,
        "code": "MD_IRN",
        "image": null,
        "droneId": 15
    }
]

```
```json
//checking loaded medication items for a given drone
https://localhost:5001/api/Dispatch/GetDrone/15
```

```json
//checking available drones for loading
https://localhost:5001/api/Dispatch/GetAvailableDrones
```

```json
//check drone battery level for a given drone
https://localhost:5001/api/Dispatch/GetDroneBatteryLevel/10
```

## Contributing
Pull requests are welcome. For major changes, please open an issue first to discuss what you would like to change.

Please make sure to update tests as appropriate.

## License
[MIT](https://choosealicense.com/licenses/mit/)
