var device = require('azure-iot-device');

// String containing Hostname, Device Id & Device Key in the following formats:
//  "HostName=<iothub_host_name>;DeviceId=<device_id>;SharedAccessKey=<device_key>"
//var connectionString = "[IoT Hub device connection string]";
var connectionString = "HostName=JakartaHub.azure-devices.net;DeviceId=MyJakartaNode;SharedAccessKey=C9He8JTmpjMu4ESGkmv5ubT9v0soTpkRQoHu1fWFLJ8=";
// Create IoT Hub client
var client = device.Client.fromConnectionString(connectionString);

// Helper function to print results for an operation
function printErrorFor(op) {
  return function printError(err) {
    if (err) console.log(op + ' error: ' + err.toString());
  };
}


// function to wait on messages
var isWaiting = false;
function waitForMessages() {
  isWaiting = true;
  client.receive(function (err, msg, res) {
    printErrorFor('receive')(err, res);
    if (!err && res.statusCode !== 204) {
      console.log('receive data: ' + msg.getData());
      try {
        var command = JSON.parse(msg.getData());
        if (command.Name === "SetTemperature") {
          temperature = command.Parameters.Temperature;
          console.log("New temperature set to :" + temperature + "F");
        }

        client.complete(msg, printErrorFor('complete'));
      }
      catch (err) {
        printErrorFor('parse received message')(err);
        client.reject(msg, printErrorFor('reject'));
      }
    }
    isWaiting = false;
  });
}

// Start messages listener
setInterval(function () {
  if (!isWaiting) waitForMessages();
}, 200);


