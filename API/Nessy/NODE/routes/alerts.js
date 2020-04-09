// var mysql = require('mysql');
// var SuperSekret = 'XXX'
// var ServerHostName = "XXX"
// var connection = mysql.createConnection({
//   host     : '{{WEBSITE}}',
//   port     : '9006',
//   user     : 'XXX',
//   password : 'XXX',
//   database : 'AIOMINER'
// });

// function isEmpty(strIn) {
//   if (strIn === undefined) {
//     return true;
//   } else if (strIn == null) {
//     return true;
//   } else if (strIn == "") {
//     return true;
//   } else {
//     return false;
//   }
// }



// connection.connect(function(err) {
//   if (!err) {
//     console.log("Database connection is ready to rock!");
//   } else {
//     console.log("Database connection is being a jerk!");
//   }
// });
// ////////////////////
// ////Alerts/////////
// ///PUT * /alert?api_key=*******&rigname=*******&message=********

// exports.alert = function(req, res) {
//   var mandrill = require('mandrill-api/mandrill');
//   var mandrill_client = new mandrill.Mandrill('XXX');
//   console.log(req.query.api_key);
//   var today = new Date();
//   console.log(req.query.rigname);
//   console.log(req.query.message);

//   if (isEmpty(req.query.api_key)) {
//     res.status(400).send({
//       "error": "invalid apikey"
//     });
//   } else if (isEmpty(req.query.rigname)) {
//     res.status(400).send({
//       "error": "invalid rigname"
//     });
//   } else if (isEmpty(req.query.message)) {
//     res.status(400).send({
//       "error": "invalid message"
//     });
//   } else {
//     // Confirm this is a rig that the user owns
//     connection.query('SELECT rigname FROM RIGS WHERE api_key = ?', [req.query.api_key], function(error, results, fields) {
//       if (error) {
//         console.log(error)
//         res.status(500).send({
//           "error": "Failed to connect to the database (1)"
//         })
//       } else {
//         if (results.length > 0) {
//           console.log("Alert - Found the rig and apikey!")
//           connection.query('SELECT * FROM USERS WHERE api_key = ?', [req.query.api_key], function(error, active_results, fields) {
//             if (error) {
//               console.log(error)
//               res.status(500).send({
//                 "error": "Failed to connect to the database (1)"
//               })
//             } else {
//               if (active_results[0].active == "0") {
//                 console.log("Alert - User is disabled")
//                 res.status(400).send({
//                   "error": "Account is disabled"
//                 })
//               } else {
//                 console.log("Alert - Checking if alerts are enabled")
//                 connection.query('SELECT alerts FROM RIGS WHERE api_key = ?', [req.query.api_key], function(error, resultZ, fields) {
//                   if (error) {
//                     console.log(error)
//                     res.status(500).send({
//                       "error": "Failed to connect to the database (1)"
//                     })
//                   } else {
//                     if (resultZ[0].alerts == "0") {
//                       console.log("Alerts are disabled")
//                       var logupdate = {
//                         "api_key": req.query.api_key,
//                         "created": today,
//                         "message": req.query.message
//                       }
//                       console.log("Logging Alert")
//                       connection.query('INSERT INTO LOGS SET ?', logupdate, function(error, log_results, fields) {
//                         if (error) {
//                           res.status(500).send({
//                             "error": "Failed to connect to the database (1)"
//                           })
//                         } else {
//                           res.status(200).send({"results": "Alerts are disabled, but was logged"})
//                         }
//                       })
//                     } else {
//                       console.log("Alerts are enabled")
//                       var logupdate = {
//                         "api_key": req.query.api_key,
//                         "created": today,
//                         "message": req.query.message
//                       }
//                       console.log("Logging Alert")
//                       connection.query('INSERT INTO LOGS SET ?', logupdate, function(error, log_results, fields) {
//                         if (error) {
//                           res.status(500).send({
//                             "error": "Failed to connect to the database (1)"
//                           })
//                         } else {
//                           // Send E-mail
//                           var template_name = "AIO-Alert";
//                           var email = active_results[0].email
//                           console.log(email)
//                           var template_content = [{

//                             }];
//                           var message = {
//                             "html": "",
//                             "text": "",
//                             "subject": "AIOMiner Alert for " & req.query.rigname,
//                             "from_email": "DoNotReply@AIOMiner.com",
//                             "from_name": "AIOMiner",
//                             "to": [{
//                               "email": email,
//                               "type": "to"
//                             }],
//                             "headers": {
//                               "Reply-To": "DoNotReplay@AIOMiner.com"
//                             },
//                             "merge_language": "handlebars",
//                             "merge": true,
//                             "global_merge_vars": [{
//                                 "name": "RIGNAME",
//                                 "content": req.query.rigname
//                               },
//                               {
//                                 "name": "MESSAGE",
//                                 "content": req.query.message
//                               }
//                             ],
//                           }
//                           console.log("Sending Email!")
//                           mandrill_client.messages.sendTemplate({
//                             "template_name": template_name,
//                             "template_content": template_content,
//                             "message": message
//                           }, function(result) {
//                             res.status(200).send({
//                               "status": "Alert - Email Sent!"
//                             })
//                           })
//                         }
//                       })
//                     }
//                   }
//                 })
//               }
//             }
//           })
//         } else {
//           console.log("Invalid rigname or apikey")
//           res.status(400).send({
//             "error": "Invalid rigname or api_key"
//           })
//         }
//       }
//     })
//   }
// };
