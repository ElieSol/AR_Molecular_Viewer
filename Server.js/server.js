"use strict";

var http = require("http");
var fs = require('fs');
class Server
{
    constructor()
    {
        this.port = 8080;
        this.ip = "localhost";

        this.start();
    }

    start()
    {
        this.server = http.createServer((req, res) =>
        {
            this.processRequest(req, res);
        });

        this.server.on("clientError", (err, socket) =>
        {
            socket.end("HTTP/1.1 400 Bad Request\r\n\r\n");
        });
        console.log("Server created");
    }

    listen()
    {
        this.server.listen(this.port, this.ip);
        console.log("Server listening for connections");
    }

    processRequest(req, res)
    {
        // Process the request from the client
        // We are only supporting POST
        if (req.method === "POST")
        {
            // Post data may be sent in chunks so need to build it up
            var body = "";
            req.on("data", (data) =>
            {
                body += data;
                // Prevent large files from benig posted
                if (body.length > 1024)
                {
                    // Tell Unity that the data sent was too large
                    res.writeHead(413, "Payload Too Large", {"Content-Type": "text/html"});
                    res.end("Error 413");
                }
            });
            req.on("end", () =>
            {
                // Now that we have all data from the client, we process it
                console.log("Received data: " + body);
                // Split the key / pair values and print them out
                console.log();
                //Remplacer les elements dans le body pour obtenir un url valide
                //grep and replace
                //http%3a%2f%2ffiles.rcsb.org%2fview%2f2XKG.pdb
                //http://files.rcsb.org/view/2XKG.pdb
                //%3a%2f%2f
                //%2f
                var pre = body.replace("%3a%2f%2f","://");
                var preurl=pre.replace("%2f","/");
                var url=preurl.replace("%2f","/");
                console.log(url);
                //
                var file = fs.createWriteStream("file.pdb");
                var request = http.get(url, function(response) {response.pipe(file);});
                var exec = require('child_process').exec;
                exec('pymol '+url);
                exec('pymol script.pml');
                // Tell Unity that we received the data OK
                res.writeHead(200, {"Content-Type": "text/plain"});
                res.end("Files Created");
                // envoyer les fichier cree au clients
                
            });
        }
        else
        {
            // Tell Unity that the HTTP method was not allowed
            res.writeHead(405, "Method Not Allowed", {"Content-Type": "text/html"});
            res.end("Error 405");
        }
    }

}

module.exports.Server = Server;
