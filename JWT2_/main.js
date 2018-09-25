const requestPr = require('request-promise')
const readline = require('readline-sync');
const snooze = ms => new Promise(resolve => setTimeout(resolve, ms));
var name = readline.question("usuario? ");
var password = readline.question("contrasennia? ");
var token;
var url = 'http://localhost:81/JWT2_';
var userJSON = { "Username": name, "Password": password};
const options = {
    method: 'POST',
    uri: url + '/api/Login/authenticate',
    type: 'application/json',
    body: userJSON,
    json: true
};
function getStudents(opciones){
    requestPr(opciones).then((response) => {
        response = JSON.parse(response);
        for(var i = 0; i < response.length; i++) console.log(response[i]);
    });
};
const main = async () => {
    requestPr(options)
    .then((response) => {
        token = response;
    })
    .catch((exception) => {
        console.log(exception);
    })
    await snooze(200);
    const optionsG = {
        method: 'GET',
        uri: url + '/api/customers',
        headers: {
            'Authorization': token
        }
    }; 
    do{
        var opcion = parseInt(readline.question('MENU \n 1.- Ver estudiantes '));
        switch (opcion) {
            case 1:
                getStudents(optionsG);
                break;
        
            default:
            console.log(`Opcion ${opcion} es invalida`);
                break;
        }
        await snooze(200);
        out = readline.question('Desea repetir 1.- seguir, 2.- salir ');
    }while(out != 2);
};
main();