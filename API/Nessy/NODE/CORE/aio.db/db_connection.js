//Connects to the MySQL database








var pool;



var mysql = require('mysql');
// var SuperSekret = 'XXX'
// var ServerHostName = "{{WEBSITE}}"
pool = mysql.createPool({
    connectionLimit: 100,
    host: 'xxxx',
    port: '3306',
    user: 'XXXX',
    password: 'XXXX',
    database: 'XXXX'
});

mysql = null;

module.exports = pool;