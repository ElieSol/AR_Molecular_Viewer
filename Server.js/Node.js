var http = require('http');
var fs = require('fs');

http.createServer(function (req, res) {
  var file = fs.createWriteStream("file.pdb");
  var request = http.get("http://files.rcsb.org/view/2XKG.pdb", function(response) {
    response.pipe(file);

  });
  var exec = require('child_process').exec;
  exec('pymol http://files.rcsb.org/view/2XKG.pdb');
	exec('pymol script.pml');
    // execution Python


    // Recuperer .obj


    //Envoi lunettes

}).listen(8001);
