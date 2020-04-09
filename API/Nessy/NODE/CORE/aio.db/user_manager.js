var express = require('express');
var router = express.Router();
var connection = require('./db_connection');
var mandrill = require('mandrill-api/mandrill');
var mandrill_client = new mandrill.Mandrill('XXX');
var ServerHostName = "{{WEBSITE}}";
function isEmpty(strIn)
{
    if (strIn === undefined)
    {
        return true;
    }
    else if(strIn == null)
    {
        return true;
    }
    else if(strIn == "")
    {
        return true;
    }
    else
    {
        return false;
    }
}


function sendConfirm(email,res,req,aioemailcheck){

        let provider = req.body.provider;
        var template_name ="";
        if(provider == "XXX"){
          template_name = "XXX-Confirmation";
        }
        else{
          template_name = "AIO-Confirmation";
        }
        var template_content = [{
        "name": "CODE",
        "content": aioemailcheck
        
          }];
        var message = {
          "html": "",
          "text": "test1234",
          "subject": "Confirmimation Email : Welcome!",
          "from_email": "DoNotReply@AIOMiner.com",
          "from_name": "AIOMiner",
          "to": [{
              "email": email,
              "type": "to"
            }],
            "headers": {
              "Reply-To": "DoNotReplay@AIOMiner.com"
            },
            "merge_language": "handlebars",
            "merge": true,
            "global_merge_vars": [{
                    "name": "CODE",
                    "content":  aioemailcheck
             }],
            }
            console.log("Sending confirmation email");
            mandrill_client.messages.sendTemplate({"template_name": template_name, "template_content": template_content, "message": message}, function(result) {
            res.status(200).send({"Status":"New User Registered"}); 
          });












}

//autheticate user and issue tocken to get access to rest of API
router.post('/authenticate',function(req,res){
  var match = false;
  if (isEmpty(req.body.email)) {
  	res.status(400).send({"error":"Blank Username"});
  }
  else if(isEmpty(req.body.password)){
    res.status(400).send({"error": "Blank Password"})
  }
  else{
  
  let user_id = req.body.email;
	connection.query("SELECT * FROM AIOMINER.KLASSY_DICK WHERE email=?",[user_id],function(err,result){
    const bcrypt = require('bcrypt');
  	var jwt = require('jsonwebtoken');
    var config = require('../config');
    if (err) throw err;
		bcrypt.compare(req.body.password,result[0].password,function(err,resolved){
      if(resolved){
      console.log("authenticated,issuing token");
      const payload = {
        "id":result[0].id,
        "email":result[0].email
      }
      var token = jwt.sign(payload, config.secret, {
          expiresIn: 60*60*3 // expires in 3 hours
        });
      res.json({
        success : true,
        message : 'Token issued',
        token : token
      });

      }
      else{
      res.status(400).send({"error":"Invalid Password"});
      }
    });
  


 });

 






  }


});

//insert new user into database
router.post('/User',function(req,res){
	const bcrypt = require('bcrypt');
	var today = new Date();
	var salt = bcrypt.genSaltSync(10);
	var hash = bcrypt.hashSync(req.body.password, salt);
	var apikey = require("apikeygen").apikey;
	var aioapi = apikey()
	var validator = require('validator');
	let email = req.body.email;
  // Check to see if this APIKey has been used, if so get another
  var aioemailcheck = apikey(15)
  var mandrill = require('mandrill-api/mandrill');
  var mandrill_client = new mandrill.Mandrill('');
  // Check if username or password is missing
 
  if (isEmpty(req.body.email)) {
  	res.status(400).send({"error":"Blank Username"});
  }
  else if(isEmpty(req.body.password)){
    res.status(400).send({"error": "Blank Password"})
  }
  else {
  	var users={
  		"email":req.body.email,
  		"hash" : aioemailcheck,
      "disabled" : "1",
      "password" : hash

    }
    let emailcheck = req.body.email;
  //Check if e-mail already exists first
  connection.query('SELECT * FROM KLASSY_DICK WHERE email = ?',[emailcheck], function (error, results, fields) {
  	if (error) {
  		console.log("error ocurred",error);
  		res.status(500).send({
  			"error":"Failed to connect to the database (1)"
  		})
  	}else{
    // console.log('The solution is: ', results);
    if(results.length >0){
       // email exists
       res.status(400).send({
       	"error":"E-Mail already exists"
       })
   }else{
   	connection.query('INSERT INTO KLASSY_DICK SET ?',users, function (error, results, fields) {
   		if (error) {
   			console.log("error ocurred",error);
   			res.status(500).send({
   				"error":"Failed to connect to the database(2)"
   			});
   		}
   	});
    //send email to confirm
   	//release connection
   	
   	sendConfirm(email,res,req,aioemailcheck);
   	
   	
   	
   }
}

});
}
});

//verify tockens from calls being made from this point forward.
function verifyToken(req, res, next) {
  var jwt = require('jsonwebtoken');
  var config = require('../config');
  var token = req.headers['x-access-token'];
  if (!token)
    return res.status(403).send({ auth: false, message: 'No token provided.' });
  jwt.verify(token, config.secret, function(err, decoded) {
    if (err)
    return res.status(500).send({ auth: false, message: 'Failed to authenticate token.' });
    // if everything good, save to request for use in other routes
    req.userId = decoded.id;
    next();
  });
}


/* Confirm API routes correctly.*/
router.get('/test', verifyToken, function(req, res) {
  res.send('Routing functional to User manager controller' + req.userId);
});

// Verify token is still valid and use is signed in
router.get('/verify_token', verifyToken, (req, res) => { 
  res.status(200).send();
})

router.get('/get_userId', verifyToken, (req, res) => {
  res.send(""+req.userId)
})

//returns info on user from db if given user account ID
router.get('/get_user/:id',verifyToken,function(req,res){
	let user_id = req.params.id;
	connection.query("SELECT * FROM AIOMINER.KLASSY_DICK WHERE id=?",[user_id],function(err,result){
		if (err) throw err;
		
		res.setHeader('Content-Type', 'applications/json');
		res.status(200).send(result);
	});


});

//returns all users from db
// router.get('/User',function(req,res,next){
// 	let user_id = req.params.id;
// 	connection.query("SELECT * FROM AIOMINER.KLASSY_DICK ",function(err,result){
// 		if (err) throw err;
		
// 		res.setHeader('Content-Type', 'applications/json');
// 		res.status(200).send(result);
// 	});


// });


//returns info on rig list from db if given user ID
router.get('/get_rigs/:id',verifyToken,function(req,res,next){
	let user_id = req.params.id;
	connection.query("SELECT * FROM AIOMINER.RIGS WHERE id=?",[user_id],function(err,result){
		if (err) throw err;
		
		res.setHeader('Content-Type', 'applications/json');
		res.status(200).send(result);
	});


});






//update  user into database
router.put('/User',verifyToken,function(req,res,next){
	const bcrypt = require('bcrypt');
	var today = new Date();
	var salt = bcrypt.genSaltSync(10);
	var hash = bcrypt.hashSync(req.body.password, salt);
	var apikey = require("apikeygen").apikey;
	var aioapi = apikey()
	var validator = require('validator');
  // Check to see if this APIKey has been used, if so get another
  var aioemailcheck = apikey(15)
  var mandrill = require('mandrill-api/mandrill');
  var mandrill_client = new mandrill.Mandrill('FUQz1q5p1ML6bgL57ighkA');
  // Check if username or password is missing
  if (isEmpty(req.body.password)) {
  	res.status(400).send({"error":"Blank Password"});
  }
  if (isEmpty(req.body.email)) {
  	res.status(400).send({"error":"Blank Username"});
  }
  
  	var data={
  		"email":req.body.email,
  		"password":hash,
  		"modified":today,
    }
    let emailcheck = req.body.email;
  //Check if e-mail already exists first
  connection.query('SELECT * FROM USERS WHERE id = ?',req.body.id, function (error, results, fields) {
  	if (error) {
  		console.log("error ocurred",error);
  		res.status(500).send({
  			"error":"Failed to connect to the database (1)"
  		})
  	}
    // console.log('The solution is: ', results);
    if(results.length === 0){
       // email exists
       res.status(400).send({
       	"error":"Unable to find user."
       })
   }
   else {	
   	connection.query('UPDATE USERS SET ? WHERE id=?',[data,req.body.id], function (error, results, fields) {
   		if (error) {
   			console.log("error ocurred",error);
   			res.status(500).send({
   				"error":"Failed to connect to the database(2)"
   			});
   		}
   		else{
   		connection.release();
   		res.status(200).send({"success":"USER UPDATED!"});

   	}
   	});

   	}

   


});

});

















module.exports = router;
