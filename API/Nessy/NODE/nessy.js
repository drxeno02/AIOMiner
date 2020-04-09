const cors = require('cors');
var express = require("express");
var backend = require('./routes/backend_routes');

var bodyParser = require('body-parser');

var paypal = require('paypal-rest-sdk');

process.setMaxListeners(0);

var app = express();

app.use(bodyParser.urlencoded({ extended: true }));
app.use(bodyParser.json());
app.use(function(req, res, next) {
    res.header("Access-Control-Allow-Origin", "*");
    res.header("Access-Control-Allow-Headers", "Origin, X-Requested-With, Content-Type, Accept");
    next();
});
var router = express.Router();

// test route
router.get('/', function(req, res) {
    res.json({ message: 'OK!' });
});



// endpt for paypal events to us
router.post('/pPalHook', backend.handlePaypalPost)


app.use('/api', router);

app.use(cors());
app.options('*', cors());

console.log('     Nessy about to listen on port 5008 ....');

app.listen(5008);