const express = require('express');
const app = express();
const requestPr = require('request-promise');
const port = 82;
const Storage = require('sessionstorage');
var bodyParser = require("body-parser");
var url = 'http://localhost:81/JWT2_';
var token;
var userJSON;
var stds;
var std;

var optionsG;

function getStudents(opciones) {
    requestPr(opciones).then((response) => {
        response = JSON.parse(response);
        stds = response;
    });
};
app.use(bodyParser.urlencoded({
    extended: false
}));
app.use(bodyParser.json());

app.get('/', (req, res) => {
    res.render('index')
})
app.get('/crud', (req, res) => {
    res.render('crud', {
        Students: stds,
        Std: std
    })
})
app.listen(port, () => {
    console.log(`http://localhost:${port}`)
});
app.set('view engine', 'pug');

app.post('/login', (req, res) => {
    userJSON = {
        "Username": req.body.Username,
        "Password": req.body.Password
    };
    var options = {
        method: 'POST',
        uri: url + '/api/Login/authenticate',
        type: 'application/json',
        body: userJSON,
        json: true
    };
    requestPr(options)
        .then((response) => {
            Storage.setItem('token', response);
        })
        .then(() => {
            optionsG = {
                method: 'GET',
                uri: url + '/api/customers',
                headers: {
                    'Authorization': Storage.getItem('token')
                }
            };
            getStudents(optionsG);
        })
        .then(() => {
            setTimeout(() => {
                res.redirect('/crud')
            }, 200);
        })
});
app.post('/getId', (req, res) => {
    var optionsGetId = {
        method: 'GET',
        uri: url + '/api/customers?id=' + req.body.id,
        headers: {
            'Authorization': Storage.getItem('token')
        }
    };
    requestPr(optionsGetId)
    .then((response) => {
        response = JSON.parse(response);
        std = response;
    })
    .then(() => {
        setTimeout(() => {
            res.redirect('/crud')
        }, 200);
    });
})