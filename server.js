var connect = require("connect");

connect(
  connect.static(__dirname + '/tactclient')
).listen(3000);

