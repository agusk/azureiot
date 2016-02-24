// Add reference to the Azure IoT Hub device library
var device = require('azure-iot-device');
// Define the connection string to connect to IoT Hub
var connectionString = 'HostName=JakartaHub.azure-devices.net;DeviceId=MyJakartaNode;SharedAccessKey=C9He8JTmpjMu4ESGkmv5ubT9v0soTpkRQoHu1fWFLJ8=';
// Create the client instance specifying the preferred protocol
var client = device.Client.fromConnectionString(connectionString);
// Create a message and send it to IoT Hub.
var data = JSON.stringify({ 'deviceId': 'MyJakartaNode', 'data': 'mydata' });
var message = new device.Message(data);
message.properties.add('myproperty', 'myvalue');
client.sendEvent(message, function(err, res){
    if (err) console.log('SendEvent error: ' + err.toString());
    if (res) console.log('SendEvent status: ' + res.statusCode + ' ' + res.statusMessage);
});
// Receive messages from IoT Hub
client.receive(function (err, res, msg) {
  console.log('receive data: ' + msg);
  client.complete(msg, function(err, res){
    if (err) console.log('Complete error: ' + err.toString());
    if (res) console.log('Complete status: ' + res.statusCode + ' ' + res.statusMessage);
  });
});