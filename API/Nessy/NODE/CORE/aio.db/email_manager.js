var express = require('express');
var router = express.Router();
var connection = require('./db_connection');
var mandrill = require('mandrill-api/mandrill');
var mandrill_client = new mandrill.Mandrill('XXX');
var ServerHostName = "{{wEBSITE}}";





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


router.post('/verify',function(req,res,next){
    let user_id = req.body.email;
    let hash = req.body.hash;
    var match = false;
    connection.query('SELECT * FROM KLASSY_DICK WHERE email = ?',[user_id], function (error, results, fields) {
        if (error) {
        console.log("error ocurred",error);
        res.status(500).send({
            "error":"Failed to connect to the database (1)"
        })
    }

    else{
        if(results.length>0 && results[0].hash === req.body.hash ){
            
            match = true;
        }
    }



   

    
    if(match){
    connection.query('UPDATE KLASSY_DICK SET DISABLED = 0 WHERE email=?',[user_id],function(err,result){
        if (err) throw err;
        
        res.setHeader('Content-Type', 'applications/json');
        res.Email = 'Confirmed';
        res.status(200).send({email : 'confirmed'});
    });
}else{
    res.status(400).send({email:'unable to cofirm'});
}

});
 });
function coolDown(req, res, next) {
  
  
  var user_id = req.body.email;
  
 connection.query('SELECT * FROM KLASSY_DICK WHERE email = ?',[user_id], function (error, results, fields) {
if(results[0].cooldown_token == "" || results[0].cooldown_token == null){
    var now = new Date();
    connection.query('UPDATE KLASSY_DICK SET cooldown_token = ? WHERE email=?',[now,user_id],function(err,result){
    next();
    });
}
else{
    var today = new Date();
    var cd_token = Date.parse(results[0].cooldown_token);
    var now_ms = Date.parse(today);
    var dif = now_ms - cd_token;
    
    if(dif < 14400000){
      console.log("still on cd")
      return res.status(200).send({ sent: false, message: 'Still on cooldown.' });  
    }
    else{
    var new_token = new Date();
    connection.query('UPDATE KLASSY_DICK SET cooldown_token = ? WHERE email=?',[new_token,user_id],function(err,result){
    next();
    });   
    }


}




 });
}
router.post('/Email/Alert',function(req,res,next){


let email = req.body.email;
let rig_name = req.body.rigname;
let alert = req.body.message;
var confirmed = false;
let provider = req.body.provider;
connection.query('SELECT * FROM KLASSY_DICK WHERE email = ?',[email], function (error, results, fields) {
        if (error) {
        console.log("error ocurred",error);
        res.status(500).send({
            "error":"Failed to connect to the database (1)"
        })
    }else{
        
        if(results.length>0 && results[0].disabled === '0'){
            
            confirmed = true;
        }
    }


if(confirmed){
    console.log("sending alert");
        var template_name = "";
        if(provider =='XXX'){
          var template_name = "XXX-Alert"
        }
        else{
          var template_name = "AIO-Alert"
        }
        
        var template_content = [{
        "name": "LINKS",
        "content":""
          }];
        var message = {
          "html": "",
          "text": "",
          "subject": "ALERT",
          "from_email": "DoNotReply@AIOMiner.com",
          "from_name": "AIOMiner",
          "to": [{
              "email": req.body.email,
              "type": "to"
            }],
            "headers": {
              "Reply-To": "DoNotReplay@AIOMiner.com"
            },
            "merge_language": "handlebars",
            "merge": true,
            "global_merge_vars": [{
                    "name": "MESSAGE",
                    // Link address here
                    "content":  alert
             },
             {
                    "name":"RIGNAME",
                    "content":rig_name
             }
             




             ],
            }
            mandrill_client.messages.sendTemplate({"template_name": template_name, "template_content": template_content, "message": message}, function(result) {
                res.status(200).send({
                "status":result
            });
          });






}

else{
    res.status(400).send({
        "status" : "Email not confirmed"
    });
}

});







});

router.post('/Email/Alert/Miner_stop',function(req,res,next){



let email = req.body.email;
let rig_name = req.body.rigname;
let alert = "that it has stopped mining.";
var confirmed = false;
let provider = req.body.provider;

connection.query('SELECT * FROM KLASSY_DICK WHERE email = ?',[email], function (error, results, fields) {
        if (error) {
        console.log("error ocurred",error);
        res.status(500).send({
            "error":"Failed to connect to the database (1)"
        })
    }else{
        
        if(results.length>0 && results[0].disabled === '0'){
            
            confirmed = true;
        }
    }


if(confirmed){
    console.log("sending alert");
        var template_name = "";
        if(provider =='XXX'){
          var template_name = "XXX-Alert"
        }
        else{
          var template_name = "AIO-Alert"
        }
        var template_content = [{
        "name": "LINKS",
        "content":""
          }];
        var message = {
          "html": "",
          "text": "",
          "subject": "ALERT-Miner Stopped",
          "from_email": "DoNotReply@AIOMiner.com",
          "from_name": "AIOMiner",
          "to": [{
              "email": req.body.email,
              "type": "to"
            }],
            "headers": {
              "Reply-To": "DoNotReplay@AIOMiner.com"
            },
            "merge_language": "handlebars",
            "merge": true,
            "global_merge_vars": [{
                    "name": "MESSAGE",
                    // Link address here
                    "content":  alert
             },
             {
                    "name":"RIGNAME",
                    "content":rig_name
             }
             




             ],
            }
            mandrill_client.messages.sendTemplate({"template_name": template_name, "template_content": template_content, "message": message}, function(result) {
                res.status(200).send({
                "status":result
            });
          });






}

else{
    res.status(400).send({
        "status" : "Email not confirmed"
    });
}

});







});


router.post('/Email/Alert/Miner_fail',coolDown,function(req,res,next){


let email = req.body.email;
let rig_name = req.body.rigname;
let provider = req.body.provider;

let alert = "that it has crashed.";
var confirmed = false;
connection.query('SELECT * FROM KLASSY_DICK WHERE email = ?',[email], function (error, results, fields) {
        if (error) {
        console.log("error ocurred",error);
        res.status(500).send({
            "error":"Failed to connect to the database (1)"
        })
    }else{
        
        if(results.length>0 && results[0].disabled === '0'){
            
            confirmed = true;
        }
    }


if(confirmed){
    console.log("sending alert");
        var template_name = "";
        if(provider =='XXX'){
          var template_name = "XXX-Alert"
        }
        else{
          var template_name = "AIO-Alert"
        }
        var template_content = [{
        "name": "LINKS",
        "content":""
          }];
        var message = {
          "html": "",
          "text": "",
          "subject": "ALERT-Miner Failed",
          "from_email": "DoNotReply@AIOMiner.com",
          "from_name": "AIOMiner",
          "to": [{
              "email": req.body.email,
              "type": "to"
            }],
            "headers": {
              "Reply-To": "DoNotReplay@AIOMiner.com"
            },
            "merge_language": "handlebars",
            "merge": true,
            "global_merge_vars": [{
                    "name": "MESSAGE",
                    // Link address here
                    "content":  alert
             },
             {
                    "name":"RIGNAME",
                    "content":rig_name
             }
             




             ],
            }
            mandrill_client.messages.sendTemplate({"template_name": template_name, "template_content": template_content, "message": message}, function(result) {
                res.status(200).send({
                "status":result
            });
          });






}

else{
    res.status(400).send({
        "status" : "Email not confirmed"
    });
}

});







});

router.post('/Email/Alert/Miner_start',coolDown,function(req,res,next){


let email = req.body.email;
let rig_name = req.body.rigname;
let provider = req.body.provider;

let alert = "that it has started mining.";
var confirmed = false;
connection.query('SELECT * FROM KLASSY_DICK WHERE email = ?',[email], function (error, results, fields) {
        if (error) {
        console.log("error ocurred",error);
        res.status(500).send({
            "error":"Failed to connect to the database (1)"
        })
    }else{
        
        if(results.length>0 && results[0].disabled === '0'){
            
            confirmed = true;
        }
    }


if(confirmed){
    console.log("sending alert");
        var template_name = "";
        if(provider =='XXX'){
          var template_name = "XXX-Alert"
        }
        else{
          var template_name = "AIO-Alert"
        }
        var template_content = [{
        "name": "LINKS",
        "content":""
          }];
        var message = {
          "html": "",
          "text": "",
          "subject": "ALERT-Miner Started",
          "from_email": "DoNotReply@AIOMiner.com",
          "from_name": "AIOMiner",
          "to": [{
              "email": req.body.email,
              "type": "to"
            }],
            "headers": {
              "Reply-To": "DoNotReplay@AIOMiner.com"
            },
            "merge_language": "handlebars",
            "merge": true,
            "global_merge_vars": [{
                    "name": "MESSAGE",
                    // Link address here
                    "content":  alert
             },
             {
                    "name":"RIGNAME",
                    "content":rig_name
             }
             




             ],
            }
            mandrill_client.messages.sendTemplate({"template_name": template_name, "template_content": template_content, "message": message}, function(result) {
                res.status(200).send({
                "status":result
            });
          });






}

else{
    res.status(400).send({
        "status" : "Email not confirmed"
    });
}

});







});











router.get('/')




















module.exports = router;