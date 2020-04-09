// let constants = require('../constants');
const db_pool = require('../CORE/aio.db/db_connection');
var paypal = require('paypal-rest-sdk');


module.exports.handlePaypalPost = function(req, res) {

    let datestring = new Date().toISOString().
    replace(/T/, ' '). // replace T with a space
    replace(/\..+/, ''); // delete the dot and everything after

    console.log('\r\n\r\n', datestring, '    incoming req.body ...\r\n',
        '-------------------------------------\r\n',
        req.body
    );

    let paymentid = '';
    let event_type = '';
    let txn_type = ''; // from suscribe path
    let txn_payment_date = '';

    event_type = req.body.event_type ? req.body.event_type : '';
    paymentid = req.body.resource && req.body.resource.parent_payment ? req.body.resource.parent_payment : '';
    txn_type = req.body && req.body.txn_type ? req.body.txn_type : '';
    txn_payment_date = req.body && req.body.payment_date ? req.body.payment_date : '';

    if (paymentid.length > 0) console.log('\r\n       parent payment: ', req.body.resource.parent_payment);
    if (event_type.length > 0) console.log('\r\n       event type: ', event_type);

    console.log('\r\n  txn type ', txn_type);

    /*
switch ($_POST['txn_type']) {
    case 'cart':
          //for products without subscription
     break;
    case 'subscr_payment':
        //subscription payment recieved
        break;

    case 'subscr_signup':
        //subscription bought payment pending
        break;

    case 'subscr_eot':
       //subscription end of term
        break;

    case 'subscr_cancel':
        //subscription canceled
        break;
 }
    */

    if (txn_type.length > 0) {
        switch (txn_type) {
            case 'subscr_cancel':
            case 'subscr_eot':

                var ppemail = req.body.payer_email;
                var lastxactnmsg = txn_type + ' Subscription Cancel/EOT.  Transaction Id: ' + req.body.txn_id + ' Payer email: ' + req.body.payer_email;
                var userid = req.body.custom;


                var nSql = 'CALL uspUpdateForCancel ( ?, ?, ? )';
                db_pool.query(nSql, [userid, lastxactnmsg, ppemail],
                    function(error, results) {
                        nSql = null;
                        ppemail = null;
                        userid = null;
                        lastxactnmsg = null;
                        results = null;
                        if (error) {
                            console.log("\r\n   ******************* Error has occured on txn subscr_cancel, db uspUpdateForCancel", error);
                        } else {
                            console.log('   successfully updated profiles_profile entry as a Normie after subscription end of term ');
                        }
                        return;
                    }
                );
                break;


            case 'subscr_payment':

                var ppemail = req.body.payer_email;
                var lastxactnmsg = txn_type + '  Amt: ' + req.body.payment_gross + '  Transaction Id: ' + req.body.txn_id + '  Payment Date: ' + txn_payment_date + '  Payer email: ' + ppemail;
                var userid = req.body.custom;

                var nSql = 'CALL uspUpdateForPayment ( ?, ?, ? )';
                db_pool.query(nSql, [userid, lastxactnmsg, ppemail],
                    function(error, results) {
                        nSql = null;
                        ppemail = null;
                        userid = null;
                        lastxactnmsg = null;
                        txn_payment_date = null;
                        results = null;
                        if (error) {
                            console.log("\r\n   ******************* Error has occured on txn subscr_payment, db uspUpdateForPayment", error);
                        } else {
                            console.log('   successfully updated profiles_profile entry as a Subscriber ');
                        }
                        return;
                    }
                );
                break;

        }

    }

    res.status(200).json({ message: 'OK!' });
    res.end();
    res = null;


}