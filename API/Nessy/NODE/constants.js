// constants.js

'use strict';

let constants = {
    sandboxUrl: "https://api.sandbox.paypal.com",
    liveUrl: "https://api.paypal.com",

    accesstoken_endpt: "/v1/oauth2/token",

    clientid: "{{CLIENTID}}",
    sekret: "{{XXXX}}",
    live_clientid: "{{XXXX}}",
    live_sekret: "{{XXXX}}"

    // key3: {
    //     subkey1: "subvalue1",
    //     subkey2: "subvalue2"
    // }
};

module.exports =
    Object.freeze(constants); // freeze prevents changes by users